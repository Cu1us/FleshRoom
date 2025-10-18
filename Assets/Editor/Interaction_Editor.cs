using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

[CustomPropertyDrawer(typeof(Interaction)), CustomPropertyDrawer(typeof(ItemInteraction))]
public class Interaction_Editor : PropertyDrawer
{
    public override VisualElement CreatePropertyGUI(SerializedProperty property)
    {
        bool isDefaultInteraction = property.name.StartsWith("Default");

        VisualElement content = new();
        Box header = new()
        {
            style = { flexDirection = FlexDirection.Row }
        };
        if (isDefaultInteraction)
        {
            Label defaultLabel = new(property.displayName)
            {
                style = { width = Length.Percent(82), alignSelf = Align.Center, marginLeft = Length.Percent(3) }
            };
            header.Add(defaultLabel);
        }
        else
        {
            PropertyField typeField = new(property.FindPropertyRelative(nameof(Interaction.Type)), "")
            {
                style = { width = Length.Percent(85) }
            };
            typeField.Bind(property.serializedObject);
            header.Add(typeField);
        }

        VisualElement foldout = new()
        {
            style = { display = DisplayStyle.None }
        };
        bool foldoutShown = false;

        Button hideShowButton = new()
        {
            style = { width = Length.Percent(15) },
            text = "Edit"
        };
        header.Add(hideShowButton);

        hideShowButton.clicked += () =>
        {
            if (foldoutShown)
                foldout.style.display = DisplayStyle.None;
            else
                foldout.style.display = DisplayStyle.Flex;
            foldoutShown = !foldoutShown;
            hideShowButton.text = foldoutShown ? "Close" : "Edit";
        };

        PropertyField conditionField = new(property.FindPropertyRelative(nameof(Interaction.Condition)));
        conditionField.Bind(property.serializedObject);
        foldout.Add(conditionField);

        PropertyField eventField = new(property.FindPropertyRelative(nameof(Interaction.Handler)));
        eventField.Bind(property.serializedObject);
        foldout.Add(eventField);

        content.Add(header);
        content.Add(foldout);

        return content;
    }
}
