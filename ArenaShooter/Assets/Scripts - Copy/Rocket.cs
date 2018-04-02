using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using System.Diagnostics;
using UnityEngine.Networking;

public class Rocket : NetworkBehaviour
{
    public Stopwatch timer;
    public Camera cam;
    public GameObject rocketObj;
    public int rocketSpeed;
    public GameObject explosionEffect;
    public Transform RocketLauncherObj;
    public Transform fwd;
    public AudioSource Audio;
    private GameObject tempProjectile;
    public AudioClip RocketFire;
    public float recoilAmount;
    public float fireRate;
    private float nextFire;
    public float proximity;
    public float Splashdamage;
    public float CritHitDamage;
    public float splashRadius;
    public RaycastHit hitInfo;
    public Collider[] hitColliders;
    public LayerMask LayerToHit;
    private Vector3 midScreen;
    
    private void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {


        if (Input.GetButton("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Audio.PlayOneShot(RocketFire);
            Shoot();

            Vector3 weaponObjectLocalPosition = RocketLauncherObj.transform.localPosition;
            weaponObjectLocalPosition.z = weaponObjectLocalPosition.z - recoilAmount;
            RocketLauncherObj.transform.localPosition = weaponObjectLocalPosition;
        }





    }
    

    private void Shoot()
    {

        Ray ray = cam.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));
        var instanceRocket = Instantiate(rocketObj, transform.position, transform.rotation);
        var rb = instanceRocket.GetComponent<Rigidbody>();
        rb.AddForce(ray.direction * rocketSpeed);
        
        
    }

    

    





}