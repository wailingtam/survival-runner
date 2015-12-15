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
        if(pm.isGrounded) transform.LookAt(point + new Vector3(0,2f,0));
        Vector3 targetCamPos;
        switch (pm.direction)
        {
            case 0: targetCamPos = target.position + new Vector3(0f, 5f, -5f); break;
            case 1: targetCamPos = target.position + new Vector3(-5f, 5f, 0f); break;
            case 2: targetCamPos = target.position + new Vector3(0f, 5f, 5f); break;
            default: targetCamPos = target.position + new Vector3(5f, 5f, 0f); break;
        }

        transform.position = Vector3.Lerp (transform.position, targetCamPos, smoothing * Time.deltaTime);
        //transform.rotation.SetEulerRotation(new Vector3(0, target.rotation.y));
        //transform.Rotate(Vector3.up, target.transform.rotation.eulerAngles.y);
        //transform.position = targetCamPos;
    }

    private bool seemsEqual(float a, float b)
    {
        return (b - 1) < a && (b + 1) > a;
    }
}
