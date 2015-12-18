﻿using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public int damagePerShot = 20;
    private float timeBetweenBullets = 0.05f;
    public float range = 100f;


    float timer;
    Ray shootRay;
    RaycastHit shootHit;
    int shootableMask;
    ParticleSystem gunParticles;
    LineRenderer gunLine;
    AudioSource gunAudio;
    Light gunLight;
    float effectsDisplayTime = 0.2f;

	GameObject effect;

    void Awake ()
    {
        shootableMask = LayerMask.GetMask ("Shootable");
        gunParticles = GetComponent<ParticleSystem> ();
        gunLine = GetComponent <LineRenderer> ();
        gunAudio = GetComponent<AudioSource> ();
        gunLight = GetComponent<Light> ();
    }


    void Update ()
    {
        timer += Time.deltaTime;

		if(Input.GetKeyDown(KeyCode.Space) && timer >= timeBetweenBullets && Time.timeScale != 0)
        {
            Shoot ();
        }

        if(timer >= timeBetweenBullets * effectsDisplayTime)
        {
            DisableEffects ();
        }
    }


    public void DisableEffects ()
    {
        gunLine.enabled = false;
        gunLight.enabled = false;
    }


    void Shoot ()
    {
        timer = 0f;

        gunAudio.Play ();

        gunLight.enabled = true;

        gunParticles.Stop ();
        gunParticles.Play ();

        gunLine.enabled = true;
        gunLine.SetPosition (0, transform.position);

        shootRay.origin = transform.position;
        shootRay.direction = transform.forward;

        if(Physics.Raycast (shootRay, out shootHit, range, shootableMask))
		{
			if (shootHit.collider.gameObject.CompareTag("Present")) {
				GameObject parent = shootHit.collider.gameObject.transform.parent.gameObject;
				effect = parent.transform.GetChild(1).gameObject;
				effect.SetActive(true);
				Invoke("StopEmitter", 3);
				Destroy(shootHit.collider.gameObject);
				DisplayManager.score += 50;
			}
			else {
	            EnemyHealth enemyHealth = shootHit.collider.GetComponent <EnemyHealth> ();
	            if(enemyHealth != null)     
	            {
	                enemyHealth.TakeDamage (damagePerShot, shootHit.point);
	            }
			}
            gunLine.SetPosition (1, shootHit.point);
        }
        else
        {
            gunLine.SetPosition (1, shootRay.origin + shootRay.direction * range);
        }
    }

	void StopEmitter() {
		effect.SetActive(false);
	}
}
