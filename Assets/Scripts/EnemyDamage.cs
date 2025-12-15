using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public int damageAmount = 1;
    public int Health = 3;
    public float timebetweenDamage = 1f;

    // time when this enemy can next take damage
    private float nextDamageTime = 0f;


    void OnTriggerEnter2D(Collider2D collision)
    {
        // Use a cooldown so repeated trigger contacts from the same lingering collider
        // don't apply damage multiple times in quick succession.
        if (Time.time < nextDamageTime)
            return;

        if(collision.CompareTag("Player"))
        {
            Debug.Log("Enemy hit " + collision.name);
            collision.GetComponent<PlayerHealth>().TakeDamage(damageAmount);
        }
        if(collision.CompareTag("Boss"))
        {
            collision.GetComponent<BossAI>().TakeDamage(damageAmount);
        }
        if(collision.CompareTag("PlayerSlash"))
        {
            Debug.Log("Player slash hit " + gameObject.name);
            Health -= 1;
            nextDamageTime = Time.time + timebetweenDamage;
            if(Health <= 0)
            {
                Destroy(gameObject);
            }
        }
        if(collision.CompareTag("PlayerKunai"))
        {
            Debug.Log("Player kunai hit " + gameObject.name);
            Health -= 1;
            nextDamageTime = Time.time + timebetweenDamage;
            if(Health <= 0)
            {
                Destroy(gameObject);
            }
        }
        if(collision.CompareTag("PlayerShurikin"))
        {
            Debug.Log("Player shurikin hit " + gameObject.name);
            Health -= 3;
            nextDamageTime = Time.time + timebetweenDamage;
            if(Health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
