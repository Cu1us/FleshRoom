using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

[CustomPropertyDrawer(typeof(DialogueSequenceStep))]
public class DialogueSequenceStep_Editor : PropertyDrawer
{
    public override VisualElement CreatePropertyGUI(SerializedProperty property)
    {
        SerializedProperty speakerProp = property.FindPropertyRelative(nameof(DialogueSequenceStep.Speaker));
        PropertyField speakerField = new(speakerProp)
        {
            tooltip = "Leave null to create a pure wait block."
        };
        speakerField.Bind(property.serializedObject);

        Color windowTextColor = Color.white;
        if (speakerProp?.objectReferenceValue != null && (speakerProp.objectReferenceValue as Dialogue) != null)
        {
            windowTextColor = (speakerProp.objectReferenceValue as Dialogue).SpeakerColor;
        }

        UnityEngine.UIElements.PopupWindow window = new()
        {
            style = { unityFontStyleAndWeight = FontStyle.Bold, color = windowTextColor }
        };

        PropertyField lineField = new(property.FindPropertyRelative(nameof(DialogueSequenceStep.Line)));
        speakerField.Bind(property.serializedObject);
        window.Add(speakerField);

        window.Add(lineField);

        SerializedProperty durationProp = property.FindPropertyRelative(nameof(DialogueSequenceStep.Duration));
        PropertyField durationField = new(durationProp);
        durationField.Bind(property.serializedObject);
        window.Add(durationField);

        OnFieldChanged();
        speakerField.RegisterValueChangeCallback((callback) => OnFieldChanged());
        durationField.RegisterValueChangeCallback((callback) => OnFieldChanged());

        return window;

        void OnFieldChanged()
        {
            if (speakerProp?.objectReferenceValue == null)
            {
                window.text = durationProp.floatValue > 0 ? "[ Wait block ]" : "[ No speaker ]";
            }
            else
            {
                window.text = speakerProp.objectReferenceValue.name;
            }
        }
    }
}