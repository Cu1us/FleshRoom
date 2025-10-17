using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

[CustomPropertyDrawer(typeof(Interaction))]
public class Interaction_Editor : PropertyDrawer
{
    public override VisualElement CreatePropertyGUI(SerializedProperty property)
    {
        VisualElement content = new();
        Box header = new()
        {
            style = { flexDirection = FlexDirection.Row }
        };
        PropertyField typeField = new(property.FindPropertyRelative(nameof(Interaction.Type)), "")
        {
            style = { width = Length.Percent(70) }
        };
        typeField.Bind(property.serializedObject);
        header.Add(typeField);
        Button makeEventButton = new(() => MakeEvent(property))
        {
            style = { width = Length.Percent(15) },
            text = "Event"
        };
        header.Add(makeEventButton);
        Button makeRefButton = new(() => MakeRef(property))
        {
            style = { width = Length.Percent(15) },
            text = "Ref"
        };
        header.Add(makeRefButton);
        content.Add(header);

        // PropertyField handlerField1 = new(property.FindPropertyRelative(nameof(Interaction.Handler)))
        // {
        //     dataSourceType = typeof(IInteractionHandler)
        // };
        // handlerField1.Bind(property.serializedObject);
        // content.Add(handlerField1);

        ObjectField handlerField = new("Handler")
        {
            objectType = typeof(IInteractionHandler)
        };
        handlerField.bindingPath = property.FindPropertyRelative(nameof(Interaction.Handler)).propertyPath;
        handlerField.Bind(property.serializedObject);
        content.Add(handlerField);

        return content;
    }

    void MakeEvent(SerializedProperty property)
    {
        Debug.Log("Made event");
        SerializedProperty handler = property.FindPropertyRelative(nameof(Interaction.Handler));
        handler.boxedValue = new InteractionEvent();
        property.serializedObject.ApplyModifiedProperties();
    }
    void MakeRef(SerializedProperty property)
    {
        Debug.Log("Made ref");
        SerializedProperty handler = property.FindPropertyRelative(nameof(Interaction.Handler));
        handler.boxedValue = null;
        property.serializedObject.ApplyModifiedProperties();
    }
}
