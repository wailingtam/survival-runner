using UnityEngine;
using System.Collections;

public class Plane : MonoBehaviour {

	
	Transform player;
	PlayerHealth playerHealth;
	EnemyHealth enemyHealth;
	
	public float speed = 1f;
	private Vector3 startPosition;

	void Awake ()
	{
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		playerHealth = player.GetComponent <PlayerHealth> ();
		enemyHealth = GetComponent <EnemyHealth> ();
		startPosition = transform.position;
	}
	
	void Update ()
	{
		if ((transform.position.z - player.position.z) > -2f) {
			if (enemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0) {
				var newRotation = Quaternion.LookRotation(-(transform.position - player.position), Vector3.forward);
				newRotation.x = 0.0f;
				newRotation.z = 0.0f;
				transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * 1f);
				//transform.LookAt (player.position);
				//transform.position = new Vector3 (transform.position.x, startPosition.y + Mathf.Sin (Time.time * speed) * 0.3f, transform.position.z);
			}
		}
		else {
			Destroy (gameObject, 2f);
		}
	}
}
