using System;
using FurrFieldStudio.MonitoringSuite.Base;
using UnityEditor;
using UnityEngine;

namespace FurrFieldStudio.MonitoringSuite.Editor.Drawers
{
    [CustomPropertyDrawer(typeof(UTimeSpan))]
    public class TimeSpawnPropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(UnityEngine.Rect position, SerializedProperty property, UnityEngine.GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            position = EditorGUI.PrefixLabel(position, label);
            SerializedProperty ticks = property.FindPropertyRelative("Ticks");

            TimeSpan ts = new TimeSpan(ticks.longValue);
            
            ticks.serializedObject.Update();
            ticks.longValue = ts.Ticks;
            ticks.serializedObject.ApplyModifiedProperties();

            EditorGUI.LabelField(new Rect(
                position.x + 110, position.y,
                100, position.height
            ), ts.ToString());

            EditorGUI.EndProperty();
        }
    }
}