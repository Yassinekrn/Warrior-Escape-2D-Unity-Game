using UnityEngine;

public class playerLife : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;
    private AudioSource audioSource;
    private AudioSource repeatSoundSource;
    private bool isRepeatSoundPlaying = false;

    public int maxHealth = 5;
    public int currentHealth;
    private bool isDead = false;

    public gameOver GameOverScreen;
    public healthBar healthBar;
    public ScoreDisplayScript scoreDisplayScript;

    public AudioClip soundOnRepeat;
    public int soundRepeatThreshold = 4;
    public float reducedVolume = 0.5f;
    public AudioClip hitSound;

    public AudioSource backgroundMusicAudioSource;
    public float reducedBackgroundMusicVolume = 0.5f;
    private float defaultBackgroundMusicVolume = 1f;

    private int damageTakenCount = 0;

    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();

        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

        repeatSoundSource = gameObject.AddComponent<AudioSource>();
        repeatSoundSource.clip = soundOnRepeat;
        repeatSoundSource.loop = true;
        repeatSoundSource.volume = reducedVolume;
        repeatSoundSource.Stop(); // Stop the sound initially

        defaultBackgroundMusicVolume = backgroundMusicAudioSource.volume;
    }

    private void Update()
    {
        if (currentHealth <= 0 && isDead == false)
        {
            stopRepeatSound();
            Die();
            isDead = true;
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

            // Decrement the damage counter
            if (damageTakenCount > 0)
            {
                damageTakenCount--;
            }

            // Stop the heartbeat sound if damage counter becomes 0 or 1
            if (damageTakenCount < soundRepeatThreshold && isRepeatSoundPlaying)
            {
                stopRepeatSound();
                ResetBackgroundMusicVolume();
            }
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
            PlayHitSound();
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

    private void takeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);

        damageTakenCount++;

        if (damageTakenCount == soundRepeatThreshold && !isRepeatSoundPlaying)
        {
            repeatSoundSource.Play();
            isRepeatSoundPlaying = true;
            ReduceBackgroundMusicVolume();
        }

        if (damageTakenCount < soundRepeatThreshold)
        {
            stopRepeatSound();
            ResetBackgroundMusicVolume();
        }

        if (currentHealth <= 0)
        {
            stopRepeatSound();
            ResetBackgroundMusicVolume();
        }
    }

    private void stopRepeatSound()
    {
        if (isRepeatSoundPlaying)
        {
            repeatSoundSource.Stop();
            isRepeatSoundPlaying = false;
        }
    }

    private void PlayHitSound()
    {
        audioSource.PlayOneShot(hitSound);
    }

    private void ReduceBackgroundMusicVolume()
    {
        if (backgroundMusicAudioSource != null)
        {
            backgroundMusicAudioSource.volume = reducedBackgroundMusicVolume;
        }
    }

    private void ResetBackgroundMusicVolume()
    {
        if (backgroundMusicAudioSource != null)
        {
            backgroundMusicAudioSource.volume = defaultBackgroundMusicVolume;
        }
    }
}
