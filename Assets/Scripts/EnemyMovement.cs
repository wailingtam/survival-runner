using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{
	public float speed = 4f;

    Transform player;
    PlayerHealth playerHealth;
    EnemyHealth enemyHealth;
    NavMeshAgent nav;
	Vector3 destination;
	PlayerMovement playerMovement;

	int direction;

    void Awake ()
    {
        player = GameObject.FindGameObjectWithTag ("Player").transform;
		playerHealth = player.GetComponent <PlayerHealth> ();
		playerMovement = player.GetComponent <PlayerMovement> ();
        enemyHealth = GetComponent <EnemyHealth> ();
		nav = GetComponent <NavMeshAgent> ();
		destination = player.position;
    }

    void Update ()
    {
		/*direction = playerMovement.direction;
		int distance;
		switch (direction) {
		case 0: distance = transform.position.z - player.position.z; break;
		case 1: distance = transform.position.x - player.position.x; break;
		case 2: distance = player.position.z - transform.position.z; break;
		default: distance = player.position.x - transform.position.x;*/
		//if (distance > 0f)
		if ((transform.position.z - player.position.z) > 0f) {
			if (enemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0) {
				nav.SetDestination (destination);
			} else {
				nav.enabled = false;
			}
		} else {
			if (enemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0) {
				nav.enabled = false;
				playerHealth.TakeDamage(1);
	        	Destroy (gameObject, 2f);
			}
		}
    }
}