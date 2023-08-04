#region Editor
#if UNITY_EDITOR
using UnityEditor;
#endif
#endregion
using UnityEngine;


public class ReadOnlyLUFIAttribute : PropertyAttribute
{
    
}


#region Editor
#if UNITY_EDITOR

[CustomPropertyDrawer(typeof(ReadOnlyLUFIAttribute))]
public class ReadOnlyLUFIPropertyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        GUI.enabled = false;
        EditorGUI.PropertyField(position, property, label);
        GUI.enabled = true;
    }
}

#endif
#endregion