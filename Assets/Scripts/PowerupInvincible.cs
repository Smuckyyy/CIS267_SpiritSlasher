using UnityEngine;

public class PowerupInvincible : MonoBehaviour
{
    public float duration = 3f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerHealth ph = collision.GetComponent<PlayerHealth>();
            
            if (ph != null)
            {
                ph.ActivateInvincibility(duration);
            }

            Destroy(gameObject);
        }
    }
}