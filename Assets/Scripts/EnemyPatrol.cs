using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [Header("Patrol Points")]
    public Transform pointA;
    public Transform pointB;
    public float speed = 2f;

    private Transform target;

    void Start()
    {
        target = pointB;
    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, target.position) < 0.2f)
        {
            //This will make the enemy walk back and forth forever
            target = (target == pointA) ? pointB : pointA;
        }
    }
}