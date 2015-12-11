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

    void Awake ()
    {
        player = GameObject.FindGameObjectWithTag ("Player").transform;
        playerHealth = player.GetComponent <PlayerHealth> ();
        enemyHealth = GetComponent <EnemyHealth> ();
		nav = GetComponent <NavMeshAgent> ();
		destination = player.position;
    }

    void Update ()
    {
		if ((transform.position.z - player.position.z) > 0f) {
			if (enemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0) {
				nav.SetDestination (destination);
			} else {
				nav.enabled = false;
			}
		} else {
			if (enemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0) {
				nav.enabled = false;
	        	Destroy (gameObject, 2f);
			}
		}
    }
}