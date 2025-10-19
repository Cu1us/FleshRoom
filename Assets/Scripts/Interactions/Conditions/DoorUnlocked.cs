using UnityEngine;

public class DoorUnlockedCondition : InteractCondition
{
    public override bool CanInteract()
    {
        return EventHandler.Instance.DungeonDoorUnlocked;
    }
}
