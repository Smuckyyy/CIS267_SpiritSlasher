using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public int damageAmount = 1;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerHealth>().TakeDamage(damageAmount);
        }
    }
}
