using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private GameObject pcamera, plight;
    private float speed = 2f;
	private float jumpHeight = 7f;
	public bool isGrounded = true;
    public int direction = 0;
    //int[] directions = new int[4] { 0, 1, 2, 3 };


    Collider turningZone;
    private bool hasTurned = false;

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
        pcamera = GameObject.Find("MainCamera");
        plight = GameObject.Find("DirectionalLight");

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
        //todo switch estats i després comprobar inputs
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

        Move(h, v);
        //Turning();
        //Animating(h, v);

        if (Input.GetKeyDown(KeyCode.E) && turningZone != null && !hasTurned && turningZone.transform.rotation.y < 180)
        {
            //If I press E to turn AND I can turn AND I am not in the process AND the rotation is towards right
            Debug.Log(transform.rotation);
            transform.Rotate(Vector3.up, 90);
            Debug.Log(transform.rotation);
             var pf = pcamera.GetComponent<CameraFollow>();
             pf.RotateLooking(30);
            direction = (direction + 1) % 4;
            //Debug.Log(pcamera.transform.rotation);
            //pcamera.transform.Rotate(0,30,0);
            //Debug.Log(pcamera.transform.rotation);
            //pcamera.transform.Rotate(Vector3.forward, -13f);
            //plight.transform.Rotate(Vector3.up, 30);
            hasTurned = true;
        }
/*
        if (Input.GetKeyDown(KeyCode.Q) && turningZone != null && !hasTurned && turningZone.transform.rotation.y > 180)
        {
            Vector3 v3 = new Vector3(0, 270, 0);
            transform.Rotate(v3);
            pcamera.transform.Rotate(v3);
            plight.transform.Rotate(v3);
            hasTurned = true;
        }
*/

	}

	void Move(float h, float v)
	{
        switch (direction)
        {
            case 0: movement.Set(h, 0f, v); break;
            case 1: movement.Set(v, 0f, h); break;
        }
		

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
        else if (other.gameObject.CompareTag("TurningZone"))
        {
            turningZone = other;
            hasTurned = false;
        }
	}

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("TurningZone"))
        {
            turningZone = null;
        }
    }
}
