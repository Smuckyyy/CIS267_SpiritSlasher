using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    [Header("Follow Settings")]
    public float speed = 3f;
    public float followRange = 8f;

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
            Vector2 direction = (player.position - transform.position).normalized;
            direction.y = 0; //This makes the enemy only move left and right

            rb.linearVelocity = new Vector2(direction.x * speed, rb.linearVelocity.y);
        }
        else
        {
            //Stop moving when out of range
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
        }
    }
}