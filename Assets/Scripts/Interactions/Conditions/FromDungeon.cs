using UnityEngine;

public class FromDungeon: IInteractCondition
{
    public bool CanInteract()
    {
        return EventHandler.Instance.DungeonDoorUnlocked;
    }
    public void SetTrue()
    {
        EventHandler.Instance.JesterGreased = true;
    }
}
