using UnityEngine;
using System.Collections;

public class Ghost : MonoBehaviour {

    private GameObject player;
    private Vector3 originalPosition;

	// Use this for initialization
	void Awake () {
        player = GameObject.Find("Player");
        originalPosition = transform.position;
	}

    void OnEnable()
    {
        transform.position = originalPosition;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        transform.LookAt(player.transform);

        transform.Translate(Time.deltaTime * Random.value, Time.deltaTime * Random.value, Time.deltaTime * Random.value); // move forward        
    }
}
