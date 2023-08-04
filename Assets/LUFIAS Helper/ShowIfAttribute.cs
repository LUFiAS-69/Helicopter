#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;


[System.AttributeUsage(System.AttributeTargets.Field)]
public class ShowIfAttribute : PropertyAttribute
{
    public string boolFieldName;
    public ShowIfAttribute(string boolFieldName)
    {
        this.boolFieldName = boolFieldName;
    }
}

#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(ShowIfAttribute))]
public class ShowIfDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        ShowIfAttribute showIf = attribute as ShowIfAttribute;
        SerializedProperty boolProperty = property.serializedObject.FindProperty
            (showIf.boolFieldName);
        if (boolProperty == null)
        {
            EditorGUI.PropertyField(position, property, label, true);
        }
        else
        {
            bool value = boolProperty.boolValue;
            if (value)
            {
                EditorGUI.PropertyField(position, property, label, true);
            }
        }
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        ShowIfAttribute showIf = attribute as ShowIfAttribute;
        SerializedProperty boolProperty = property.serializedObject.FindProperty
            (showIf.boolFieldName);
        if (boolProperty == null)
        {
            return EditorGUI.GetPropertyHeight(property, label);
        }
        else
        {
            bool value = boolProperty.boolValue;
            if (value)
            {
                return EditorGUI.GetPropertyHeight(property, label);
            }
            else
            {
                return 0f;
            }
        }
    }
}
#endif
