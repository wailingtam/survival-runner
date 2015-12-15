using UnityEngine;
using System.Collections;

public class ZoneMain : MonoBehaviour {

    private Transform zoneOutPos, zoneInPos;

	// Use this for initialization
	void Awake () {
        zoneOutPos = transform.Find("ZoneOutPos");
        zoneInPos = transform.Find("ZoneInPos");
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    public Transform GetOutPos()
    {
        return zoneOutPos;
    }

    public Transform GetInPos()
    {
        return zoneInPos;
    }

    public void SetPos(Vector3 pos, Quaternion rot)
    {
        /**
            pos is the new position ZoneInPos is going to be, and rot is the rotation all the zone is going to take.
        **/
        transform.rotation = rot;
        Vector3 differenceVector = transform.position - zoneInPos.position; //In fact we are just using x
        differenceVector.y = pos.y = 0;
        transform.position = pos + differenceVector;
        
        
    }
}
