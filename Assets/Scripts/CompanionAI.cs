using UnityEngine;
using System.Collections;

public class CompanionAI : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 2f;
    public Transform groundCheck;
    public Transform cliffCheckFront;
    public Transform cliffCheckBack;
    public Transform wallCheckFront;
    public Transform wallCheckBack;
    public LayerMask groundMask;

    private Rigidbody2D rb;
    private bool canMove = true;
    private bool paused = false;
    private bool isGrounded;
    private bool groundAhead;
    private bool wallAhead;

    public bool forward = true;
    private Transform forwardCliffCheck;
    private Transform forwardWallCheck;
    public float forwardCheckDistance = 0.3f;

    public float flipDelay;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        if (forward)
        {
            forwardCliffCheck = cliffCheckFront;
            forwardWallCheck = wallCheckFront;
        }
        else
        {
            forwardCliffCheck = cliffCheckBack;
            forwardWallCheck = wallCheckBack;
        }
    }

    void FixedUpdate()
    {
        // Check ground underfoot
        bool groundedNow = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundMask);

        // If previously in air but now grounded again → resume movement
        if (!isGrounded && groundedNow)
            canMove = true;

        isGrounded = groundedNow;

        if (canMove && !paused)
            if (forward) {
                rb.linearVelocity = new Vector2(moveSpeed, rb.linearVelocity.y);
            } else {
                rb.linearVelocity = new Vector2(-moveSpeed, rb.linearVelocity.y);
            }
        else
            rb.linearVelocity = new Vector2(0f, rb.linearVelocity.y);

        // Flip if about to walk off a ledge or a wall
        groundAhead = Physics2D.Raycast(forwardCliffCheck.position, Vector2.down, forwardCheckDistance, groundMask);
        if (forward)
            wallAhead = Physics2D.Raycast(forwardWallCheck.position, Vector2.right, forwardCheckDistance, groundMask);
        else
            wallAhead = Physics2D.Raycast(forwardWallCheck.position, Vector2.left, forwardCheckDistance, groundMask);

        if (isGrounded) {
            canMove = true;

            // Flip if running into a wall or cliff
            if (!groundAhead) {
                StartCoroutine(DelayActionFlip(flipDelay));
            }
            else if (wallAhead) {
                StartCoroutine(DelayActionFlip(flipDelay));
            }
        } else {
            canMove = false;
        }
    }

    private void Flip()
    {
        forward = !forward;

        if (forward)
        {
            forwardCliffCheck = cliffCheckFront;
            forwardWallCheck = wallCheckFront;
        }
        else
        {
            forwardCliffCheck = cliffCheckBack;
            forwardWallCheck = wallCheckBack;
        }
    }
    
    IEnumerator DelayActionFlip(float delayTime)
    {
        Flip();
        paused = true;

        // Wait for the specified delay time before continuing.
        yield return new WaitForSeconds(delayTime);
        paused = false;
    }
}
