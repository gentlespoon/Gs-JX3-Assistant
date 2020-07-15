using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace GsJX3AssistantNativeHelper.Kits
{
    public class TimeoutKit
    {
        LoggingKit loggingKit;
        NotifyIconKit notifyIconKit;
        int timeoutDefault;
        int timeoutCountdown;
        Timer timeoutCountdownTimer;

        int thresholdDanger;
        int thresholdWarning;

        public delegate void Terminate();
        Terminate terminateDelegate;

        public TimeoutKit(int timeoutDefault, Terminate terminateDelegate, NotifyIconKit notifyIconKit, LoggingKit loggingKit)
        {
            this.terminateDelegate = terminateDelegate;
            this.loggingKit = loggingKit;
            this.notifyIconKit = notifyIconKit;
            timeoutCountdownTimer = new System.Timers.Timer(1000);
            timeoutCountdownTimer.Elapsed += timerTickEvent;
            this.timeoutDefault = timeoutDefault;
            thresholdWarning = timeoutDefault / 3 * 2;
            thresholdDanger = timeoutDefault / 3;
            loggingKit.Verbose($"Timeout has been set to {timeoutDefault} seconds.");
        }

        private void timerTickEvent(object s, ElapsedEventArgs e)
        {
            timeoutCountdown--;
            if (timeoutCountdown < thresholdWarning && timeoutCountdown > thresholdDanger)
            {
                notifyIconKit.Warning();
            }
            if (timeoutCountdown <= thresholdDanger && timeoutCountdown > 0)
            {
                notifyIconKit.Danger();
            }
            if (timeoutCountdown <= 0)
            {
                loggingKit.Warn("Heartbeat timeout, terminating.");
                terminateDelegate();
            }
        }

        public void ResetTimeout()
        {
            notifyIconKit.Normal();
            timeoutCountdown = timeoutDefault;
        }

        public void Start()
        {
            ResetTimeout();
            timeoutCountdownTimer.Start();
        }

        public void Stop()
        {
            timeoutCountdownTimer.Stop();
            ResetTimeout();
        }
    }
}
