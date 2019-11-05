using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Timers;

using System.Drawing;
using System.Threading;

using YariControl.RealCursorPosition;
using GsJX3NonInjectAssistant;

namespace GsJX3NonInjectAssistant.Fishing
{


    

    public class State
    {
        public bool RequiredCoords = false;
        public bool OptionalCoords = false;
        public bool Running = false;
        public bool FishingMode = false;
        public bool Started = false;
        public bool Stopped = false;
        public bool Sleeping = false;
    }

    public class ActionControl {
        public Point Coordinates = Common.NullPoint;
        public Color PixelColor = Common.NullColor;
        public string MouseAction = "";
    }

    public class ActionControlDataSet
    {
        public ActionControl RegularSkillBar = new ActionControl();
        public ActionControl SuccessIndicator = new ActionControl();
        public ActionControl EnterFishingMode = new ActionControl();
        public ActionControl StartFishing = new ActionControl();
        public ActionControl StopFishing = new ActionControl();
    }
    public class FishingController
    {

        // Check screen every x milliseconds
        private const int timerInterval = 50;

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

        public void VerifyACDS()
        {
            // required Coords
            if (
                // successful fishing detection set
                ACDS.SuccessIndicator.Coordinates != Common.NullPoint &&
                ACDS.SuccessIndicator.PixelColor != Common.NullColor &&
                // start fishing button set
                ACDS.StartFishing.Coordinates != Common.NullPoint &&
                // stop fishing button set
                ACDS.StopFishing.Coordinates != Common.NullPoint
            )
                state.RequiredCoords = true;
            else
                state.RequiredCoords = false;

            // optional Coords
            if (
                // skillBar coords
                ACDS.RegularSkillBar.Coordinates != Common.NullPoint &&
                ACDS.RegularSkillBar.PixelColor != Common.NullColor &&
                // enter fishing button
                ACDS.EnterFishingMode.Coordinates != Common.NullPoint
            )
                state.OptionalCoords = true;
            else
                state.OptionalCoords = false;
        }


        // constructor
        public FishingController()
        {
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
            counterSuccess = 0;
            counterTotal = 0;
        }




        private void Timer_CheckScreen_Ticker(Object source, ElapsedEventArgs e)
        {
            if (!state.Running) return;
            if (state.Stopped) return;

            Color color = ScreenPixelColor.GetPixelColor(ACDS.SuccessIndicator.Coordinates);
            // if the color matches success color, we have a fish
            if (ACDS.SuccessIndicator.PixelColor == color)
            {
                Action_StopFishing();
            }

        }

        private void Timer_Seconds_Ticker(Object source, ElapsedEventArgs e)
        {
            if (!state.Running || state.Sleeping) return;

            // if we need to check fishing mode and re-enter fishing mode
            if (state.OptionalCoords)
            {
                Color color = ScreenPixelColor.GetPixelColor(ACDS.RegularSkillBar.Coordinates);
                // if the color matches regular skill bar, it means we are out of fishing mode
                if (ACDS.RegularSkillBar.PixelColor == color)
                {
                    Console.WriteLine("* Not in fishing mode");
                    state.FishingMode = false;
                    Action_EnterFishingMode();
                    return;
                }
                else
                {
                    Console.WriteLine("Fishing...");
                    state.FishingMode = true;
                }
            }

            // if we are not fishing
            if (!state.Started)
            {
                Thread.Sleep(1000);
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





        private void Action_EnterFishingMode()
        {
            Console.WriteLine("* Enter Fishing Mode");
            MouseEvents.SimulateMouseClick(ACDS.EnterFishingMode.Coordinates, ACDS.EnterFishingMode.MouseAction);
            state.Started = false;
            state.Stopped = false;

            state.Sleeping = true;
            Thread.Sleep(1000);
            state.Sleeping = false;
        }

        private void Action_StartFishing()
        {
            Console.WriteLine("* Start Fishing");
            MouseEvents.SimulateMouseClick(ACDS.StartFishing.Coordinates, ACDS.StartFishing.MouseAction);
            state.Stopped = false;
            state.Started = true;
            timer_timeout_tick = timer_timeout;
            counterTotal++;
        }

        private void Action_StopFishing()
        {
            Console.WriteLine("* Stop Fishing");
            MouseEvents.SimulateMouseClick(ACDS.StopFishing.Coordinates, ACDS.StopFishing.MouseAction);
            state.Stopped = true;
            timer_waitForPickup_tick = timer_waitForPickup;
            counterSuccess++;
        }

    }
}
