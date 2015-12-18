using UnityEngine;
using System.Collections;

public class ShootingPlaceMain : MonoBehaviour {

    private HellZoneMain hm;

	// Use this for initialization
	void Start () {
        hm = transform.parent.GetComponent<HellZoneMain>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            hm.Execute();
        }
    }
}
