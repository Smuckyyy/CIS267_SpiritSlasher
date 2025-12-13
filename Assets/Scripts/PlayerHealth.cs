using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 3;
    public int currentHealth;

    [Header ("Health UI")]
    public Image[] hearts; //This is where 3 hearts will go
    public Sprite fullHeart;
    public Sprite emptyHeart;

    [Header ("I-Frames")]
    public float iFramesDuration = 3f;
    private bool isInvincible = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();    
    }

    public void TakeDamage(int amount)
    {
        if(isInvincible)
        {
            return;
        }

        currentHealth -= amount;

        if(currentHealth <= 0)
        {
            currentHealth = 0;
            UpdateHealthUI();
            Die();
            return;
        }

        UpdateHealthUI();

        ActivateInvincibility(iFramesDuration);
    }

    void Die()
    {
        Debug.Log("Player Died");
        SceneManager.LoadScene("GameOver");
    }

    void UpdateHealthUI()
    {
        if (hearts == null || hearts.Length == 0)
        {
            return;
        }
        
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < currentHealth)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }
                
        }
    }

    public void ActivateInvincibility(float duration)
    {
        StartCoroutine(InvincibilityForDuration(duration));
    }

    public System.Collections.IEnumerator InvincibilityForDuration(float duration)
    {
        isInvincible = true;

        //This finds the players sprite and makes it flash for a short amount of time
        SpriteRenderer player = GetComponentInChildren<SpriteRenderer>();
        float elapsed = 0f;
        float flashInterval = 0.15f;

        while (elapsed < duration)
        {
            if (player != null)
            player.enabled = !player.enabled;

            yield return new WaitForSeconds(flashInterval);
            elapsed += flashInterval;
        }

        if (player != null)
        player.enabled = true;

        isInvincible = false;
    }


    void Update()
    {
        //For testing the HUD
        if(Input.GetKeyDown(KeyCode.H))
        {
            TakeDamage(1);
        }
    }
}
