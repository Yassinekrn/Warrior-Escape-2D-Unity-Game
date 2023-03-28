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
        GameOverScreen.Setup();
    }

    void takeDamage (int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }
    
}
