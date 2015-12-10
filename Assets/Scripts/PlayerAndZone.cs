using UnityEngine;
using System.Collections;

public class PlayerAndZone : MonoBehaviour {

    public GameObject zones;
    private ZonesMain script;

	// Use this for initialization
	void Start () {
        script = zones.GetComponent<ZonesMain>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.name)
        {
            case "ZoneOut":
                Debug.Log("zone out triggered");
                script.ActivateZone(); break;
            case "ZoneIn":
                Debug.Log("zone in triggered");
                script.DeactivateZone(); break;     
        }
    }
}
