using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public float speed = 6f;
	public float jumpHeight = 7f;
	bool isGrounded = true;

	Vector3 movement;
	Animator anim;
	Rigidbody playerRigidbody;
	int floorMask;
	float camRayLength = 100f; //Length of the ray we cast from the camera
	
	float timer;
	float timeBetweenPoints = 0.2f;
	PlayerHealth playerHealth;

	//Awake is called regarless the script is enable or not. Good to set up references
	void Awake()
	{
		floorMask = LayerMask.GetMask ("Floor");
		anim = GetComponent<Animator> ();
		playerRigidbody = GetComponent<Rigidbody> ();
		playerHealth = GetComponent <PlayerHealth> ();
	}

	void Update()
	{
		timer += Time.deltaTime;
		if (timer >= timeBetweenPoints) {
			timer = 0f;
			DisplayManager.score += 10;
		}
		DisplayManager.distance += 10 * Time.deltaTime;
	}

	void FixedUpdate()
	{
		//GetAxisRaw only get -1, 0 or 1 values
		float h = Input.GetAxisRaw ("Horizontal");
		//float v = Input.GetAxisRaw ("Vertical");
		float v = 1f;

		if (!isGrounded && playerRigidbody.transform.position.y <= 0.1f) {
			isGrounded = true;
		}

		if (Input.GetButtonDown ("Jump") && isGrounded) {
			playerRigidbody.velocity = new Vector3 (0, jumpHeight, 0);
			isGrounded = false;
		}

		Move (h, v);
		Turning ();
		Animating (h, v);
	}

	void Move(float h, float v)
	{
		movement.Set (h, 0f, v);

		movement = movement.normalized * speed * Time.deltaTime;

		playerRigidbody.MovePosition (transform.position + movement);

	}

	void Turning ()
	{
		Ray camRay = Camera.main.ScreenPointToRay (Input.mousePosition);

		RaycastHit floorHit;

		//if the ray has hit something...
		if (Physics.Raycast (camRay, out floorHit, camRayLength, floorMask)) 
		{
			Vector3 playerToMouse = floorHit.point - transform.position;
			playerToMouse.y = 0f;

			Quaternion newRotation = Quaternion.LookRotation (playerToMouse);
			playerRigidbody.MoveRotation (newRotation);
		}
	}

	void Animating (float h, float v)
	{
		if (isGrounded) {
			bool walking = h != 0f || v != 0f;
			anim.SetBool ("IsWalking", walking);
		} else {
			anim.SetBool ("IsWalking", false);
		}
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.gameObject.CompareTag ("Star")) {
			Destroy (other.gameObject);
			DisplayManager.stars += 1;
			DisplayManager.score += 100;
		} 
		else if (other.gameObject.CompareTag ("Medipack")) {
			Destroy (other.gameObject);
			playerHealth.Heal();
		}
		else if (other.gameObject.CompareTag ("Teddybear")) {
			Destroy (other.gameObject);
			DisplayManager.teddies += 1;
			DisplayManager.score += 300;
		} 
	}
}
