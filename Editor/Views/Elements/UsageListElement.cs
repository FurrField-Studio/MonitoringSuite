using System.Linq;
using FurrFieldStudio.MonitoringSuite.SO;
using UnityEditor;
using UnityEngine;

namespace FurrFieldStudio.MonitoringSuite.Editor.Views.Elements
{
    public class UsageListElement
    {
        public MonitoringObjectEntrySO MonitoringObjectEntry { get; private set; }

        private int m_UnneededChanges;

        public const int WIDTH = 300;
        
        public UsageListElement(MonitoringObjectEntrySO monitoringObjectEntrySo)
        {
            MonitoringObjectEntry = monitoringObjectEntrySo;
            m_UnneededChanges = MonitoringObjectEntry.Entries.Where(entry => entry.NewValue == entry.PreviousValue).Count();
        }

        public void Render()
        {
            GUIStyle guiStyle = new GUIStyle("BOX");
            guiStyle.fixedWidth = WIDTH;
            
            GUILayout.BeginVertical(guiStyle);

            if (GUILayout.Button(MonitoringObjectEntry.ObjectName))
            {
                var monitoringSuite = EditorWindow.GetWindow<MonitoringSuiteEditorWindow>();
                monitoringSuite.GetView<EventLogView>().ApplyNameFilter(MonitoringObjectEntry.ObjectName);
                monitoringSuite.ChangeView<EventLogView>();
            }

            GUILayout.Label("Changes: " + MonitoringObjectEntry.Entries.Count);
            GUILayout.Label("Unneeded changes: " + m_UnneededChanges);
            
            GUILayout.EndVertical();
        }
    }
}