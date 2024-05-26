using FurrFieldStudio.MonitoringSuite.Base;
using FurrFieldStudio.MonitoringSuite.SO;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using UnityEngine;
#if UNITY_EDITOR
using System.IO;
using UnityEditor;
#endif

namespace FurrFieldStudio.MonitoringSuite
{
    public static class MonitoringSuiteManager
    {
        public static MonitoringSessionSO MonitoringSessionSO;

#if UNITY_EDITOR
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
        private static void Initialize()
        {
            EditorApplication.playModeStateChanged += PlayModeChanged;
        }

        private static void PlayModeChanged(PlayModeStateChange playModeStateChange)
        {
            if(playModeStateChange != PlayModeStateChange.ExitingPlayMode) return;

            OnExitPlaymode();
        }
#endif

        private static void CreateNewSession()
        {
            MonitoringSessionSO = ScriptableObject.CreateInstance<MonitoringSessionSO>();
            MonitoringSessionSO.name = "Session " + ((DateTime)MonitoringSessionSO.SessionTime).ToString("G", CultureInfo.InvariantCulture).Replace('.', '_').Replace(':', '_').Replace('/', '-');

#if UNITY_EDITOR
            AssetDatabase.CreateAsset(MonitoringSessionSO, Const.SESSIONS_PATH + '/' + MonitoringSessionSO.name + ".asset");
#endif
        }

        private static void OnExitPlaymode()
        {
#if UNITY_EDITOR
            EditorUtility.SetDirty(MonitoringSessionSO);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            DeleteOldSessions();
#endif
        }

        public static void ObjectReset(string objectName)
        {
            ObjectChanged_Internal(objectName, "null");
        }

        public static void ObjectChanged<T>(string objectName, T obj)
        {
            ObjectChanged_Internal(objectName, obj.ToString());
        }
        
        public static void ObjectChanged(string objectName, string entry)
        {
            ObjectChanged_Internal(objectName, entry);
        }

        private static void ObjectChanged_Internal(string objectName, string entry)
        {
            if(MonitoringSessionSO == null) CreateNewSession();
            
            MonitoringObjectEntrySO monitoringObjectEntrySo = MonitoringSessionSO.Entries.Find(entry => entry.ObjectName == objectName);

            if (monitoringObjectEntrySo == null)
            {
                monitoringObjectEntrySo = new MonitoringObjectEntrySO(objectName);
                MonitoringSessionSO.Entries.Add(monitoringObjectEntrySo);
            }

            monitoringObjectEntrySo.AddEntry(entry);

#if !UNITY_EDITOR
            ObjectEventEntry objectEventEntry = monitoringObjectEntrySo.Entries.Last();
            Debug.Log($"[MS] {((DateTime)objectEventEntry.Time).ToString()} - [{monitoringObjectEntrySo.ObjectName}] {objectEventEntry.PreviousValue} -> {objectEventEntry.NewValue}");
#endif
        }

        public static MonitoringSessionSO GetMonitoringSession() => MonitoringSessionSO;

#if UNITY_EDITOR

        private static void DeleteOldSessions()
        {
            string[] sessionsPaths = Directory.GetFiles(Const.SESSIONS_PATH + '/', "*.asset");

            if(sessionsPaths.Count() <= 10)
                return;

            List<MonitoringSessionSO> sessions = sessionsPaths
                                                 .Select(sessionPath => AssetDatabase.LoadAssetAtPath<MonitoringSessionSO>(sessionPath))
                                                 .Where(so => so != null)
                                                 .OrderByDescending(key => (DateTime)key.SessionTime)
                                                 .ToList();

            if(sessions.Count <= 10)
                return;

            string[] sessionsToDelete = new string[sessions.Count - 10];
            Array.ConstrainedCopy(sessions.Select(session => AssetDatabase.GetAssetPath(session)).ToArray(), 10, sessionsToDelete, 0, sessionsToDelete.Length);
            AssetDatabase.DeleteAssets(sessionsToDelete, new List<string>());
        }
#endif
    }
}