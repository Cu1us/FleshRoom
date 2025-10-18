using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [SerializeField] GameObject[] objects;
    [SerializeField] MousePointer mouse;
    Item[] items = new Item[8];

    private void Start()
    {
        EventHandler.Instance.ItemRemovedEvent += RemoveItem;
        EventHandler.Instance.ItemAddedEvent += AddItem;
        for (int i = 0; i < objects.Length; i++) 
        {
            objects[i].SetActive(false);
        }

    }
    public void SelectItem(int Index)
    {
        mouse.SelectInteractionItem(items[Index]);
    }

    public void AddItem(Item newItem)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] == null)
            {
                items[i] = newItem;
                objects[i].SetActive(true);
                objects[i].GetComponent<Image>().sprite = newItem.Icon;
                return;
            }
        }
    }

    public void RemoveItem(Item removeItem)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] == removeItem)
            {
                items[i] = null;
                objects[i].SetActive(false);
                return;
            }
        }
    }
}
