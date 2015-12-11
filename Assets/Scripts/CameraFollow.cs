using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	public Transform target;
	public float smoothing = 10f;

	Vector3 offset, point;
    PlayerMovement pm;

    void Start()
	{
		offset = transform.position - target.position;
        pm = target.GetComponent<PlayerMovement>();
    }

	void FixedUpdate()
	{
        
        point = target.transform.position;
        if(pm.isGrounded) transform.LookAt(point);
        Vector3 targetCamPos;
        switch (pm.direction)
        {
            case 0: targetCamPos = target.position + new Vector3(0f, 3f, -4f); break;
            case 1: targetCamPos = target.position + new Vector3(-4f, 3f, 0f); break;
            case 2: targetCamPos = target.position + new Vector3(4f, 3f, 0f); break;
            default: targetCamPos = target.position + new Vector3(0f, 3f, 4f); break;
        }

        transform.position = Vector3.Lerp (transform.position, targetCamPos, smoothing * Time.deltaTime);
        //transform.rotation.SetEulerRotation(new Vector3(0, target.rotation.y));
        //transform.Rotate(Vector3.up, target.transform.rotation.eulerAngles.y);
        //transform.position = targetCamPos;
    }

    public void RotateLooking(float y)
    {
        //transform.RotateAround(point, new Vector3(0.0f, 1.0f, 0.0f), 90);
        //transform.LookAt(target);
        //transform.Translate(Vector3.right * Time.deltaTime);
    }

    private bool seemsEqual(float a, float b)
    {
        return (b - 1) < a && (b + 1) > a;
    }
}
