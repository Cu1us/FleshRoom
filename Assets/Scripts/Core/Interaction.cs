using System;
using UnityEngine;
using UnityEngine.Events;
using Object = UnityEngine.Object;

[Serializable]
public class Interaction
{
    public InteractionType Type;
    public InteractCondition Condition;
    public InteractionEvent<InteractionType> Handler;
}
[Serializable]
public class ItemInteraction
{
    public Item Type;
    public InteractCondition Condition;
    public InteractionEvent<Item> Handler;
}
[Serializable]
public class InteractionEvent<T> : UnityEvent<T>
{
    public void Interact(T type)
    {
        Invoke(type);
    }
}