using UnityEngine;

public class Crate : MonoBehaviour
{
    public Transform wallCheckFront;
    public Transform wallCheckBack;
    public float forwardCheckDistance = 0.3f;

    private int pushableLayer;
    private int nonPushableLayer;

    public LayerMask wallMask;

    void Start()
    {
        pushableLayer = LayerMask.NameToLayer("Pushable");
        nonPushableLayer = LayerMask.NameToLayer("Non-Pushable");
    }

    void Update()
    {
        bool wallFront = Physics2D.Raycast(wallCheckFront.position, Vector2.right, forwardCheckDistance, wallMask);
        bool wallBack = Physics2D.Raycast(wallCheckBack.position, Vector2.left, forwardCheckDistance, wallMask);

        if (wallFront || wallBack)
        {
            this.gameObject.layer = nonPushableLayer;
        }
        else if (!wallFront && !wallBack)
        {
            this.gameObject.layer = pushableLayer;
        }
    }
}
