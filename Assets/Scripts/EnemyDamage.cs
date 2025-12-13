using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public int damageAmount = 1;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Debug.Log("Enemy hit " + collision.name);
            collision.GetComponent<PlayerHealth>().TakeDamage(damageAmount);
        }
    }
}
