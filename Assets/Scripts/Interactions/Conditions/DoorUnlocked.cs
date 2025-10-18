using UnityEngine;

public class DoorUnlockedCondition : IInteractCondition
{
    public bool CanInteract()
    {
        return EventHandler.Instance.DungeonDoorUnlocked;
    }
}
