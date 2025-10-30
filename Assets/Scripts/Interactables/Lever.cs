using UnityEngine;

public class Lever : MonoBehaviour, IInteractable
{
    public bool isOn;

    // Optional: visual feedback (e.g., rotation or sprite change)
    public Sprite leverOnSprite;
    public Sprite leverOffSprite;
    private SpriteRenderer sr;

    public GameObject target;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        UpdateLeverVisual();
    }

    // This function is called when the player interacts with the lever
    public void Interact()
    {
        isOn = !isOn; // toggle state
        UpdateLeverVisual();

        Debug.Log($"Lever toggled! New state: {isOn}");

        // You can trigger any function you want here
        if (isOn)
            ActivateMechanism();
        else
            DeactivateMechanism();
    }

    private void UpdateLeverVisual()
    {
        if (sr == null) return;
        sr.sprite = isOn ? leverOnSprite : leverOffSprite;
    }

    private void ActivateMechanism()
    {
        // Example: activate bridge, open door, power machine
        Debug.Log("Mechanism activated!");
        target.SetActive(true);
    }

    private void DeactivateMechanism()
    {
        Debug.Log("Mechanism deactivated!");
        target.SetActive(false);
    }
}
