
using FurrFieldStudio.MonitoringSuite.Editor.Views.Elements;
using FurrFieldStudio.MonitoringSuite.SO;
using UnityEditor;
using UnityEngine;

namespace FurrFieldStudio.MonitoringSuite.Editor.Views
{
    public class UsageListView : MonitoringSuiteWindowView
    {
        private MonitoringSessionSO m_MonitoringSession;

        private UsageListElement[] m_UsageListElements;
        
        private string m_NameFilter;

        private MonitoringSuiteEditorWindow m_Window;
        
        public override void Initialize(MonitoringSessionSO monitoringSession)
        {
            m_MonitoringSession = monitoringSession;

            m_UsageListElements = new UsageListElement[m_MonitoringSession.Entries.Count];
            for (int index = 0; index < m_MonitoringSession.Entries.Count; index++)
            {
                m_UsageListElements[index] = new UsageListElement(m_MonitoringSession.Entries[index]);
            }

            m_Window = EditorWindow.GetWindow<MonitoringSuiteEditorWindow>();

            Initialized = true;
        }

        private Vector2 m_ScrollPos;
        
        public override void Render()
        {
            GUILayout.BeginVertical("BOX");
            
            GUILayout.Label("Usage List");
            
            m_NameFilter = EditorGUILayout.TextField("Search: ", m_NameFilter);

            int availableWidth = (int) m_Window.position.width;
            availableWidth = (int) availableWidth / UsageListElement.WIDTH;
            availableWidth -= 1;
            if(availableWidth <= 0) availableWidth = 1;

            m_ScrollPos = GUILayout.BeginScrollView(m_ScrollPos);
            
            GUILayout.BeginVertical();
            for (int i = 0; i <= m_MonitoringSession.Entries.Count/availableWidth; i++)
            {
                GUILayout.BeginHorizontal();
                
                for(int j = 0; j < availableWidth; j++)
                {
                    int index = i * availableWidth + j;
                    
                    if(index >= m_UsageListElements.Length) continue;
                    
                    if (string.IsNullOrEmpty(m_NameFilter))
                    {
                        m_UsageListElements[index].Render();
                    }
                    else if(m_UsageListElements[index].MonitoringObjectEntry.ObjectName.ToLower().Contains(m_NameFilter.ToLower()))
                    {
                        m_UsageListElements[index].Render();
                    }
                }
                
                GUILayout.EndHorizontal();
            }
            
            GUILayout.EndVertical();
            GUILayout.EndScrollView();
            GUILayout.EndVertical();
        }
    }
}