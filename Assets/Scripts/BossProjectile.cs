using UnityEngine;

public class BossProjectile : MonoBehaviour
{
    public float speed = 6f;
    public int damage = 1;
    public float lifeTime = 4f;

    private Vector2 direction;

    void Start()
    {
        //Find player and shoot at them
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            direction = (player.transform.position - transform.position).normalized;
        }

        //Destroy the projectile
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        //If it hits the player deal damage
        if (collision.CompareTag("Player"))
        {
            PlayerHealth player = collision.GetComponent<PlayerHealth>();
            if (player != null)
            {
                player.TakeDamage(damage);
            }

            Destroy(gameObject);
        }

        //If the projectile hits the ground or platforms it disappears
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            Destroy(gameObject);
        }
    }
}