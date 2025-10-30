using UnityEngine;

public class BridgeController : MonoBehaviour {
    public Animator animator;

    public void OnLeverToggled(bool state) {
        animator?.SetBool("Lowered", state);
    }
}
