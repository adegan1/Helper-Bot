using UnityEngine;
using UnityEngine.SceneManagement;

public class GoalTrigger : MonoBehaviour {
    public string sceneName;

    void OnTriggerEnter2D(Collider2D col) {
        if (col.CompareTag("Companion"))
            SceneManager.LoadScene(sceneName); // Restart or load next
    }
}
