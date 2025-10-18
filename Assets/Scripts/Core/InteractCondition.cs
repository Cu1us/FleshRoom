using UnityEngine;

public abstract class InteractCondition : MonoBehaviour, IInteractCondition
{
    public abstract bool CanInteract();
}

public interface IInteractCondition
{
    public bool CanInteract();
}