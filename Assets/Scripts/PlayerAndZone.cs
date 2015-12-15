using UnityEngine;
using System.Collections;

public class PlayerAndZone : MonoBehaviour {

    public GameObject zones;
    private ZonesMain script;

	// Use this for initialization
	void Awake () {
        zones = GameObject.Find("Zones");
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
                script.ActivateZone(); break;
            case "ZoneIn":
                script.DeactivateZone(); break;     
        }
    }
}
