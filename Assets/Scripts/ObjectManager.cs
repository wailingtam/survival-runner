using UnityEngine;
using System.Collections;

public class ObjectManager : MonoBehaviour {

	private Object star, medipack, teddyBear;
    public int minStars = 3, maxStars = 10;
    public string starString = "Star";
    public string medipackString = "Medipack";
    public string teddybearString = "Teddybear";
    public string spawnsFolderSuffix = "Spawns";
    

	Vector3 distanceInbetween = new Vector3(0f, 0f, 1f);

    void Awake()
    {
        star = Resources.Load(starString);
        medipack = Resources.Load(medipackString);
        teddyBear = Resources.Load(teddybearString);

    }

    void OnEnable()
    {
        Vector3 direction = (transform.Find("TurningZone").position - transform.Find("ZoneIn").position).normalized;
        DestroyAll(starString);
        DestroyAll(medipackString);
        DestroyAll(teddybearString);
        SpawnMultiple(star, starString + spawnsFolderSuffix, direction);
        SpawnMultiple(medipack, medipackString + spawnsFolderSuffix, direction);
        SpawnMultiple(teddyBear, teddybearString + spawnsFolderSuffix, direction);
    }
	
    private void DestroyAll(string pickupType)
    {
        foreach(GameObject pickup in GameObject.FindGameObjectsWithTag(pickupType)) Destroy(pickup);
    }


    private void SpawnMultiple(Object pickup, string spawnFolder, Vector3 direction)
    {
        Transform folder = transform.Find(spawnFolder);
        if(folder != null)
        {
            foreach (Transform spawn in folder)
            {
                int numInstances = spawnFolder.Equals(starString + spawnsFolderSuffix) ? Random.Range(minStars, maxStars) : Random.Range(0, 1);  //50% prob
                for (int i = 0; i < numInstances; i++)
                {
                    var newPickup = (GameObject) Instantiate(pickup, spawn.position + direction * i, spawn.rotation);
                    newPickup.transform.parent = gameObject.transform;  //If this script is executed before
                    //The script that turns the zone, we want the stars to turn and position with the zone, so we
                    //Set them as children
                }
            }
        }
    }
}
