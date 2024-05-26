using System;
using FurrFieldStudio.MonitoringSuite.Base;
using UnityEditor;
using UnityEngine;

namespace FurrFieldStudio.MonitoringSuite.Editor.Drawers
{
    [CustomPropertyDrawer(typeof(UDateTime))]
    public class DateTimePropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(UnityEngine.Rect position, SerializedProperty property, UnityEngine.GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            position = EditorGUI.PrefixLabel(position, label);
            SerializedProperty ticks = property.FindPropertyRelative("Ticks");

            DateTime dt = new DateTime(ticks.longValue);
            ticks.serializedObject.Update();
            ticks.longValue = dt.Ticks;
            ticks.serializedObject.ApplyModifiedProperties();

            EditorGUI.LabelField(new Rect(
                position.x + 110, position.y,
                100, position.height
            ), dt.ToString("dd/MM/yyyy HH:mm:ss"));

            EditorGUI.EndProperty();
        }
    }
}