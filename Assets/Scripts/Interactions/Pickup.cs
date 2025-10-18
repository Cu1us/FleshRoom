using UnityEngine;

public class Pickup : MonoBehaviour
{
    public void TakeItem(Item item)
    {
        EventHandler.Instance.ItemAddedEvent(item);
        this.gameObject.SetActive(false);
    }

    public void UseItem(Item item)
    {
        EventHandler.Instance.ItemRemovedEvent(item);
    }
}
