using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerLife : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;

    public int maxHealth = 3;
    public int currentHealth;
    private bool isDead = false;

    public gameOver GameOverScreen;

    public healthBar healthBar;

    public ScoreDisplayScript scoreDisplayScript;

    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        
    }

    private void Update()
    {
        if ( currentHealth <= 0 && isDead == false)
        {
            Die();
            isDead= true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("HealthBoost") && currentHealth < maxHealth)
        {
            // Increase the player's health by 1
            currentHealth++;

            // Destroy the health boost item
            Destroy(collision.gameObject);

            // Update the health bar
            healthBar.SetHealth(currentHealth);
        }
        else if (collision.gameObject.CompareTag("HealthBoost"))
        {
            // Destroy the health boost item
            Destroy(collision.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Trap") && currentHealth > 0)
        {
            takeDamage(1);
        }
        
    }
    private void Die()
    {
        rb.bodyType = RigidbodyType2D.Static;
        anim.SetTrigger("death");

        // Retrieve the timer value from the TimerScript component
        float timerValue = FindObjectOfType<Timerlogic>().StopTimer();

        // Display the score using the ScoreDisplayScript
        scoreDisplayScript.DisplayScore(timerValue);

        GameOverScreen.Setup();
    }

    void takeDamage (int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }
    
}
