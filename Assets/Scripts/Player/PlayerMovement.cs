using System.Threading.Tasks;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float playerJumpForce;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Collider2D healthCollider, movementCollider;
    [SerializeField] bool isGrounded, hasDoubleJumped, canGoDown;
    [SerializeField] LayerMask whatIsGround;
    [SerializeField] float yHeightCutoff;

    [SerializeField] AudioSource source;
    [SerializeField] AudioClip jumpClip;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Jumping();
        CheckHeight();
        GoDown();
    }

    void CheckHeight()
    {
        float playerYPos = transform.position.y;

        if (playerYPos > yHeightCutoff && isGrounded) canGoDown = true;
        else canGoDown = false;
    }

    void GoDown()
    {
        if (canGoDown && Input.GetKey(KeyCode.S) && Input.GetKeyDown(KeyCode.Space))  
        {
            movementCollider.enabled = false;
            canGoDown = false;
            isGrounded = false;
            WaitColliderRestart(501);
            return;
        }
    }


    async void WaitColliderRestart(int waitTime)
    {
        await Task.Delay(waitTime);
        movementCollider.enabled = true;
    }

    void Jumping()
    {
        if (isGrounded && !Input.GetKey(KeyCode.S) && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(new(0, playerJumpForce), ForceMode2D.Impulse);
            isGrounded = false;
            source.PlayOneShot(jumpClip);
        }

        if(!isGrounded && !hasDoubleJumped && !Input.GetKeyDown(KeyCode.S) && Input.GetKeyDown(KeyCode.Space))
        {
            rb.linearVelocity = new(0,0);
            rb.AddForce(new(0, playerJumpForce), ForceMode2D.Impulse);
            hasDoubleJumped = true;
            source.PlayOneShot(jumpClip);
        }
    }

    private void FixedUpdate()
    {
        CheckGround();
    }

    void CheckGround()
    {
        if(Physics2D.Raycast(transform.position, -transform.up, 1f, whatIsGround))
        {
            isGrounded = true;
            hasDoubleJumped = false;

        }
    }

    public void SetHealthCollider(bool state)
    {
        healthCollider.enabled = state;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, -transform.up * 1f);
    }
}