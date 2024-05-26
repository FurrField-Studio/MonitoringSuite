using System;
using System.Collections.Generic;
using System.IO;
using FurrFieldStudio.MonitoringSuite.Base;
using FurrFieldStudio.MonitoringSuite.SO;
using System.Linq;
using UnityEditor;
using UnityEditor.Graphs;
using UnityEngine;

namespace FurrFieldStudio.MonitoringSuite.Editor.Views.Elements
{
    public class EventLogElement
    {
        public MonitoringObjectEntrySO MonitoringObjectEntry { get; private set; }

        public ObjectEventEntry ObjectEventEntry { get; private set; }

        private bool m_Folded;

        private const int BUTTON_WIDTH = 20;

        private static readonly Color UNNEDED_CHANGE_COLOR = new Color(1f, 0.92888888888f, 0.11555555555f);
        
        public EventLogElement(MonitoringObjectEntrySO monitoringObjectEntrySo, ObjectEventEntry objectEventEntry)
        {
            MonitoringObjectEntry = monitoringObjectEntrySo;
            ObjectEventEntry = objectEventEntry;
        }
        
        public void Render(bool ignoreRepeatedChanges)
        {
            if(ignoreRepeatedChanges && ObjectEventEntry.PreviousValue == ObjectEventEntry.NewValue) return;
            
            Color defaultColor = GUI.color;

            if(ObjectEventEntry.PreviousValue == ObjectEventEntry.NewValue)
            {
                GUI.backgroundColor = UNNEDED_CHANGE_COLOR;
            }

            GUILayout.BeginVertical("box");
            GUILayout.BeginHorizontal();

            TimeSpan ts = ObjectEventEntry.RelativeTime;
            GUILayout.Label(ts.ToString(), GUILayout.ExpandWidth(false), GUILayout.Width(120));
            EditorGUILayout.SelectableLabel(MonitoringObjectEntry.ObjectName, GUILayout.ExpandWidth(true), GUILayout.Height(16));
            GUILayout.Label("Value change: " + ObjectEventEntry.PreviousValue + " \u2192 " + ObjectEventEntry.NewValue, GUILayout.ExpandWidth(false));

            if(GUILayout.Button(m_Folded ? "\u2191" : "\u2193", GUILayout.Width(BUTTON_WIDTH)))
            {
                m_Folded = !m_Folded;
            }
            
            GUILayout.EndHorizontal();

            if(m_Folded)
            {
                GUILayout.TextArea(string.Join(Environment.NewLine, ReadLines(ObjectEventEntry.StackTrace).Skip(5)));
            }

            GUILayout.EndVertical();
            
            GUI.backgroundColor = defaultColor;
        }
        
        internal static IEnumerable<string> ReadLines(string s)
        {
            string line;
            using (var sr = new StringReader(s))
                while ((line = sr.ReadLine()) != null)
                    yield return line;
        }
    }
}