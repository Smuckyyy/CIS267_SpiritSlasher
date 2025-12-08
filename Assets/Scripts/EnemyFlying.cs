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
            //This makes the enemy fly towards the player in all directions
            Vector2 direction = (player.position - transform.position).normalized;
            rb.velocity = direction * speed;
        }
        else
        {
            rb.velocity = Vector2.zero;
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
