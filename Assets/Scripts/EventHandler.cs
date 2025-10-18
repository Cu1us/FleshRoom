using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public delegate void PositionChangedEvent(float position, Action onComplete);
public delegate void AnimationActivation(int animationID);


public delegate void ItemEvent(Item item);

public class EventHandler : MonoBehaviour
{

    public static EventHandler Instance;
    private void Awake()
    {
        Instance = this;
    }

    
    public PositionChangedEvent PlayerChangeEvent;
    public ItemEvent ItemRemovedEvent;
}
