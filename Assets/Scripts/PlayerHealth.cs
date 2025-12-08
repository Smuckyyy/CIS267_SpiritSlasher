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
    public float iFramesDuration = 1f;
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
    }

    void Die()
    {
        Debug.Log("Player Died");
        SceneManager.LoadScene("GameOver"); //This scene hasnt been created yet but once it is we can load it here
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

        SpriteRenderer sr = GetComponentInChildren<SpriteRenderer>();
        float elapsed = 0f;
        float flashInterval = 0.15f;

        while (elapsed < duration)
        {
            if (sr != null)
            sr.enabled = !sr.enabled;

            yield return new WaitForSeconds(flashInterval);
            elapsed += flashInterval;
        }

        if (sr != null)
        sr.enabled = true;

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
