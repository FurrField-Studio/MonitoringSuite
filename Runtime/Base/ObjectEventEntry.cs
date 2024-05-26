using System;

namespace FurrFieldStudio.MonitoringSuite.Base
{
    [Serializable]
    public class ObjectEventEntry
    {
        public string PreviousValue;
        public string NewValue;

        public UDateTime Time;
        public UTimeSpan RelativeTime;
        
        public string StackTrace;
        
        public ObjectEventEntry() { }

        public ObjectEventEntry(string previousValue, string newValue)
        {
            PreviousValue = previousValue;
            NewValue = newValue;
            StackTrace = Environment.StackTrace;
            Time = DateTime.Now;
            RelativeTime = (Time - MonitoringSuiteManager.GetMonitoringSession().SessionTime);
        }
    }
}