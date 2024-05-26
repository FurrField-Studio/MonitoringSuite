using System;
using UnityEngine;

namespace FurrFieldStudio.MonitoringSuite.Base
{
    [Serializable]
    public struct UDateTime
    {
        public UDateTime(long ticks)
        {
            Ticks = ticks;
        }

        [SerializeField]
        public long Ticks;

        private DateTime m_DateTime => new DateTime(Ticks);

        public static implicit operator DateTime(UDateTime udt) => udt.m_DateTime;

        public static implicit operator UDateTime(DateTime dt) => new UDateTime(dt.Ticks);
        
        public static UTimeSpan operator -(UDateTime a, UDateTime b)
        {
            return new UTimeSpan(a.Ticks - b.Ticks);
        }
    }
}