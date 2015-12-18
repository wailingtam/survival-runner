using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private GameObject pcamera, plight;
    private float speed = 8f;
	private float jumpHeight = 15f;
    private float MARGIN = 8f;
    public bool isGrounded = true;
    public int direction = 0;
    public float v = 1f;

    public float smoothTime = 0.5f;
    public Vector3 velocity = Vector3.zero;
    Vector3 turnPosition;
    enum Lane{left, right, middle}
    Lane actualLane = Lane.middle;
    bool disableHorizontalAxis = false;
    //int[] directions = new int[4] { 0, 1, 2, 3 };


    Collider turningZone;
    private bool hasTurned = false;

	Vector3 movement;
	Animator anim;
    private float playerVerticalSize;
    Rigidbody playerRigidbody;
	int floorMask;
	//float camRayLength = 100f; //Length of the ray we cast from the camera
	
	float timer;
	float timeBetweenPoints = 0.2f;
	PlayerHealth playerHealth;

	AudioSource starAudio;


	//Awake is called regarless the script is enable or not. Good to set up references
	void Awake()
	{
        int platformLayer = LayerMask.NameToLayer("Floor");
        floorMask = 1 << platformLayer;
        anim = GetComponent<Animator> ();
		playerRigidbody = GetComponent<Rigidbody> ();
		playerHealth = GetComponent <PlayerHealth> ();
        pcamera = GameObject.Find("MainCamera");
        plight = GameObject.Find("DirectionalLight");
		playerVerticalSize = (float) gameObject.GetComponent<CapsuleCollider>().bounds.size.y;

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

        Vector3 positionToMeasure = transform.position + new Vector3(0, 0.1f, 0);
        bool isGrounded = Physics.Raycast(positionToMeasure, Vector3.down, 0.2f , floorMask);
        bool fallingToEmptyness = positionToMeasure.y < -5f && !Physics.Raycast(positionToMeasure, Vector3.down, 100f, floorMask);
        bool crashedInWall = (Physics.Raycast(positionToMeasure, Vector3.left, 0.2f, floorMask)
                            || Physics.Raycast(positionToMeasure, Vector3.right, 0.2f, floorMask)
                            || Physics.Raycast(positionToMeasure, Vector3.forward, 0.2f, floorMask)
                            || Physics.Raycast(positionToMeasure, Vector3.back, 0.2f, floorMask))
                            && !Physics.Raycast(positionToMeasure, Vector3.down, 100f, floorMask);
        if (crashedInWall) v = 0; //Let's let gravity to do it's job
        if (fallingToEmptyness) playerHealth.TakeDamage(playerHealth.currentHealth);
        if (Input.GetKeyDown(KeyCode.W) && isGrounded) playerRigidbody.velocity = new Vector3(0, jumpHeight, 0);

        if (turningZone != null && !hasTurned)
        {
            if (Input.GetKey(KeyCode.D) && turningZone.gameObject.CompareTag("TurningZoneRight"))
            {
                transform.Rotate(Vector3.up, 90);
                direction = (direction + 1) % 4;
                hasTurned = true;
            }
            else if(Input.GetKey(KeyCode.A) && turningZone.gameObject.CompareTag("TurningZoneLeft")){
                transform.Rotate(Vector3.up, -90);
                direction = (direction - 1) % 4;
                hasTurned = true;

            }
            if (hasTurned) // Do common stuff
            {
                Debug.Log("Turning!");
                transform.position = turningZone.transform.position;
                velocity = Vector3.zero;
            }
            
        }
        //Turning();
        Animating(v);
        Move(Input.GetKey(KeyCode.A), Input.GetKey(KeyCode.D), v);
    }

	void Move(bool a, bool d, float v)
	{
        float marginToDo = 0;
        if (disableHorizontalAxis) actualLane = Lane.middle;
        else
        {
            if (a && !d && actualLane != Lane.left)
            {
                marginToDo = -MARGIN;
                actualLane = Lane.left;
            }
            else if (d && !a && actualLane != Lane.right)
            {
                marginToDo = MARGIN;
                actualLane = Lane.right;
            }
            else if (actualLane != Lane.middle)
            {
                marginToDo = actualLane == Lane.left ? MARGIN : -MARGIN;
                actualLane = Lane.middle;
            }
        }
        switch (direction)
        {
            case 0: movement.Set(marginToDo, 0f, v); break;
            case 1: movement.Set(v, 0f, -marginToDo); break;
            case 2: movement.Set(-marginToDo, 0f, -v); break;
            default: movement.Set(-v, 0f, marginToDo); break;
        }
		movement = movement * speed;
        playerRigidbody.MovePosition (Vector3.SmoothDamp(transform.position, transform.position + movement, ref velocity, smoothTime));

    }

	/*void Turning ()
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
	}*/

	void Animating (float v)
	{
		if (isGrounded) {
			anim.SetBool ("IsWalking", true);
		} else {
			anim.SetBool ("IsWalking", false);
		}
	}

	void OnTriggerEnter (Collider other)
	{

        if (other.gameObject.CompareTag("Star"))
        {
            starAudio = other.gameObject.GetComponent<AudioSource>();
            starAudio.Play();
            other.gameObject.GetComponent<MeshRenderer>().enabled = false;
            Destroy(other.gameObject, 0.5f);
            DisplayManager.stars += 1;
            DisplayManager.score += 100;
        }
        else if (other.gameObject.CompareTag("Medipack"))
        {
            Destroy(other.gameObject);
            playerHealth.Heal();
        }
        else if (other.gameObject.CompareTag("Teddybear"))
        {
            Destroy(other.gameObject);
            DisplayManager.teddies += 1;
            DisplayManager.score += 300;
        }
        else if (other.gameObject.CompareTag("LowObstacle"))
        {
            playerHealth.TakeDamage(1);
        }
        else if (other.gameObject.CompareTag("HighObstacle"))
        {
            playerHealth.TakeDamage(3);
        }
        else if (other.gameObject.CompareTag("TurningZoneLeft") || other.gameObject.CompareTag("TurningZoneRight"))
        {
            disableHorizontalAxis = true;
            turningZone = other;
            hasTurned = false;
            turnPosition = other.gameObject.transform.position - (other.gameObject.transform.position - transform.position) / 4;
            Debug.Log("turning zone in");
        }
	}

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("TurningZoneLeft") || other.gameObject.CompareTag("TurningZoneRight"))
        {
            Debug.Log("turning zone out");
            disableHorizontalAxis = false;
            turningZone = null;
        }
    }
}
