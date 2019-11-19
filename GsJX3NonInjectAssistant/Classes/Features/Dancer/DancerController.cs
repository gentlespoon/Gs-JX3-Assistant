using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Timers;

using System.Drawing;
using System.Threading;

using System.Threading.Tasks;

using GsJX3NonInjectAssistant.Classes.HID.Display;
using GsJX3NonInjectAssistant.Classes.HID.Mouse;
using GsJX3NonInjectAssistant.Classes.HID;

namespace GsJX3NonInjectAssistant.Classes.Features.Dancer
{
    public class State
    {
        public bool DancingMonitor = false;
        public bool CanStartDancing = false;
        public bool CanStopDancing = false;
        public bool Running = false;
        public bool Started = false;
    }

    public class ActionControlDataSet
    {
        public MouseScreenEvent RegularSkillBar = new MouseScreenEvent();
        public MouseScreenEvent StartDancing = new MouseScreenEvent();
        public MouseScreenEvent StopDancing = new MouseScreenEvent();
    }
    public class DancerController
    {
        private IDisplayHelper displayHelper;
        private IMouseReader mouseReader;
        private IMouseSimulator mouseSimulator;

        // Fishing timeout
        public int timer_timeout = 60*30; // seconds
        public int timer_timeout_tick = 0;

        private State state = new State();
        public State State { get => state; }

        private int counterTotal = 0;
        public int CounterTotal { get => counterTotal; }

        private int counterSuccess = 0;
        public int CounterSuccess { get => counterSuccess; }

        // timer setup
        private System.Timers.Timer timer_seconds = new System.Timers.Timer(1000);


        // Action Control Data Set
        public ActionControlDataSet ACDS = new ActionControlDataSet();


        // constructor
        public DancerController(
            IDisplayHelper displayHelper,
            IMouseReader mouseReader,
            IMouseSimulator mouseSimulator
        )
        {
            this.displayHelper = displayHelper;
            this.mouseReader = mouseReader;
            this.mouseSimulator = mouseSimulator;

            timer_seconds.Elapsed += Timer_Seconds_Ticker;
            timer_seconds.AutoReset = true;
        }

        // destructor
        ~DancerController()
        {
            timer_seconds.Stop();
            timer_seconds.Dispose();
        }

        public void VerifyACDS()
        {
            state.CanStartDancing = ACDS.StartDancing.Coordinates != Common.NullPoint;
            state.CanStopDancing = ACDS.StopDancing.Coordinates != Common.NullPoint;
            state.DancingMonitor = ACDS.RegularSkillBar.Coordinates != Common.NullPoint;
        }

        public void Start()
        {
            timer_seconds.Enabled = true;
            state.Running = true;
        }

        public void Stop()
        {
            timer_seconds.Enabled = false;
            Reset();
        }

        private void Reset()
        {
            state.Running = false;
            state.Started = false;
        }

        public void ResetStatistics()
        {
            counterSuccess = 0;
            counterTotal = 0;
        }



        private void Timer_Seconds_Ticker(Object source, ElapsedEventArgs e)
        {
            if (!state.Running) return;

            // if we need to check regular skill bar
            if (state.DancingMonitor)
            {
                Color color = displayHelper.GetColorAt(ACDS.RegularSkillBar.Coordinates);
                // if the color matches regular skill bar, it means we are out of dancing mode
                if (ACDS.RegularSkillBar.PixelColor == color)
                {
                    state.Started = false;
                }
                else
                {
                    state.Started = true;
                }
            }

            // if we are not already dancing
            if (!state.Started)
            {
                Action_StartDancing();
                return;
            }

            // normal tick action
            if (state.CanStopDancing)
            {
                timer_timeout_tick--;
                if (timer_timeout_tick < 1)
                {
                    Action_StopDancing();
                }
            }

        }

        private void Action_StartDancing()
        {
            mouseSimulator.Click(ACDS.StartDancing.Coordinates, ACDS.StartDancing.MouseAction);
            timer_timeout_tick = timer_timeout;
            counterTotal++;
        }

        private void Action_StopDancing()
        {
            mouseSimulator.Click(ACDS.StopDancing.Coordinates, ACDS.StopDancing.MouseAction);
            counterSuccess++;
        }

    }
}
