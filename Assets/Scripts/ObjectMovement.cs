using UnityEngine;
using System.Collections;

public class ObjectMovement : MonoBehaviour {
	
	public float speed = 1.5f;
	private Vector3 startPosition;

	
	// Use this for initialization
	void Start () 
	{
		if (CompareTag ("Teddybear")) {
			startPosition = transform.position;
		}
	}

	// Update is called once per frame
	void Update () {
		
		if (CompareTag ("Star")) {
			transform.Rotate (new Vector3 (0, 30, 0) * Time.deltaTime);
		} else if (CompareTag ("Medipack")){
			transform.Rotate (new Vector3 (30, 30, 0) * Time.deltaTime);
		}
		else if (CompareTag ("Teddybear")) {
			transform.position = new Vector3(transform.position.x, startPosition.y + Mathf.Sin(Time.time * speed)*0.3f, transform.position.z);
		}
	}
}