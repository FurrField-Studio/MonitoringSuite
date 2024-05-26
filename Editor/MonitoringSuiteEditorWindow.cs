using System;
using System.Collections.Generic;
using System.Linq;
using FurrFieldStudio.MonitoringSuite.Base;
using FurrFieldStudio.MonitoringSuite.Editor.Utils;
using FurrFieldStudio.MonitoringSuite.Editor.Views;
using FurrFieldStudio.MonitoringSuite.SO;
using UnityEditor;
using UnityEngine;

namespace FurrFieldStudio.MonitoringSuite.Editor
{
    public class MonitoringSuiteEditorWindow : EditorWindow
    {
        private List<MonitoringSessionSO> m_Sessions;

        private int m_ChoosenSession;

        private SessionView m_CurrentView;

        private Dictionary<SessionView, MonitoringSuiteWindowView> m_Views = new Dictionary<SessionView, MonitoringSuiteWindowView>();

        public const int SESSION_LIST_WIDTH = 300;

        [MenuItem("Tools/Monitoring Suite")]
        private static void ShowWindow()
        {
            var window = GetWindow<MonitoringSuiteEditorWindow>();
            window.minSize = new Vector2(900, 200);
            window.titleContent = new GUIContent("Monitoring Suite");
            window.Show();
        }

        private void LoadAllSessions()
        {
            m_Sessions = IO.TryGetUnityObjectsOfTypeFromPath<MonitoringSessionSO>(Const.SESSIONS_PATH);
            m_Sessions.Reverse();
            
            if(m_Views.Count != 0) return;
            m_Views.Add(SessionView.UsageList, new UsageListView());
            m_Views.Add(SessionView.EventLog, new EventLogView());
        }

        private void OnGUI()
        {
            LoadAllSessions();
            
            GUILayout.BeginHorizontal();
            
            RenderSessionList();

            RenderMainPage();
            
            GUILayout.EndHorizontal();
        }

        private void RenderMainPage()
        {
            if(!m_SessionInRange) return;

            foreach(var kvp in m_Views)
            {
                if(!kvp.Value.Initialized)
                    kvp.Value.Initialize(m_Sessions[m_ChoosenSession]);
            }

            GUILayout.BeginVertical();

            GUILayout.BeginHorizontal();
            
            GUILayout.Label(m_Sessions[m_ChoosenSession].name);
            GUILayout.FlexibleSpace();
            
            // TODO: add live view
            // if()
            // GUILayout.Button()

            GUILayout.EndHorizontal();
            
            GUILayout.BeginHorizontal("BOX");

            string[] viewsList = Enum.GetNames(typeof(SessionView));
            for (int i = 0; i < viewsList.Length; i++)
            {
                if (GUILayout.Button(viewsList[i]))
                {
                    m_CurrentView = (SessionView)i;
                }
            }
                
            GUILayout.EndHorizontal();

            m_Views[m_CurrentView].Render();
                
            // GUILayout.FlexibleSpace();
            GUILayout.EndVertical();
        }

        #region SessionList

        private Vector2 m_ScrollPos;
        
        private void RenderSessionList()
        {
            GUILayout.BeginVertical("BOX", GUILayout.Width(SESSION_LIST_WIDTH));
            GUILayout.Label("Sessions");
            
            m_ScrollPos = GUILayout.BeginScrollView(m_ScrollPos, false, true);

            for (int index = 0; index < m_Sessions.Count; index++)
            {
                var monitoringSession = m_Sessions[index];
                if (GUILayout.Button(monitoringSession.name))
                {
                    m_ChoosenSession = index;

                    if(m_SessionInRange)
                    {
                        foreach(var kvp in m_Views)
                        {
                            kvp.Value.Initialized = false;
                        }
                    }
                }
            }

            GUILayout.EndScrollView();
            GUILayout.EndVertical();
        }

        #endregion

        #region Views

        public T GetView<T>() where T : MonitoringSuiteWindowView
        {
            foreach (var kvp in m_Views)
            {
                if (kvp.Value is T)
                {
                    return kvp.Value as T;
                }
            }

            return null;
        }
        
        public void ChangeView<T>() where T : MonitoringSuiteWindowView
        {
            foreach (var kvp in m_Views)
            {
                if (kvp.Value is T)
                {
                    m_CurrentView = kvp.Key;
                }
            }
        }

        #endregion

        private bool m_SessionInRange => m_ChoosenSession > -1 && m_ChoosenSession <= m_Sessions.Count - 1;

        private enum SessionView
        {
            UsageList = 0,
            EventLog = 1
        }
    }
}