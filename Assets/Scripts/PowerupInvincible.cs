using UnityEngine;

public class PowerupInvincible : MonoBehaviour
{
    public float duration = 3f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerHealth player = collision.GetComponent<PlayerHealth>();
            
            if (player != null)
            {
                
                player.ActivateInvincibility(duration);
            }

            Destroy(gameObject);
        }
    }
}