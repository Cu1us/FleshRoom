using UnityEngine;

public class ManualInteractCondition : InteractCondition
{
    public bool AllowInteraction = true;
    public override bool CanInteract()
    {
        return AllowInteraction;
    }
}
