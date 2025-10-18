using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] MousePointer mouse;
    Item[] items = new Item[8];
    public void SelectItem(int Index)
    {
        mouse.SelectInteractionItem(items[Index]);
    }

    public void AddItem(Item newItem)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] == null) items[i] = newItem;
        }
    }

    public void RemoveItem(Item removeItem)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] == removeItem) items[i] = null;
        }
    }
}
