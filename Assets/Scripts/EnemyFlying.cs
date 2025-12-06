using UnityEngine;

public class EnemyFlying : MonoBehaviour
{
    [Header("Flying Settings")]
    public float speed = 4f;
    public float followRange = 10f;

    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        float distance = Vector2.Distance(transform.position, player.position);

        if (distance < followRange)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }
    }
}
