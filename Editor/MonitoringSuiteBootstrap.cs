using System.IO;
using UnityEditor;
using UnityEngine;

namespace FurrFieldStudio.MonitoringSuite
{
    public class MonitoringSuiteBootstrap
    {
        [InitializeOnLoadMethod]
        private static void CreateSessionFolder()
        {
            if (!Directory.Exists(Application.dataPath + "/MonitoringSuite"))
            {
                AssetDatabase.CreateFolder("Assets", "MonitoringSuite");
            }
            
            if (!Directory.Exists(Application.dataPath + "/MonitoringSuite/Sessions"))
            {
                AssetDatabase.CreateFolder("Assets/MonitoringSuite", "Sessions");
            }
        }
    }
}