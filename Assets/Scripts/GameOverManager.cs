using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    private PlayerHealth playerHealth;
	public float restartDelay = 5f;


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
