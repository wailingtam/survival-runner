using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 6f; //How fast player is

    Vector3 movement;  //Store movement applying to player
    Animator anim;  //Animation
    Rigidbody playerRigidbody;
    int floorMask;  //layer id of the floor for raycasting
    float camRayLength = 100; //Length of the ray of the camera

    void Awake()  //Like Start() function but this gets called regardless the script is enable or not. Setting up references.
    {
        floorMask = LayerMask.GetMask("Floor"); //Get the layer floor
        anim = GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate() //Update before physics, so for physics
    {
        float h = Input.GetAxisRaw("Horizontal"); //standard GetAxis gets values from -1 to 1, GetAxisRaw gets just 3 values: -1, 0, 1. (Horizontal is default unity name and represents: A key, D key)
        float v = Input.GetAxisRaw("Vertical");
        Move(h, v);
        Turning();
        Animating(h, v);
    }

    private void Move(float h, float v)
    {
        movement.Set(h, 0f, v);
        //If we move diagonal:
        movement = movement.normalized * speed * Time.deltaTime;

        playerRigidbody.MovePosition(transform.position + movement);
    }

    private void Turning() //We turn the character wheter the mouse is pointing at
    {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);  //We need to check the called raycast (video 2)
        RaycastHit floorHit;

        if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
        {
            Vector3 playerToMouse = floorHit.point - transform.position;  //We generate a vector using 2 points
            playerToMouse.y = 0f;  //We just want to use x and z components

            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);   //We get the rotation of the vector
            playerRigidbody.MoveRotation(newRotation);  //We apply it to the character
        }
    }

    private void Animating(float h, float v)
    {
        bool walking = h != 0f || v != 0f;
        anim.SetBool("IsWalking", walking); //If for this frame the h or v are different from 0 (-1 or 1) let's set the boolean of walking to the animation
        //In onenote is the picture where this boolean is written in animation and used, all by Unity's interface


    }
}
