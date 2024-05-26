using System;
using UnityEngine;

namespace FurrFieldStudio.MonitoringSuite.Base
{
    [Serializable]
    public struct UTimeSpan
    {
        public UTimeSpan(long ticks)
        {
            Ticks = ticks;
            m_TimeSpan = new TimeSpan(ticks);
        }

        [SerializeField]
        public long Ticks;

        private TimeSpan m_TimeSpan;

        public static implicit operator TimeSpan(UTimeSpan uts) => new TimeSpan(uts.Ticks);

        public static implicit operator UTimeSpan(TimeSpan ts) => new UTimeSpan(ts.Ticks);
    }
}