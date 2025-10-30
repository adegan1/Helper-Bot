using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteract : MonoBehaviour
{
    public float interactRange = 1.5f;
    public LayerMask interactableMask;
    public Transform interactionOrigin;

    private void OnInteract(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        Vector2 origin = interactionOrigin ? interactionOrigin.position : transform.position;

        // Check for any collider within range that’s on the interactable layer
        Collider2D col = Physics2D.OverlapCircle(origin, interactRange, interactableMask);

        if (col != null && col.TryGetComponent(out IInteractable interactable))
        {
            interactable.Interact();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Vector2 origin = interactionOrigin ? interactionOrigin.position : transform.position;
        Gizmos.DrawWireSphere(origin, interactRange);
    }
}
