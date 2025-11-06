using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GoalTrigger : MonoBehaviour {
    public string sceneName;

    public float delayTime;
    public GameObject beacon;
    public float beaconSpeed;
    private bool complete = false;

    public AudioSource audioSource;
    public AudioClip winSFX;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Companion"))
        {
            complete = true;

            audioSource.clip = winSFX;
            audioSource.Play();

            StartCoroutine(DelayAction(delayTime));
        }
    }

    private void Update()
    {
        if (complete)
        {
            beacon.transform.localScale += new Vector3(0, beaconSpeed, 0);
        }
    }
    
    IEnumerator DelayAction(float delayTime)
    {
        // Wait for the specified delay time before continuing.
        yield return new WaitForSeconds(delayTime);
        SceneManager.LoadScene(sceneName); // Restart or load next
    }
}
