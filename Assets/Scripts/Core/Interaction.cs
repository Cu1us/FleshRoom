using System;
using UnityEngine;
using UnityEngine.Events;
using Object = UnityEngine.Object;

[Serializable]
public class Interaction
{
    public InteractionType Type;
    public Object Handler;
}
[Serializable]
public class ItemInteraction
{
    public Item Item;
    public Object Handler;
}
[Serializable]
public class InteractionEvent : UnityEvent, IInteractionHandler
{
    public void Interact()
    {
        Invoke();
    }
}