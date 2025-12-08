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
        FindPlayer();
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        //Debug.Log("UPDATE RUNNING: " + gameObject.name);

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
            //Debug.Log("CHASING PLAYER");
            Vector2 direction = (player.position - transform.position).normalized;
            direction.y = 0; //This makes the enemy only move left and right

            rb.velocity = new Vector2(direction.x * speed, rb.velocity.y);
        }
        else
        {
            //Stop moving when out of range
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }

    void FindPlayer()
{
    GameObject p = GameObject.FindGameObjectWithTag("Player");

    if (p != null)
    {
        player = p.transform;
    }
    else
    {
        Debug.LogWarning("No player found! Trying again...");
        Invoke(nameof(FindPlayer), 0.5f);
    }
}
}