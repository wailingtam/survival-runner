using UnityEngine;
using System.Collections;

public class HellZoneMain : MonoBehaviour {

    private PlayerMovement playerMovement;
    private float playerV;
    private Object zomBear, zomBunny, hellephant, walkingMedipack, obstacle;
    public float spawnTime = 2f;
    public Transform[] spawnPoints;
    public string zomBearString = "ZomBear";
    public string zomBunnyString = "ZomBunny";
    public string hellephantString = "Hellephant";
    public Material HellSkybox;
    private Material mainSkybox;
    private bool hell = false;
    ObjectManager objectManager;
    private Light spotlight, mainlight;
    private float mainlightIntensity;
    private GameObject hellObjects;
    AudioSource suspense;
    GameObject hellMusic;
    AudioSource backgroundMusic;
    float rot = 0;
    public int kills = 0;
    private int NEED_KILLS = 10;
    private bool last = false, executed = false;
    Material ground;

    // Use this for initialization
    void Awake () {
        zomBear = Resources.Load(zomBearString);
        zomBunny = Resources.Load(zomBunnyString);
        hellephant = Resources.Load(hellephantString);
        objectManager = gameObject.GetComponent<ObjectManager>();
        spotlight = GameObject.Find("Spotlight").GetComponent<Light>();
        mainlight = GameObject.Find("MainLight").GetComponent<Light>();
        mainlightIntensity = mainlight.intensity;
        mainSkybox = RenderSettings.skybox;
        hellObjects = transform.FindChild("HellObjects").gameObject;
        suspense = gameObject.GetComponent<AudioSource>();
        backgroundMusic = GameObject.Find("Background Music").GetComponent<AudioSource>();
        hellMusic = transform.FindChild("Hell Music").gameObject;
        playerMovement = GameObject.Find("Player").gameObject.GetComponent<PlayerMovement>();
        playerV = playerMovement.v;
        ground = transform.FindChild("Grounds").transform.FindChild("Ground").gameObject.GetComponent<Renderer>().material;
    }
	
	// Update is called once per frame
	void Update () {
        if (hell)
        {
            rot += 2 * Time.deltaTime;
            rot %= 360;
            HellSkybox.SetFloat("_Rotation", rot);
        }
        if (executed)
        {
            backgroundMusic.Stop();
            ground.mainTextureOffset = new Vector2(0, -Time.time * (playerV + 1f));
        }
	}

    void OnDisable()
    {
        backgroundMusic.Play();
    }



    public void Execute()
    {
        //Executed by ShootingPlaceMain
        playerMovement.v = 0;
        PlayWithLight();
        executed = true;
    }

    private void PlayWithLight()
    {
        InvokeRepeating("DisableLight", 2f, 1f);
        InvokeRepeating("EnableLight", 2.5f, 1f);
        Invoke("CancelLights", 4f);
        
    }

    private void EnableLight()
    {
        spotlight.enabled = true;
    }

    private void DisableLight()
    {
        spotlight.enabled = false;
    }

    private void CancelLights()
    {
        spotlight.enabled = false;
        mainlight.intensity = 0.4f;
        CancelInvoke();
        Invoke("TriggerSpawn", 1f);
    }

    void TriggerSpawn()
    {
        RenderSettings.skybox = HellSkybox;
        hell = true;
        hellObjects.SetActive(true);
        suspense.Play();
        Invoke("PlayHellMusic", 2f);
        Invoke("Spawn", 3f);
    }

    void PlayHellMusic()
    {
        hellMusic.SetActive(true);
    }

     public void AddKill()
    {
        kills++;
        if (last) Finish();
    }

    void Spawn()
    {
        Debug.Log(kills);
        if (kills >= NEED_KILLS)
        {
            objectManager.DestroyAll(zomBearString);
            objectManager.DestroyAll(zomBunnyString);
            objectManager.SpawnMultiple(hellephant, "Spawn3", Vector3.zero, this);
            last = true;
        }
        else
        {
            Invoke("Spawn", kills > NEED_KILLS / 2 ? 0.9f : 1.5f);
            int rows = Random.Range(1, 4);
            Object enemy = Random.Range(0, 3) <= 1 ? zomBear : zomBunny;
            objectManager.SpawnMultiple(enemy, "Spawn" + rows.ToString(), Vector3.zero, this);
            if (Random.Range(0, 4) == 0) objectManager.SpawnMultiple(zomBear, "Spawn" + (((rows + 1) % 4)+1).ToString(), Vector3.zero, this);
        }

    }

    private void Finish()
    {
        RenderSettings.skybox = mainSkybox;
        hellObjects.SetActive(false);
        hellMusic.SetActive(false);
        spotlight.enabled = true;
        mainlight.intensity = mainlightIntensity;
        playerMovement.v = playerV;
        executed = last = hell = false;
        kills = 0;
        rot = 0;
    }


}
