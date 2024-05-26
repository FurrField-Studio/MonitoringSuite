using System;
using System.Collections.Generic;
using System.Linq;
using FurrFieldStudio.MonitoringSuite.Editor.Views.Elements;
using FurrFieldStudio.MonitoringSuite.SO;
using UnityEditor;
using UnityEngine;

namespace FurrFieldStudio.MonitoringSuite.Editor.Views
{
    public class EventLogView : MonitoringSuiteWindowView
    {
        private MonitoringSessionSO m_MonitoringSession;

        private List<EventLogElement> m_EventLogElements;

        private string m_NameFilter;
        
        private bool m_IgnoreRepeatedChanges;
        
        public override void Initialize(MonitoringSessionSO monitoringSession)
        {
            m_MonitoringSession = monitoringSession;

            m_EventLogElements = new List<EventLogElement>();
            for (int index = 0; index < m_MonitoringSession.Entries.Count; index++)
            {
                foreach (var objectEventEntry in m_MonitoringSession.Entries[index].Entries)
                {
                    m_EventLogElements.Add(new EventLogElement(m_MonitoringSession.Entries[index], objectEventEntry));
                }
            }
            
            m_EventLogElements.Sort((a, b) => b.ObjectEventEntry.RelativeTime.Ticks.CompareTo(a.ObjectEventEntry.RelativeTime.Ticks));

            Initialized = true;
        }

        public void ApplyNameFilter(string name)
        {
            m_NameFilter = name;
        }

        private Vector2 m_ScrollPos;

        public override void Render()
        {
            GUILayout.BeginVertical("BOX");

            GUILayout.BeginHorizontal();
            GUILayout.Label("Event Log");
            GUILayout.FlexibleSpace();
            m_IgnoreRepeatedChanges = GUILayout.Toggle(m_IgnoreRepeatedChanges, "Ignore repeated changes");
            GUILayout.EndHorizontal();

            m_NameFilter = EditorGUILayout.TextField("Search: ", m_NameFilter);

            m_ScrollPos = EditorGUILayout.BeginScrollView(m_ScrollPos);
            GUILayout.BeginVertical();
                    
            foreach (var eventLogElement in m_EventLogElements)
            {
                if (string.IsNullOrEmpty(m_NameFilter))
                {
                    eventLogElement.Render(m_IgnoreRepeatedChanges);
                }
                else if (eventLogElement.MonitoringObjectEntry.ObjectName.ToLower().Contains(m_NameFilter.ToLower()))
                {
                    eventLogElement.Render(m_IgnoreRepeatedChanges);
                }
            }
                    
            GUILayout.EndVertical();
            GUILayout.EndScrollView();
            
            GUILayout.EndVertical();
        }
    }
}