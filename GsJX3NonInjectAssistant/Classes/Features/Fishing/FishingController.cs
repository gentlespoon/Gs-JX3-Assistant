using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Timers;

using System.Drawing;
using System.Threading;

using System.Threading.Tasks;

using GsJX3NIA.Classes.HID.Display;
using GsJX3NIA.Classes.HID.Mouse;
using GsJX3NIA.Classes.HID;

namespace GsJX3NIA.Classes.Features.Fishing
{
    public class State
    {
        public bool RequiredCoords = false;
        public bool FishingModeMonitor = false;
        public bool AutoRevive = false;
        public bool Running = false;
        public bool FishingMode = false;
        public bool Started = false;
        public bool Stopped = false;
        public bool Sleeping = false;
    }

    public class ActionControlDataSet
    {
        public MouseScreenEvent RegularSkillBar = new MouseScreenEvent();
        public MouseScreenEvent SuccessIndicator = new MouseScreenEvent();
        public MouseScreenEvent EnterFishingMode = new MouseScreenEvent();
        public MouseScreenEvent StartFishing = new MouseScreenEvent();
        public MouseScreenEvent StopFishing = new MouseScreenEvent();
        public MouseScreenEvent Revive = new MouseScreenEvent();
    }
    public class FishingController
    {
        private IDisplayHelper displayHelper;
        private IMouseReader mouseReader;
        private IMouseSimulator mouseSimulator;

        // Check screen every x milliseconds
        private const int timerInterval = 5;

        // Fishing timeout
        public int timer_timeout = 22; // seconds
        public int timer_timeout_tick = 0;
        public int timer_waitForPickup = 8;
        public int timer_waitForPickup_tick = 0;

        private State state = new State();
        public State State { get => state; }

        private int counterTotal = 0;
        public int CounterTotal { get => counterTotal; }

        private int counterSuccess = 0;
        public int CounterSuccess { get => counterSuccess; }

        // timer setup
        private System.Timers.Timer timer_checkScreen = new System.Timers.Timer(timerInterval);
        private System.Timers.Timer timer_seconds = new System.Timers.Timer(1000);


        // Action Control Data Set
        public ActionControlDataSet ACDS = new ActionControlDataSet();


        // constructor
        public FishingController(
            IDisplayHelper displayHelper,
            IMouseReader mouseReader,
            IMouseSimulator mouseSimulator
        )
        {
            this.displayHelper = displayHelper;
            this.mouseReader = mouseReader;
            this.mouseSimulator = mouseSimulator;

            timer_checkScreen.Elapsed += Timer_CheckScreen_Ticker;
            timer_checkScreen.AutoReset = true;

            timer_seconds.Elapsed += Timer_Seconds_Ticker;
            timer_seconds.AutoReset = true;
        }

        // destructor
        ~FishingController()
        {
            timer_checkScreen.Stop();
            timer_checkScreen.Dispose();
            timer_seconds.Stop();
            timer_seconds.Dispose();
        }


        public void VerifyACDS()
        {
            // required Coords
            state.RequiredCoords = 
                Common.NullPoint != ACDS.SuccessIndicator.Coordinates &&
                Common.NullPoint != ACDS.StartFishing.Coordinates &&
                Common.NullPoint != ACDS.StopFishing.Coordinates;

            state.FishingModeMonitor =
                Common.NullPoint != ACDS.RegularSkillBar.Coordinates &&
                Common.NullPoint != ACDS.EnterFishingMode.Coordinates;

            state.AutoRevive =
                Common.NullPoint != ACDS.Revive.Coordinates;

        }



        public void Start()
        {
            timer_checkScreen.Enabled = true;
            timer_seconds.Enabled = true;
            state.Running = true;
        }

        public void Stop()
        {
            timer_checkScreen.Enabled = false;
            timer_seconds.Enabled = false;
            Reset();
        }
        
        private void Reset()
        {
            state.Running = false;
            state.FishingMode = false;
            state.Started = false;
            state.Stopped = false;
            state.Sleeping = false;
            timer_timeout_tick = 0;
            timer_waitForPickup_tick = 0;
        }

        public void ResetStatistics()
        {
            counterSuccess = 0;
            counterTotal = 0;
        }




        private void Timer_CheckScreen_Ticker(Object source, ElapsedEventArgs e)
        {
            if (!state.Running) return;
            if (state.Stopped) return;

            Color color = displayHelper.GetColorAt(ACDS.SuccessIndicator.Coordinates);
            // if the color matches success color, we have a fish
            if (ACDS.SuccessIndicator.PixelColor == color)
            {
                Action_StopFishing();
            }

        }

        private void Timer_Seconds_Ticker(Object source, ElapsedEventArgs e)
        {
            if (!state.Running) return;
            
            // if we need to check fishing mode and re-enter fishing mode
            if (state.FishingModeMonitor)
            {
                Color color = displayHelper.GetColorAt(ACDS.RegularSkillBar.Coordinates);
                // if the color matches regular skill bar, it means we are out of fishing mode
                if (ACDS.RegularSkillBar.PixelColor == color)
                {
                    state.FishingMode = false;
                    Action_EnterFishingMode();
                    return;
                }
                else
                {
                    state.FishingMode = true;
                }
            }

            // if we need to automatically revive
            if (ACDS.Revive.Coordinates != Common.NullPoint)
            {
                Color color = displayHelper.GetColorAt(ACDS.Revive.Coordinates);
                if (ACDS.Revive.PixelColor == color)
                {
                    Action_Revive();
                    return;
                }
            }

            // if we are not fishing
            if (!state.Started)
            {
                Action_StartFishing();
            }
            // if we are fishing and we are not waiting for UI delay,
            // start fishing every 15 or so seconds
            if (state.Started && !state.Stopped)
            {
                timer_timeout_tick--;
                if (timer_timeout_tick <= 0)
                {
                    // not successful
                    Action_StartFishing();
                }
            }
            if (state.Started && state.Stopped)
            {
                timer_waitForPickup_tick--;
                if (timer_waitForPickup_tick <= 0)
                {
                    // successfully picked up
                    Action_StartFishing();
                }
            }
        }



        private void Sleep(int milliseconds = 3000)
        {
            state.Sleeping = true;
            System.Timers.Timer t = new System.Timers.Timer();
            t.Interval = milliseconds; // In milliseconds
            t.AutoReset = false; // Stops it from repeating
            t.Elapsed += new ElapsedEventHandler(Wake);
            t.Start();
        }
        private void Wake(object sender, ElapsedEventArgs e)
        {
            state.Sleeping = false;
        }



        private void Action_EnterFishingMode()
        {
            if (state.Sleeping) return;
            Sleep(3000);
            mouseSimulator.Click(ACDS.EnterFishingMode.Coordinates, ACDS.EnterFishingMode.MouseAction);
            state.Started = false;
            state.Stopped = false;
        }

        private void Action_StartFishing()
        {
            if (state.Sleeping) return;
            Sleep(3000);
            mouseSimulator.Click(ACDS.StartFishing.Coordinates, ACDS.StartFishing.MouseAction);
            state.Stopped = false;
            state.Started = true;
            timer_timeout_tick = timer_timeout;
            counterTotal++;
        }

        private void Action_StopFishing()
        {
            if (state.Sleeping) return;
            Sleep(3000);
            mouseSimulator.Click(ACDS.StopFishing.Coordinates, ACDS.StopFishing.MouseAction);
            timer_waitForPickup_tick = timer_waitForPickup;
            counterSuccess++;
            state.Stopped = true;
        }

        private void Action_Revive()
        {
            if (state.Sleeping) return;
            mouseSimulator.Click(ACDS.Revive.Coordinates, ACDS.StartFishing.MouseAction);
        }

    }
}
