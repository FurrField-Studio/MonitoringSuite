using System;
using System.Collections.Generic;
using FurrFieldStudio.MonitoringSuite.Base;
using UnityEngine;

namespace FurrFieldStudio.MonitoringSuite.SO
{
    [CreateAssetMenu(fileName = "MonitoringSessionSO", menuName = "MonitoringSessionSO", order = 0)]
    public class MonitoringSessionSO : ScriptableObject
    {
        public UDateTime SessionTime = DateTime.Now;
        
        public List<MonitoringObjectEntrySO> Entries = new List<MonitoringObjectEntrySO>();
    }
}