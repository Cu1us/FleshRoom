using System;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField] Interaction[] Interactions;
    [SerializeField] ItemInteraction[] ItemInteractions;

    public void Interact(InteractionType interaction)
    {

    }
    public void InteractItem(Item item)
    {

    }
}