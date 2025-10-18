using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

[RequireComponent(typeof(RectTransform))]
public class MousePointer : MonoBehaviour
{
    [SerializeField] RectTransform rect;
    [SerializeField] InputActionReference PointerAction;
    [SerializeField] InputActionReference ClickAction;
    [SerializeField] LayerMask InteractableLayers;

    public Action OnSelectionChanged;

    [SerializeField] TextMeshProUGUI ContextUseHintLabel;

    public InteractionType SelectedInteraction;
    public Item SelectedItem;

    new Camera camera;

    void Reset()
    {
        rect = GetComponent<RectTransform>();
    }
    void Start()
    {
        ClickAction.action.performed += OnClick;

    }
    void Update()
    {
        if (camera == null)
            camera = Camera.main;
        Vector2 pointerPos = PointerAction.action.ReadValue<Vector2>();
        rect.position = pointerPos;
        TryRaycastForInteractable(out Interactable interactable, pointerPos, out _);
        SetContextUseHintLabelText(interactable);
    }
    void OnClick(InputAction.CallbackContext callbackContext)
    {
        Vector2 pointerPos = PointerAction.action.ReadValue<Vector2>();
        Vector2 worldPos = camera.ScreenToWorldPoint(pointerPos);
        if (TryRaycastForInteractable(out Interactable interactable, pointerPos, out bool hitBackground))
        {
            if (hitBackground)
            {
                EventHandler.Instance.PlayerChangeEvent?.Invoke(worldPos.x, null);
                ClearInteraction();
                return;
            }
            if (SelectedItem != null)
            {
                Item type = SelectedItem;
                EventHandler.Instance.PlayerChangeEvent?.Invoke(
                    interactable.transform.position.x,
                    () => interactable.InteractItem(type)
                );
            }
            else if (SelectedInteraction != InteractionType.None)
            {
                InteractionType type = SelectedInteraction;
                EventHandler.Instance.PlayerChangeEvent?.Invoke(
                    interactable.transform.position.x,
                    () => interactable.Interact(type)
                );
            }
        }
        ClearInteraction();
    }

    bool TryRaycastForInteractable(out Interactable interactable, Vector2 pointerPos, out bool hitBackground)
    {
        Vector2 origin = camera.ScreenToWorldPoint(pointerPos);
        RaycastHit2D hit = Physics2D.Raycast(origin, new(0, 0), 100, InteractableLayers);
        if (hit.collider != null)
        {
            if (hit.collider.CompareTag("Background"))
            {
                hitBackground = true;
                interactable = null;
                return true;
            }
            if (hit.collider.gameObject.TryGetComponent(out interactable))
            {
                hitBackground = false;
                return true;
            }
        }
        hitBackground = false;
        interactable = null;
        return false;
    }

    void SetContextUseHintLabelText(Interactable interactable)
    {
        if (interactable == null)
        {
            ContextUseHintLabel.text = string.Empty;
            ContextUseHintLabel.enabled = false;
            return;
        }
        if (SelectedItem != null)
        {
            ContextUseHintLabel.text = $"Use {SelectedItem.name} on {interactable.Name}";
            ContextUseHintLabel.enabled = true;
        }
        else
        {
            ContextUseHintLabel.text = SelectedInteraction switch
            {
                InteractionType.Interact => "Interact with ",
                InteractionType.Examine => "Examine ",
                _ => ""
            } + interactable.Name;
            ContextUseHintLabel.enabled = true;
        }
    }

    public void SelectInteractionType(InteractionType interactionType)
    {
        SelectedInteraction = interactionType;
        SelectedItem = null;
        OnSelectionChanged?.Invoke();
    }
    public void SelectInteractionExamine() => SelectInteractionType(InteractionType.Examine);
    public void SelectInteractionInteract() => SelectInteractionType(InteractionType.Interact);
    public void ClearInteraction() => SelectInteractionItem(null);
    public void SelectInteractionItem(Item item)
    {
        SelectedInteraction = InteractionType.None;
        SelectedItem = item;
        OnSelectionChanged?.Invoke();
    }

    void OnEnable()
    {
        Cursor.visible = false;
    }
    void OnDisable()
    {
        Cursor.visible = true;
    }
}
