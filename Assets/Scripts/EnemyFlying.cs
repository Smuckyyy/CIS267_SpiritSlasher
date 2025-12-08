using UnityEngine;

public class EnemyFlying : MonoBehaviour
{
    [Header("Flying Settings")]
    public float speed = 4f;
    public float followRange = 10f;

    private Transform player;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void FixedUpdate()
    {
        //This is for if the player isnt found
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player")?.transform;
            return;
        }

        rb.WakeUp();

        float distance = Vector2.Distance(transform.position, player.position);

        if (distance < followRange)
        {
            //This makes the enemy fly towards the player in all directions
            Vector2 direction = (player.position - transform.position).normalized;
            rb.linearVelocity = direction * speed;
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
        }
    }
}
