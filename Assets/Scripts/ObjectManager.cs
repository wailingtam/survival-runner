using UnityEngine;
using System.Collections;

public class ObjectManager : MonoBehaviour {

	public PlayerHealth playerHealth;
	public GameObject pickup;
	public float spawnTime = 3f;
	public Transform[] spawnPoints;

	Vector3 distanceInbetween = new Vector3(0f, 0f, 1f);
	
	void Start ()
	{
		InvokeRepeating ("Spawn", spawnTime, spawnTime);
	}
	
	
	void Spawn ()
	{
		if(playerHealth.currentHealth <= 0f)
		{
			return;
		}
		
		int spawnPointIndex = Random.Range (0, spawnPoints.Length);

		if (pickup.CompareTag ("Star")) {
			int numInstances = Random.Range (3, 10);

			for (int i = 0; i < numInstances; i++) {
				Instantiate (pickup, spawnPoints [spawnPointIndex].position + distanceInbetween * i, spawnPoints [spawnPointIndex].rotation);
			}
		} else {
			Instantiate (pickup, spawnPoints [spawnPointIndex].position, spawnPoints [spawnPointIndex].rotation);
		}

	}
}
