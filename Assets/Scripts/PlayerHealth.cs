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
        //If the player is damaged, the iframes will start for however many frames we set so the player cant be damaged immediately again
        StartCoroutine(InvincibilityFlash());
    }

    void Die()
    {
        Debug.Log("Player Died");
        SceneManager.LoadScene("GameOver"); //This scene hasnt been created yet but once it is we can load it here
    }

    void UpdateHealthUI()
    {
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

    System.Collections.IEnumerator InvincibilityFlash()
    {
        isInvincible = true;

        //This will make the player flash during the i-frames (after being hit)
        yield return new WaitForSeconds(iFramesDuration);

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
