using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{
	public float speed; //In UI

    Transform player;
    PlayerHealth playerHealth;
    EnemyHealth enemyHealth;
    //NavMeshAgent nav;
	//Vector3 destination;
	PlayerMovement playerMovement;
	bool damaged;

	int direction;
	Vector3 movement;
	Rigidbody enemyRigidbody;

    void Awake ()
    {
        player = GameObject.FindGameObjectWithTag ("Player").transform;
		playerHealth = player.GetComponent <PlayerHealth> ();
		playerMovement = player.GetComponent <PlayerMovement> ();
        enemyHealth = GetComponent <EnemyHealth> ();
		enemyRigidbody = GetComponent <Rigidbody> ();
		//nav = GetComponent <NavMeshAgent> ();
		//destination = player.position;
    }

    void Update ()
    {
		direction = playerMovement.direction;
		float distance;
		switch (direction) {
			case 0:
				distance = transform.position.z - player.position.z;
				break;
			case 1:
				distance = transform.position.x - player.position.x;
				break;
			case 2:
				distance = player.position.z - transform.position.z;
				break;
			default:
				distance = player.position.x - transform.position.x;
			break;
		}

		//if ((transform.position.z - player.position.z) > 0f) {
			if (enemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0) {
				//nav.SetDestination (destination);
				switch (direction)
				{
					case 0: movement.Set(0f, 0f, 1f); break;
					case 1: movement.Set(1f, 0f, 0f); break;
					case 2: movement.Set(0f, 0f, -1f); break;
					default: movement.Set(-1f, 0f, 0f); break;
				}
				movement = movement * speed;
				enemyRigidbody.MovePosition (transform.position - movement);
			} /*else {
				nav.enabled = false;
			}*/

			if (distance <= 0f && enemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0 && !damaged) {
				damaged = true;
				//nav.enabled = false;
				playerHealth.TakeDamage(1);
	        	Destroy (gameObject, 1.5f);
			}
		}
}