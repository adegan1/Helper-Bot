using UnityEngine;

public class Lever : MonoBehaviour, IInteractable
{
    public bool isOn;

    // Optional: visual feedback (e.g., rotation or sprite change)
    public Sprite leverOnSprite;
    public Sprite leverOffSprite;
    private SpriteRenderer sr;

    public GameObject target;

    public AudioSource audioSource;
    public AudioClip leverOnSFX;
    public AudioClip leverOffSFX;

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
        audioSource.clip = leverOnSFX;
        audioSource.Play();

        target.SetActive(true);
    }

    private void DeactivateMechanism()
    {
        audioSource.clip = leverOffSFX;
        audioSource.Play();

        target.SetActive(false);
    }
}
