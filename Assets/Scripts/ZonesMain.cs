using UnityEngine;
using System.Collections;

public class ZonesMain : MonoBehaviour {

    private GameObject activeNewZone;  //We set in UI the initial active zone
    public GameObject activeOldZone;
    int activeNewZoneIndex = 0;
    System.Random rnd = new System.Random();

    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	   
	}

    public void ActivateZone()
    {
        /**
            Traverse children: http://answers.unity3d.com/questions/205391/how-to-get-list-of-child-game-objects.html
        */
        int newV;
        do
        {
            newV = rnd.Next(transform.childCount);
        } while (newV == activeNewZoneIndex);
        activeNewZoneIndex = newV;
        //activeNewZoneIndex = (activeNewZoneIndex + 1) % transform.childCount;
        Transform child = transform.GetChild(activeNewZoneIndex);
        activeNewZone = child.gameObject;
        activeNewZone.SetActive(true);
        Transform outPos = activeOldZone.GetComponent<ZoneMain>().GetOutPos();
        activeNewZone.GetComponent<ZoneMain>().SetPos(outPos.position, outPos.rotation);

    }

    public void DeactivateZone()
    {
        activeOldZone.SetActive(false);
        activeOldZone = activeNewZone;
        activeNewZone = null;
    }

}
