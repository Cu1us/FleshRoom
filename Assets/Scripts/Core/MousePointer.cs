using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(RectTransform))]
public class MousePointer : MonoBehaviour
{
    [SerializeField] RectTransform rect;
    [SerializeField] InputActionReference PointerAction;
    [SerializeField] LayerMask InteractableLayers;

    [SerializeField] TextMeshProUGUI ContextUseHintLabel;

    new Camera camera;

    void Reset()
    {
        rect = GetComponent<RectTransform>();
    }
    void Update()
    {
        if (camera == null)
            camera = Camera.main;
        Vector2 pointerPos = PointerAction.action.ReadValue<Vector2>();
        rect.position = pointerPos;
        if (TryRaycastForInteractable(out Interactable interactable))
        {
            
        }
    }
    
    bool TryRaycastForInteractable(out Interactable interactable)
    {
        Vector2 pointerPos = PointerAction.action.ReadValue<Vector2>();
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
}
