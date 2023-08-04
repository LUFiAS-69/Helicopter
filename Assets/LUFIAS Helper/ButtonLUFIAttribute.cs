#region Editor
#if UNITY_EDITOR
using System.Reflection;
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using Object = UnityEngine.Object;
#endif
#endregion
using System;
using JetBrains.Annotations;

[AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
public sealed class ButtonLUFIAttribute : Attribute
{
    public readonly string Name;
    public ButtonLUFIAttribute() { }

    public ButtonLUFIAttribute(string name) => Name = name;

    [PublicAPI]
    public ButtonMode Mode { get; set; } = ButtonMode.AlwaysEnabled;

    [PublicAPI]
    public ButtonSpacing Spacing { get; set; } = ButtonSpacing.None;
}

public enum ButtonMode { AlwaysEnabled, EnabledInPlayMode, DisabledInPlayMode }

[Flags]
public enum ButtonSpacing
{
    None = 0,
    Before = 1,
    After = 2
}

#region Editor
#if UNITY_EDITOR
public abstract class ButtonLUFI
{
    [PublicAPI] public readonly string DisplayName;
    [PublicAPI] public readonly MethodInfo Method;

    private readonly ButtonSpacing _spacing;
    private readonly bool _disabled;

    protected ButtonLUFI(MethodInfo method, ButtonLUFIAttribute buttonAttribute)
    {
        DisplayName = string.IsNullOrEmpty(buttonAttribute.Name)
            ? ObjectNames.NicifyVariableName(method.Name)
            : buttonAttribute.Name;

        Method = method;

        _spacing = buttonAttribute.Spacing;

        bool inAppropriateMode = EditorApplication.isPlaying
            ? buttonAttribute.Mode == ButtonMode.EnabledInPlayMode
            : buttonAttribute.Mode == ButtonMode.DisabledInPlayMode;

        _disabled = !(buttonAttribute.Mode == ButtonMode.AlwaysEnabled || inAppropriateMode);
    }

    public void Draw(IEnumerable<object> targets)
    {
        using (new EditorGUI.DisabledScope(_disabled))
        {
            DrawInternal(targets);
        }
    }

    internal static ButtonLUFI Create(MethodInfo method, ButtonLUFIAttribute buttonAttribute)
    {
        var parameters = method.GetParameters();

        if (parameters.Length == 0)
        {
            return new ButtonWithoutParams(method, buttonAttribute);
        }
        else
        {
            Debug.Log("Function with Button attribute, cannot have parameters");
            return new ButtonWithoutParams(method, buttonAttribute);
        }
    }

    protected abstract void DrawInternal(IEnumerable<object> targets);
}

internal class ButtonWithoutParams : ButtonLUFI
{
    public ButtonWithoutParams(MethodInfo method, ButtonLUFIAttribute buttonAttribute)
        : base(method, buttonAttribute) { }

    protected override void DrawInternal(IEnumerable<object> targets)
    {
        if (!GUILayout.Button(DisplayName))
            return;

        foreach (object obj in targets)
        {
            Method.Invoke(obj, null);
        }
    }
}

internal static class DrawUtility
{
    public readonly struct VerticalIndent : IDisposable
    {
        private const float SpacingHeight = 10f;
        private readonly bool _bottom;

        public VerticalIndent(bool top, bool bottom)
        {
            if (top)
                GUILayout.Space(SpacingHeight);

            _bottom = bottom;
        }

        public void Dispose()
        {
            if (_bottom)
                GUILayout.Space(SpacingHeight);
        }
    }

}

[CustomEditor(typeof(Object), true)]
[CanEditMultipleObjects]
internal class ObjectEditor : Editor
{
    private ButtonsDrawer _buttonsDrawer;

    private void OnEnable()
    {
        _buttonsDrawer = new ButtonsDrawer(target);
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        _buttonsDrawer.DrawButtons(targets);
    }
}

public class ButtonsDrawer
{
    [PublicAPI]
    public readonly List<ButtonLUFI> Buttons = new List<ButtonLUFI>();

    public ButtonsDrawer(object target)
    {
        const BindingFlags flags = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
        var methods = target.GetType().GetMethods(flags);

        foreach (MethodInfo method in methods)
        {
            var buttonAttribute = method.GetCustomAttribute<ButtonLUFIAttribute>();

            if (buttonAttribute == null)
                continue;

            Buttons.Add(ButtonLUFI.Create(method, buttonAttribute));
        }
    }

    public void DrawButtons(IEnumerable<object> targets)
    {
        foreach (ButtonLUFI button in Buttons)
        {
            button.Draw(targets);
        }
    }
}

#endif
#endregion