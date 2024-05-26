using System;
using System.Collections.Generic;
using FurrFieldStudio.MonitoringSuite.Base;
using UnityEngine;

namespace FurrFieldStudio.MonitoringSuite.SO
{
    [Serializable]
    public class MonitoringObjectEntrySO
    {
        public string ObjectName;
        
        public string CurrentValue;

        public List<ObjectEventEntry> Entries;
        
        public MonitoringObjectEntrySO() {}

        public MonitoringObjectEntrySO(string objectName)
        {
            ObjectName = objectName;
            Entries = new List<ObjectEventEntry>();
        }

        public void AddEntry(string value)
        {
            string previousValue = CurrentValue == null ? "" : CurrentValue;

            Entries.Add(new ObjectEventEntry(previousValue, value));

            CurrentValue = value;
        }
    }
}