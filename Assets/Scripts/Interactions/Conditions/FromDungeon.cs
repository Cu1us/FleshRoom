using UnityEngine;

public class FromDungeon: InteractCondition
{
    public override bool CanInteract()
    {
        return EventHandler.Instance.DungeonDoorUnlocked;
    }
    public void SetTrue()
    {
        EventHandler.Instance.JesterGreased = true;
    }
}
