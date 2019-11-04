using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

using System.Timers;

using YariControl.RealCursorPosition;


namespace GsJX3NonInjectAssistant.Fishing
{
    class FishingController
    {
        private System.Timers.Timer aTimer;

        private readonly static Point nullPoint = new Point(0, 0);
        private readonly static Color nullColor = Color.Transparent;

        private readonly static int timerInterval = 100;


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
            set => color_Indicator_Regular_SkillBar = value;
        }
        

        // successful fishing detection
        private Point coord_Indicator_Success = nullPoint;
        public Point Coord_Indicator_Success
        { 
            get => coord_Indicator_Success;
            set => coord_Indicator_Success = value;
        }

        private Color color_Indicator_Success = nullColor;
        public Color Color_Indicator_Success
        {
            get => color_Indicator_Success;
            set => color_Indicator_Success = value;
        }


        // enter fishing mode button
        private Point coord_Button_EnterFishingMode = nullPoint;
        public Point Coord_Button_EnterFishingMode
        { 
            get => coord_Button_EnterFishingMode;
            set => coord_Button_EnterFishingMode = value;
        }
        
        // start fishing button
        private Point coord_Button_StartFishing = nullPoint;
        public Point Coord_Button_StartFishing 
        {
            get => coord_Button_StartFishing; 
            set => coord_Button_StartFishing = value;
        }
        
        // end fishing button
        private Point coord_Button_EndFishing = nullPoint;
        public Point Coord_Button_EndFishing
        {
            get => coord_Button_EndFishing;
            set => coord_Button_EndFishing = value;
        }

        private bool isRunning = false;

        // External CanStartFishing Interface
        public bool CanStartFishing
        {
            get
            {
                return
                (
                    //fishing mode detection set
                    //coord_Indicator_Regular_SkillBar != nullPoint &&
                    //color_Indicator_Regular_SkillBar != nullColor &&

                    //successful fishing detection set
                    coord_Indicator_Success != nullPoint &&
                    color_Indicator_Success != nullColor &&

                    //enter fishing mode button set
                    //coord_Button_EnterFishingMode != nullPoint &&
                    
                    //start fishing button set
                    coord_Button_StartFishing != nullPoint &&
                    
                    // end fishing button set
                    coord_Button_EndFishing != nullPoint &&
                    
                    // if already running
                    !isRunning
                );
            }
        }

        public bool CanStopFishing { get => isRunning; }


        // constructor
        public FishingController()
        {
            aTimer = new System.Timers.Timer(timerInterval);
            aTimer = new System.Timers.Timer(timerInterval);
            aTimer.Elapsed += TimerTicker;
            aTimer.AutoReset = true;
        }

        // destructor
        ~FishingController()
        {
            aTimer.Stop();
            aTimer.Dispose();
        }


        private void TimerTicker(Object source, ElapsedEventArgs e)
        {
            Console.WriteLine("The Elapsed event was raised at {0:HH:mm:ss.fff}", e.SignalTime);
            tickEvent();
        }


        public void Start()
        {
            aTimer.Enabled = true;
            isRunning = true;
        }

        public void Stop()
        {
            aTimer.Enabled = false;
            isRunning = false;
        }

        public void tickEvent()
        {
            
        }

    }
}
