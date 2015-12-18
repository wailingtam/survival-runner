using UnityEngine;
using System.Collections;

public class ZonesMain : MonoBehaviour {

    private GameObject activeNewZone;  //We set in UI the initial active zone
    public GameObject activeOldZone;
    public int numberOfZones = 3;
    int activeNewZoneIndex = 0;
    System.Random rnd = new System.Random();
    private int count = 0;
    public int num_zones_until_hell = 1;

    void Awake () {
	}
	
	// Update is called once per frame
	void Update () {
	   
	}

    public void ActivateZone()
    {
        /**
            Traverse children: http://answers.unity3d.com/questions/205391/how-to-get-list-of-child-game-objects.html
        */
        if(count++ < num_zones_until_hell)
        {
            int newV;
            do
            {
                newV = rnd.Next(numberOfZones);
            } while (newV == activeNewZoneIndex);
            activeNewZoneIndex = newV;
        }
        else
        {
            count = 0;
            activeNewZoneIndex = 4;
        }
        //activeNewZoneIndex = (activeNewZoneIndex + 1) % 2;
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
