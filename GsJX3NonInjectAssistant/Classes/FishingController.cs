using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Timers;

using System.Drawing;
using System.Threading;

using YariControl.RealCursorPosition;

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
    public class FishingController
    {

        // Check screen every x milliseconds
        private const int timerInterval = 100;

        // Fishing timeout
        public int timer_timeout = 10; // seconds
        public int timer_timeout_tick = 0;
        public int timer_waitForPickup = 10;
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

        private readonly static Point nullPoint = new Point(0, 0);
        private readonly static Color nullColor = Color.Transparent;


        // fishing mode detection
        private Point coord_Indicator_Regular_SkillBar = nullPoint;
        public Point Coord_Indicator_Regular_SkillBar
        {
            get => coord_Indicator_Regular_SkillBar;
            set
            {
                coord_Indicator_Regular_SkillBar = value;
                if (value != nullPoint)
                {

                }
            }
        }

        private Color color_Indicator_Regular_SkillBar = nullColor;
        public Color Color_Indicator_Regular_SkillBar
        {
            get => color_Indicator_Regular_SkillBar;
            set
            {
                color_Indicator_Regular_SkillBar = value;
                checkCoords();
            }
        }
        

        // successful fishing detection
        private Point coord_Indicator_Success = nullPoint;
        public Point Coord_Indicator_Success
        { 
            get => coord_Indicator_Success;
            set
            {
                coord_Indicator_Success = value;
                checkCoords();
            }
        }

        private Color color_Indicator_Success = nullColor;
        public Color Color_Indicator_Success
        {
            get => color_Indicator_Success;
            set
            {
                color_Indicator_Success = value;
                checkCoords();
            }
}


        // enter fishing mode button
        private Point coord_Button_EnterFishingMode = nullPoint;
        public Point Coord_Button_EnterFishingMode
        { 
            get => coord_Button_EnterFishingMode;
            set
            {
                coord_Button_EnterFishingMode = value;
                checkCoords();
            }
        }
        
        // start fishing button
        private Point coord_Button_StartFishing = nullPoint;
        public Point Coord_Button_StartFishing 
        {
            get => coord_Button_StartFishing; 
            set
            {
                coord_Button_StartFishing = value;
                checkCoords();
            }
        }
        
        // end fishing button
        private Point coord_Button_EndFishing = nullPoint;
        public Point Coord_Button_EndFishing
        {
            get => coord_Button_EndFishing;
            set
            {
                coord_Button_EndFishing = value;
                checkCoords();
            }
        }


        private void checkCoords()
        {
            // required Coords
            if (
                // successful fishing detection set
                coord_Indicator_Success != nullPoint &&
                color_Indicator_Success != nullColor &&
                // start fishing button set
                coord_Button_StartFishing != nullPoint &&
                // end fishing button set
                coord_Button_EndFishing != nullPoint
            )
                state.RequiredCoords = true;
            else
                state.RequiredCoords = false;

            // optional Coords
            if (
                // skillBar coords
                coord_Indicator_Regular_SkillBar != nullPoint &&
                color_Indicator_Regular_SkillBar != nullColor &&
                // enter fishing button
                coord_Button_EnterFishingMode != nullPoint
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

            Color color = ScreenPixelColor.GetPixelColor(coord_Indicator_Success);
            // if the color matches success color, we have a fish
            if (color_Indicator_Success == color)
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
                Color color = ScreenPixelColor.GetPixelColor(coord_Indicator_Regular_SkillBar);
                // if the color matches regular skill bar, it means we are out of fishing mode
                if (color_Indicator_Regular_SkillBar == color)
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
            MouseEvents.SimulateMouseClick(coord_Button_EnterFishingMode, 1);
            state.Started = false;
            state.Stopped = false;

            state.Sleeping = true;
            Thread.Sleep(1000);
            state.Sleeping = false;
        }

        private void Action_StartFishing()
        {
            Console.WriteLine("* Start Fishing");
            MouseEvents.SimulateMouseClick(coord_Button_StartFishing, 2);
            state.Stopped = false;
            state.Started = true;
            timer_timeout_tick = timer_timeout;
            counterTotal++;
        }

        private void Action_StopFishing()
        {
            Console.WriteLine("* Stop Fishing");
            MouseEvents.SimulateMouseClick(coord_Button_EndFishing, 2);
            state.Stopped = true;
            timer_waitForPickup_tick = timer_waitForPickup;
            counterSuccess++;
        }

    }
}
