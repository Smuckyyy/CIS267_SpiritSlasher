using UnityEngine;

public class EnemyFlying : MonoBehaviour
{
    [Header("Flying Settings")]
    public float hoverHeight = 0.5f;
    public float hoverSpeed = 3f;
    public float chaseSpeed = 2f;
    public float chaseRange = 6f;

    private float startY;
    private Transform player;

    void Start()
    {
        //Set starting Y position for hovering
        startY = transform.position.y;

        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        //Hover motion
        float newY = startY + Mathf.Sin(Time.time * hoverSpeed) * hoverHeight;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);

        //Chase player if in range
        float distance = Vector2.Distance(transform.position, player.position);

        if (distance < chaseRange)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(player.position.x, newY), chaseSpeed * Time.deltaTime);
        }
    }
}
