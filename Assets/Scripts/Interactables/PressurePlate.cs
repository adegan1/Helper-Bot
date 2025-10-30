using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class PressurePlate : MonoBehaviour, IInteractable
{
    [Header("Plate Settings")]
    public bool isPressed = false;
    public float depressAmount = 0.1f;
    public LayerMask activatorMask; // What can press the plate (e.g. Player, Companion)

    private Vector3 originalPosition;
    private BoxCollider2D col;

    public GameObject target;
    public bool invertActivation;

    private void Awake()
    {
        col = GetComponent<BoxCollider2D>();
        col.isTrigger = true;
        originalPosition = transform.localPosition;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object can activate the plate
        if (((1 << other.gameObject.layer) & activatorMask) != 0)
        {
            if (!isPressed)
            {
                isPressed = true;
                Press();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (((1 << other.gameObject.layer) & activatorMask) != 0)
        {
            if (isPressed)
            {
                isPressed = false;
                Release();
            }
        }
    }

    private void Press()
    {
        // Visually move the plate down a bit
        transform.localPosition = originalPosition - new Vector3(0, depressAmount, 0);
        Debug.Log($"{name} pressed!");

        // Optionally trigger something
        Interact();
    }

    private void Release()
    {
        transform.localPosition = originalPosition;
        Debug.Log($"{name} released!");

        if (target)
        {
            if (!invertActivation)
            {
                target.SetActive(false);
            }
            else
            {
                target.SetActive(true);
            }
        }
    }

    public void Interact()
    {
        // Called when something presses the plate (or manually by player)
        if (target)
        {
            if (!invertActivation)
            {
                target.SetActive(true);
            }
            else
            {
                target.SetActive(false);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(transform.position, transform.localScale);
    }
}
