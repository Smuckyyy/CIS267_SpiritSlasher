using UnityEngine;

public class BossAI : MonoBehaviour
{
    
    
        [Header("Stats")]
        public int maxHealth = 50;
        private int currentHealth;

        [Header("Movement")]
        public float moveSpeed = 2f;
        public float followRange = 10f;

        [Header("Attacks")]
        public GameObject projectilePrefab;        // what the boss shoots
        public Transform projectileSpawnPoint;     // where the shot comes from
        public float attackCooldown = 2f;
        private float attackTimer = 0f;

        private Transform player;

        void Start()
        {
            currentHealth = maxHealth;

            // find the player by tag
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }

        void Update()
        {
            float distance = Vector2.Distance(transform.position, player.position);

            // follow player when close enough
            if (distance < followRange)
            {
                transform.position = Vector2.MoveTowards(
                    transform.position,
                    player.position,
                    moveSpeed * Time.deltaTime
                );
            }

            // countdown before next attack
            attackTimer += Time.deltaTime;

            // attack if cooled down and player is in range
            if (attackTimer >= attackCooldown && distance < followRange)
            {
                Attack();
                attackTimer = 0f;
            }
        }

        void Attack()
        {
            // spawn a projectile
            if (projectilePrefab != null && projectileSpawnPoint != null)
            {
                Instantiate(projectilePrefab, projectileSpawnPoint.position, Quaternion.identity);
            }
        }

        // boss takes damage
        public void TakeDamage(int amount)
        {
            currentHealth -= amount;

            if (currentHealth <= 0)
            {
                Die();
            }
        }

        void Die()
        {
            // destroy boss
            Destroy(gameObject);
        }
    
}
