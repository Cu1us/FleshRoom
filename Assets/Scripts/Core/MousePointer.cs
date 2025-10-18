using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(RectTransform))]
public class MousePointer : MonoBehaviour
{
    [SerializeField] RectTransform rect;
    [SerializeField] InputActionReference PointerAction;
    [SerializeField] InputActionReference ClickAction;
    [SerializeField] LayerMask InteractableLayers;

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
        if (TryRaycastForInteractable(out Interactable interactable, pointerPos))
        {

        }
    }
    void OnClick(InputAction.CallbackContext callbackContext)
    {
        Vector2 pointerPos = PointerAction.action.ReadValue<Vector2>();
        Vector2 worldPos = camera.ScreenToWorldPoint(pointerPos);
        if (TryRaycastForInteractable(out Interactable interactable, pointerPos))
        {
            if (SelectedItem != null)
            {
                EventHandler.Instance.PlayerChangeEvent?.Invoke(
                    interactable.transform.position.x,
                    0,
                    false,
                    () => interactable.InteractItem(SelectedItem)
                );
            }
            else if (SelectedInteraction != InteractionType.None)
            {
                EventHandler.Instance.PlayerChangeEvent?.Invoke(
                    interactable.transform.position.x,
                    0,
                    false,
                    () => interactable.Interact(SelectedInteraction)
                );
            }
        }
        else
        {
            EventHandler.Instance.PlayerChangeEvent?.Invoke(worldPos.x, 0, false, null);
        }
    }

    bool TryRaycastForInteractable(out Interactable interactable, Vector2 pointerPos)
    {
        Ray ray = camera.ScreenPointToRay(pointerPos);
        if (Physics.Raycast(ray, out RaycastHit hit, 100, InteractableLayers))
        {
            if (hit.collider.gameObject.TryGetComponent(out interactable))
            {
                return true;
            }
        }
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
            } + interactable.name;
            ContextUseHintLabel.enabled = true;
        }
    }

    public void SelectInteractionType(InteractionType interactionType)
    {
        SelectedInteraction = interactionType;
        SelectedItem = null;
    }
    public void SelectInteractionExamine() => SelectInteractionType(InteractionType.Examine);
    public void SelectInteractionInteract() => SelectInteractionType(InteractionType.Interact);
    public void SelectInteractionItem(Item item)
    {
        SelectedInteraction = InteractionType.None;
        SelectedItem = item;
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
