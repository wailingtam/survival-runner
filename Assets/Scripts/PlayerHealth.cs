using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
	public int currentHealth;
	public Image heart1;
	public Image heart2;
	public Image heart3;
	
    /*public int startingHealth = 100;
    public int currentHealth;
    public Slider healthSlider;*/
    public Image damageImage;
    public AudioClip deathClip;
    public float flashSpeed = 5f;
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);


    Animator anim;
    AudioSource playerAudio;
    PlayerMovement playerMovement;
    PlayerShooting playerShooting;
    bool isDead;
    bool damaged;

	//int healingAmount = 20;

    void Awake ()
    {
        anim = GetComponent <Animator> ();
        playerAudio = GetComponent <AudioSource> ();
        playerMovement = GetComponent <PlayerMovement> ();
        playerShooting = GetComponentInChildren <PlayerShooting> ();
        //currentHealth = startingHealth;
		currentHealth = 3;
    }


    void Update ()
    {
        if(damaged)
        {
            damageImage.color = flashColour;
        }
        else
        {
            damageImage.color = Color.Lerp (damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        damaged = false;
    }


    public void TakeDamage (int amount)
    {
        damaged = true;

		currentHealth -= amount;

		switch (currentHealth) {
			case 2: heart3.enabled = false; break;
			case 1: heart2.enabled = false; break;
			case 0: heart1.enabled = false; break;
		}

        //currentHealth -= amount;

        //healthSlider.value = currentHealth;

        playerAudio.Play ();

        if(currentHealth <= 0 && !isDead)
        {
            Death ();
        }
    }


    void Death ()
    {
        isDead = true;

        playerShooting.DisableEffects ();

        anim.SetTrigger ("Die");

        playerAudio.clip = deathClip;
        playerAudio.Play ();

        playerMovement.enabled = false;
        playerShooting.enabled = false;
    }


    public void RestartLevel ()
    {
        Application.LoadLevel (Application.loadedLevel);
    }

	public void Heal ()
	{
		/*if (currentHealth < startingHealth) {
			if (startingHealth - currentHealth < healingAmount) {
				currentHealth = startingHealth;
			} else {
				currentHealth += healingAmount;
			}

			healthSlider.value = currentHealth;
		}*/
		if (currentHealth < 3) {
			currentHealth += 1;
		}
		
		switch (currentHealth) {
			case 3: heart3.enabled = true; break;
			case 2: heart2.enabled = true; break;
		}

	}
}
