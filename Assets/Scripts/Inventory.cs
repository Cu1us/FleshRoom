using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [SerializeField] InventorySlot interactSlot;
    [SerializeField] InventorySlot examineSlot;
    [SerializeField] InventorySlot[] slots;
    [SerializeField] MousePointer mouse;
    Item[] items = new Item[8];

    private void Start()
    {
        EventHandler.Instance.ItemRemovedEvent += RemoveItem;
        EventHandler.Instance.ItemAddedEvent += AddItem;
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].SetItemSprite(null);
        }
        mouse.OnSelectionChanged += OnMouseSelectionChange;
    }
    public void SelectItem(int Index)
    {
        mouse.SelectInteractionItem(items[Index]);
        DeselectAllSlots();
        slots[Index].SetFrame(InventorySlot.SelectionState.Selected); // Mark slot at Index as selected
    }

    void OnMouseSelectionChange()
    {
        if (mouse.SelectedItem == null)
        {
            DeselectAllSlots();
            switch (mouse.SelectedInteraction)
            {
                case InteractionType.Interact:
                    interactSlot.SetFrame(InventorySlot.SelectionState.Selected);
                    break;
                case InteractionType.Examine:
                    examineSlot.SetFrame(InventorySlot.SelectionState.Selected);
                    break;
            }
        }
    }

    void DeselectAllSlots()
    {
        foreach (InventorySlot slot in slots)
        {
            slot.SetFrame(InventorySlot.SelectionState.Normal);
        }
        interactSlot.SetFrame(InventorySlot.SelectionState.Normal);
        examineSlot.SetFrame(InventorySlot.SelectionState.Normal);
    }

    public void AddItem(Item newItem)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] == null)
            {
                items[i] = newItem;
                slots[i].SetItemSprite(newItem.Icon);
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
                slots[i].SetItemSprite(null);
                return;
            }
        }
    }
}
