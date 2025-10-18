using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public delegate void PositionChangedEvent(float position, int animationID, bool right, Action onComplete);

public class EventHandler : MonoBehaviour
{

    public static EventHandler Instance;
    private void Awake()
    {
        Instance = this;
    }

    public PositionChangedEvent PlayerChangeEvent;
}
