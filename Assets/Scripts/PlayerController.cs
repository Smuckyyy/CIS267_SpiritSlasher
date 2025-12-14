using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 8f;

    
    [Header("Jumping")]
    public float jumpForce = 12f;
    private bool canDoubleJump = false;
    private bool cantrippleJump = false;
    
    
    [Header("Ground Check")]
    public Transform groundCheck;
    public float checkRadius = 0.2f;
    public LayerMask theGround;
    private bool isGrounded;

    private Rigidbody2D rb;
    private float moveInput;
    private Animator anim;
    private SpriteRenderer sr;
    [Header("Atttcking")]
    public GameObject kunai;
    public GameObject slash;
    public GameObject shurikin;
    public float attackSpeed  = 0;
    private int numOfKunai = 3;
    private bool shurkinThrown = false;
    private float timepassed = 0;
    public float meleeRange = 2;
    // vertical offset to raise melee spawn toward player's torso
    public float meleeHeightOffset = 0.6f;
    // force applied to thrown projectiles (impulse)
    public float throwForce = 10f;
    public float rapidcooldown = 1f;
    public float rapidtimepassed = 0f;

    // unlocks
    private bool trippleUpgrade = false;
    private bool shurikinUpgrade = false;
    private bool rapidUpgrade = false;
    private bool israpid = false;

    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        sr = GetComponentInChildren<SpriteRenderer>();
        
        // Restore upgrades saved in GameManager so they persist between levels
        if (GameManager.instance != null)
        {
            trippleUpgrade = GameManager.instance.Gettriple();
            shurikinUpgrade = GameManager.instance.GetReturn();
            rapidUpgrade = GameManager.instance.GetRapid();
        }
    }

    // Update is called once per frame
    void Update()
    {
        moveInput = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump"))
        {
            HandleJump();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            melee();
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            throwShurikin();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            throwKunai();
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            rapid();
        }


            if (moveInput > 0)
            {
                sr.flipX = false; //Facing right
            }
            else if (moveInput < 0)
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

            if (isGrounded)
            {
                canDoubleJump = true;
                cantrippleJump = true;
            }
            timepassed += Time.fixedDeltaTime;
            rapidtimepassed += Time.fixedDeltaTime;
    }

        void HandleJump()
        {
            //Reset double jump when grounded
            if (isGrounded)
            {
                //Regular Jump
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            }
            else if (canDoubleJump)
            {
                //Double Jump
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
                canDoubleJump = false;

                //Double Jump Animation
                anim.SetTrigger("DoubleJump");
            }
            else if(cantrippleJump && trippleUpgrade)
            {
             rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
             cantrippleJump = false;
             anim.SetTrigger("DoubleJump");


             }
    }

        void melee()
        {
            // Allow attack only when cooldown elapsed
            if (timepassed > attackSpeed)
            {
                // Spawn strictly in the direction the player is facing (ignore cursor)
                float dirSign = (sr != null && sr.flipX) ? -1f : 1f;

                // Raise spawn position toward the middle of the body using meleeHeightOffset
                Vector3 spawnPos = transform.position + new Vector3(meleeRange * dirSign, meleeHeightOffset, 0f);
                GameObject instance = Instantiate(slash, spawnPos, Quaternion.identity);

                // Orient the slash to face horizontally in the facing direction.
                // Use localScale to mirror prefab if needed.
                instance.transform.localScale = new Vector3(Mathf.Abs(instance.transform.localScale.x) * dirSign, instance.transform.localScale.y, instance.transform.localScale.z);

                // Optionally flip sprite renderer to match facing direction
                var slashSr = instance.GetComponentInChildren<SpriteRenderer>();
                if (slashSr != null)
                {
                    slashSr.flipX = sr != null && sr.flipX;
                }

                // Destroy the slash after 0.5 seconds
                Destroy(instance, 0.5f);

                timepassed = 0f;
            }

        }
        void throwShurikin()
        {
            if (shurikinUpgrade)
            {
                // Only throw if not already thrown
                if (!shurkinThrown)
                {
                    float dirSign = (sr != null && sr.flipX) ? -1f : 1f;
                    Vector3 spawnPos = transform.position + new Vector3(meleeRange * dirSign, meleeHeightOffset, 0f);
                    GameObject instance = Instantiate(shurikin, spawnPos, Quaternion.identity);
                    var prb = instance.GetComponent<Rigidbody2D>();
                    if (prb != null)
                    {
                        prb.AddForce(new Vector2(dirSign * throwForce, 0f), ForceMode2D.Impulse);
                    }
                    shurkinThrown = true;
                }
            }

        }
        void throwKunai()
        
    
    

    
        {
            if (numOfKunai > 0)
            {
                float dirSign = (sr != null && sr.flipX) ? -1f : 1f;
                Vector3 spawnPos = transform.position + new Vector3(meleeRange * dirSign, meleeHeightOffset, 0f);
                GameObject instance = Instantiate(kunai, spawnPos, Quaternion.identity);
                var prb = instance.GetComponent<Rigidbody2D>();
                if (prb != null)
                {
                    prb.AddForce(new Vector2(dirSign * throwForce, 0f), ForceMode2D.Impulse);
                }
                numOfKunai -= 1;
            }

        }
        void rapid()
        {
            if (rapidUpgrade && !israpid && rapidcooldown < rapidtimepassed )
            {
                attackSpeed = 0.2f;
                moveSpeed = moveSpeed * 2f;
                israpid = true;
                rapidtimepassed = 0f;

        }

    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("PierceUpgrade"))
        {
            trippleUpgrade = true;
            if (GameManager.instance != null) GameManager.instance.Settriple();
            Destroy(collision.gameObject);
        }
        if(collision.gameObject.CompareTag("ShurikinUpgrade"))
        {
            shurikinUpgrade = true;
            if (GameManager.instance != null) GameManager.instance.SetReturn();
            Destroy(collision.gameObject);
        }
        if(collision.gameObject.CompareTag("RapidUpgrade"))
        {
            rapidUpgrade = true;
            if (GameManager.instance != null) GameManager.instance.SetRapid();
            Destroy(collision.gameObject);
        }
        if(collision.gameObject.CompareTag("PlayerShurikin"))
        {
            shurkinThrown = false;
            Destroy(collision.gameObject);
        }
        if(collision.gameObject.CompareTag("PlayerKunai"))
        {
            numOfKunai += 1;
            Destroy(collision.gameObject);
        }

    }


    // private void OnDrawGizmosSelected()
    // {
    //     //Visualize ground check
    //     Gizmos.color = Color.red;
    //     Gizmos.DrawWireSphere(groundCheck.position, checkRadius);
    // }
}
