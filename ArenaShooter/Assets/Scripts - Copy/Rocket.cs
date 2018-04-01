using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using System.Diagnostics;


public class Rocket : MonoBehaviour
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
            RayCastShoot();
            Vector3 weaponObjectLocalPosition = RocketLauncherObj.transform.localPosition;
            weaponObjectLocalPosition.z = weaponObjectLocalPosition.z - recoilAmount;
            RocketLauncherObj.transform.localPosition = weaponObjectLocalPosition;
        }





    }
    

    private void Shoot()
    {
        tempProjectile = Instantiate(rocketObj, fwd.position, fwd.rotation) as GameObject;
        var rb = tempProjectile.GetComponent<Rigidbody>();
        rb.AddForce(tempProjectile.transform.forward * rocketSpeed);
        Destroy(tempProjectile, 6);
        
    }

    private void RayCastShoot()
    {
        Ray ray = cam.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo))
        {
            
            //RocketLauncherObj.LookAt(hitInfo.transform);
           

                var tempexpEffect = Instantiate(explosionEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                Destroy(tempexpEffect, 1);
                Destroy(tempProjectile);

               
            
            

        }


    }

}