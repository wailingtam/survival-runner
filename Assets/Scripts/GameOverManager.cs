using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    public PlayerHealth playerHealth;
	private float restartDelay = 15f;


    Animator anim;
	float restartTimer;

    void Awake()
    {
        anim = GetComponent<Animator>();
        playerHealth = GameObject.Find("Player").GetComponent<PlayerHealth>();
    }


    void Update()
    {
        if (playerHealth.currentHealth <= 0)
        {
            anim.SetTrigger("GameOver");

			restartTimer += Time.deltaTime;

			if (restartTimer >= restartDelay)
			{
				Application.LoadLevel(Application.loadedLevel);
			}
        }
    }
}
