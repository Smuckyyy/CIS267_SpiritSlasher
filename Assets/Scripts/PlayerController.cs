using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 8f;

    
    [Header("Jumping")]
    public float jumpForce = 12f;
    private bool canDoubleJump = false;
    
    
    [Header("Ground Check")]
    public Transform groundCheck;
    public float checkRadius = 0.2f;
    public LayerMask theGround;
    private bool isGrounded;

    private Rigidbody2D rb;
    private float moveInput;
    private Animator anim;
    private SpriteRenderer sr;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        sr = GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        moveInput = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump"))
        {
            HandleJump();
        }

        if(moveInput > 0)
        {
            sr.flipX = false; //Facing right
        }
        else if(moveInput < 0)
        {
            sr.flipX = true; //Facing left
        }

        //Walking animation
        //anim.SetBool("isRunning", moveInput != 0);
        //Jumping animation
        //anim.SetBool("isGrounded", isGrounded);
        //anim.SetFloat("verticalVelocity", rb.linearVelocity.y);
    }

    void FixedUpdate()
    {
        //Move player horizontally
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        //Check if theres ground
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, theGround);

        if(isGrounded)
        {
            canDoubleJump = true;
        }
    }

    void HandleJump()
    {
        //Reset double jump when grounded
        if(isGrounded)
        {
            //Regular Jump
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
        else if(canDoubleJump)
        {
            //Double Jump
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            canDoubleJump = false;

            //Double Jump Animation
            anim.SetTrigger("DoubleJump");
        }
    }
    
    // private void OnDrawGizmosSelected()
    // {
    //     //Visualize ground check
    //     Gizmos.color = Color.red;
    //     Gizmos.DrawWireSphere(groundCheck.position, checkRadius);
    // }
}
