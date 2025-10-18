using UnityEngine;

public class JesterGreased : IInteractCondition
{
    public bool CanInteract()
    {
        return EventHandler.Instance.JesterGreased;
    }

    public void SetTrue()
    {
        EventHandler.Instance.JesterGreased = true;
    }
}
