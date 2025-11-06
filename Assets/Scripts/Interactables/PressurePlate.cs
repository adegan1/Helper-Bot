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

    public AudioSource audioSource;
    public AudioClip buttonOnSFX;
    public AudioClip buttonOffSFX;

    public bool oneTime;
    private int pressAmount = 0;

    private void Awake()
    {
        col = GetComponent<BoxCollider2D>();
        col.isTrigger = true;
        originalPosition = transform.localPosition;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (pressAmount < 1 || !oneTime)
        {
            // Check if the object can activate the plate
            if (((1 << other.gameObject.layer) & activatorMask) != 0)
            {
                pressAmount++;

                if (!isPressed)
                {
                    isPressed = true;
                    Press();
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (pressAmount < 1 || !oneTime)
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
    }

    private void Press()
    {
        // Play sound
        audioSource.clip = buttonOnSFX;
        audioSource.Play();

        // Visually move the plate down a bit
        transform.localPosition = originalPosition - new Vector3(0, depressAmount, 0);

        // Optionally trigger something
        Interact();
    }

    private void Release()
    {
        // Play sound
        audioSource.clip = buttonOffSFX;
        audioSource.Play();

        // Return button size
        transform.localPosition = originalPosition;

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
