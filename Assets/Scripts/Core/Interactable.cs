using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class Interactable : MonoBehaviour
{
    [Space(10)]
    public string Name;
    [Space(10)]
    [SerializeField] Interaction[] Interactions;
    [SerializeField] ItemInteraction[] ItemInteractions;
    [Space(10)]
    [SerializeField] Interaction DefaultInteraction;
    [SerializeField] ItemInteraction DefaultItemInteraction;

    public void Interact(InteractionType interaction)
    {
        bool found = false;
        for (int i = 0; i < Interactions.Length; i++)
        {
            if (Interactions[i].Type == interaction)
            {
                if (Interactions[i].Condition == null || Interactions[i].Condition.CanInteract())
                    Interactions[i].Handler.Interact(interaction);
                found = true;
            }
        }
        if (!found)
        {
            DefaultInteraction?.Handler?.Interact(interaction);
        }
    }
    public void InteractItem(Item item)
    {
        bool found = false;
        for (int i = 0; i < ItemInteractions.Length; i++)
        {
            if (ItemInteractions[i].Type == item)
            {
                if (ItemInteractions[i].Condition == null || ItemInteractions[i].Condition.CanInteract())
                    ItemInteractions[i].Handler.Interact(item);
                found = true;
            }
        }
        if (!found)
        {
            DefaultItemInteraction?.Handler?.Interact(item);
        }
    }
}