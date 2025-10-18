using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class InventorySlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] Image Frame;

    [SerializeField] Sprite NormalFrame;
    [SerializeField] Sprite HoverFrame;
    [SerializeField] Sprite SelectedFrame;

    [SerializeField] Image Item;

    SelectionState state = SelectionState.Normal;


    void Reset()
    {
        Frame = GetComponent<Image>();
    }

    public void SetFrame(SelectionState state)
    {
        this.state = state;
        Frame.sprite = state switch
        {
            SelectionState.Hover => HoverFrame,
            SelectionState.Selected => SelectedFrame,
            _ => NormalFrame
        };
    }

    public void SetItemSprite(Sprite sprite)
    {
        Item.sprite = sprite;
        Item.enabled = sprite != null;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (state == SelectionState.Normal)
            SetFrame(SelectionState.Hover);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (state == SelectionState.Hover)
            SetFrame(SelectionState.Normal);
    }

    public enum SelectionState
    {
        Normal,
        Hover,
        Selected
    }
}
