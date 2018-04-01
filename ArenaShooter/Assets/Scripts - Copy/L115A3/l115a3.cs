using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System;
using EZCameraShake;
//script by Daniel Bryant


public class l115a3 : NetworkBehaviour

{
    //[SerializeField]
    //variables!

    [Header("CameraShake")]
    public float Magnitude = 2f;
    public float Roughness = 3f;
    public float FadeOutTime = 2f;

    [SerializeField]

    [Header("Misc")]
    public playerWeaponData weapon;     //Data for weapon
    public Camera FPSCamera;                //Camera
    public Camera snipercam;
    public LayerMask enviormentMask;
    public LayerMask Playermask;              //Mask to know what to shoot.
    public LayerMask enemyMask;
    [Space]

    [Header("Integers/Values")]
                   //Damage
    public int magAmmo = 30;            //Total mag ammo.
    public int reserveAmmo = 90;            //Ammo that is left or reserved.
    public int maxMagazineSize = 30;    //The total Size of the mag.

    [Space]
    [Header("Floats/Values")]
    //public float boltForce = .3f;           //The force of the bolt.
    //public float boltTime = .1f;        //How much time the bolt has.
    public float fireRate = 1f;             //FireRate.
    public float weaponRange = 100f;    //Range of the weapon.
    public float tracerPower;               //The thrust of the tracer.
    public float aimSpeed = 2f;         //How fast the gun aims(Not really weird bug rn).
    public float maxRnd = 10;               //Recoil stuff.
    public float minRnd = 3;            //Recoil stuff.
    public float shotRnd = 3;               //Recoil stuff.
    public float recoilAmt = 5;         //Recoil stuff.
    public float recoilRecoveryRate = 4;    //Recoil stuff.
    public float muzzleflashtime = 0.1f;//How fast the muzzle flash goes off for.
    public float recoilAmount = 2f;         //Physical recoil stuff.
    public float bulletCasingForce;     //The force of the bullet casing.
    public float fovintensity = 15f;        //The intensity of the field of view.
    public float shiftfovintensity = 17f;//The intensity of the field of view while pressing shift.
    public float fovSpeed = 3f;             //How fast the field of view takes to set in.
    
    public float raycastForce;
   
    public float reloadTime = 3f;        //How long it takes to reload.
    //privates
    public float fireCountdown = 0f;   //Something to do with firerate.
    private float defaultFov;               //The field of view that the camera is on.
    private float shiftdefaultFov;      //The field of view that the camera is on when shift is pressed.
    private float aimFOV;                   //The field of view when aiming.
    private float shiftaimFOV;          //The field of view when pressing shift.
    private float muzzleFlashTimerStart;    //When the muzzle flash should start again.


    [Header("GameObjects")]
    [Space]
    public GameObject boltObject;       //The bolt and or slide of the gun.
    public GameObject weaponObject;         //The weapon its self
    public GameObject muzzleFlashObject;//The muzzle flash
    public GameObject thirdpersonMuzzleFlashObject;
    public GameObject muzzleFlash;          //The light
    public GameObject sandimpactEffect;      //The impact effect
    public GameObject metalimpactEffect;
    public GameObject cokeEffect;
    public GameObject glassImpact;
    public GameObject playerimpactEffect;
    public GameObject rockimpactEffect;
    public GameObject concreteimpactEffect;
    public GameObject bulletCasing;         //The bullet casing
    public GameObject tracer;            //The tracer
    public GameObject networkviewAkm;       //The object that has the animated component attached to it.
    //privates
    private GameObject playerCharacter; //The player character.


    [Space]
    [Header("Booleans")]
    public bool canShoot;              //If the weapon can shoot or not.
    public bool muzzleFlashEnabled = false;//If the muzzle flash is enabled.
    public bool magEmpty;              //If the mag is empty.
    public bool isAiming = false;        //If the player is aiming.
    public bool isReloading;           //If the player is reloading.
    public bool isshiftAiming;          //If the player is aiming and pressing shift.
    public bool shootAnim;
    public bool isShooting;
    private bool audioPlay = true;     //If the audio is playing.
    private bool isPressed;

    [Space]
    [Header("Transforms")]
    public Transform bulletCasingSpawn; //The location of the "bulletcasingspawn"
    public Transform fwd;             //The forward working direction that the "tracer" gameobject comes out from

    [Space]
    [Header("Vectors")]
    public Vector3 normalPosition;          //The normal position of the view model.
    public Vector3 AimPos;              //The postion of the view model while aiming. 
    public Vector3 extAimPos;
    //public Vector3 shiftaimPos;
    private Vector3 swayPos;                //Sway position



    [Space]
    [Header("Audio")]
    private AudioEngine _audioEngine;
    private AudioSource Audio;         //The audio source
    public AudioClip reloadsound;       //Audio stuff
   public AudioClip gunshotClip;      //Audio stuff
    public AudioClip noAmmo;            //Audio stuff
    public AudioClip tacticalreloadSound;   //Audio stuff
    public AudioClip sandimpactSound;      //Audio stuff
   public AudioClip metalimpactSound;
    public AudioClip playerimpactSound;
    public AudioClip concreteimpactSound;
    //animation stuff
    public Animator akmReload;         //Animation stuff



    //public Animation Reload;
    //public Text ammoText;










    private void Start()
    {

        isPressed = true;
        shootAnim = true;
        _audioEngine = this.GetComponent<AudioEngine>();
        canShoot = true;
        akmReload = networkviewAkm.GetComponent<Animator>();
        playerCharacter = GameObject.FindGameObjectWithTag("Anim");
        //networkviewAkm = GameObject.FindGameObjectWithTag("Anim");
        isReloading = false;
        Audio = GetComponent<AudioSource>(); // finding the audio source
        defaultFov = FPSCamera.fieldOfView; // finding the fov on camera on start!
        aimFOV = defaultFov - fovintensity; //aim fov is the fps.cameras fieldofview - the fov intensity
        shiftaimFOV = shiftdefaultFov + shiftfovintensity;
        muzzleFlashTimerStart = muzzleflashtime;
        akmReload.SetBool("isIdle", true);
        akmReload.SetBool("isReload", false);
        akmReload.SetBool("isFire", false);
        if (FPSCamera == null)
        {

            Debug.LogError(Color.cyan + "No FPS Cam is attached");
            this.enabled = false;
        }

    }



    // Update is called once per frame
    //[Client]

    

        
    void Update()
    {

        if (isLocalPlayer)
        {

            shotRnd = Mathf.MoveTowards(shotRnd, minRnd, recoilRecoveryRate * Time.deltaTime);


            if (!isAiming)
            { shotRnd = Mathf.Clamp(shotRnd + recoilAmt, minRnd, maxRnd); }

            if (isAiming)
            { shotRnd = 0; }

            if (fireCountdown <= 0f)
            {


                if (canShoot == true &&isReloading == false)
                {
                    Shoot();
                    isPressed = false;
                }

            }

            fireCountdown -= Time.deltaTime;




            //if (fireCountdown >= 0f)
            //{
               
                if (Input.GetKeyDown(KeyCode.R) && magAmmo != maxMagazineSize && reserveAmmo != 0 && isAiming == false &&shootAnim &&fireCountdown <= 0 &&!isPressed)
                {
                    
                    //animationaccrossNetwork animName = gameObject.AddComponent<animationaccrossNetwork>();
                    if (magEmpty == true)
                    {

                        isPressed = true;
                        StartCoroutine(ReloadTimer());
                        Audio.PlayOneShot(reloadsound);
                        Debug.Log("Reload with slide");
                        akmReload.SetBool("isIdle", false);
                        akmReload.SetBool("isReload", true);
                        akmReload.SetBool("isFire", false);


                        isReloading = true;
                        
                    }
                    if (magEmpty == false)
                    {
                        akmReload.SetBool("isIdle", false);
                        akmReload.SetBool("isReload", true);
                        akmReload.SetBool("isFire", false);

                        Audio.PlayOneShot(tacticalreloadSound);
                         StartCoroutine(ReloadTimer());
                         isPressed = true;
                         isReloading = true;

                    }
                }

            //}




            if (magAmmo <= 0)
            {

                magEmpty = true;


            }
            else magEmpty = false;
            

            
            




            if (muzzleFlashEnabled == true)
            {

                muzzleFlashObject.SetActive(true);
                muzzleflashtime -= Time.deltaTime;
                muzzleFlash.SetActive(true);
                thirdpersonMuzzleFlashObject.SetActive(true);
            }


            if (muzzleflashtime <= 0)
            {
                muzzleFlashObject.SetActive(false);
                muzzleFlashEnabled = false;
                muzzleflashtime = muzzleFlashTimerStart;
                muzzleFlash.SetActive(false);
                thirdpersonMuzzleFlashObject.SetActive(false);
                CmdNoShootEffect();
            }

            if (Input.GetKey(KeyCode.Mouse1)&& isReloading == false)
            {

                isAiming = true;

            }
            else if (!Input.GetKey(KeyCode.Mouse1)&& isReloading == false)
            {


                isAiming = false;

            }










        }




    }

    private void shiftaim()
    {

        weaponObject.transform.localPosition = Vector3.Lerp(weaponObject.transform.localPosition, extAimPos, Time.deltaTime * aimSpeed);


    }

    private IEnumerator ReloadTimer()
    {




        yield return new WaitForSeconds(reloadTime);



        int totalAmmo = magAmmo + reserveAmmo;
        if (totalAmmo <= maxMagazineSize)
        {

            magAmmo = totalAmmo;
            reserveAmmo = 0;

        }
        else
        {

            int shots = maxMagazineSize - magAmmo;
            magAmmo = maxMagazineSize;
            reserveAmmo -= shots;

        }
        isReloading = false;
        canShoot = true;
        akmReload.SetBool("isIdle", true);
        akmReload.SetBool("isReload", false);
        akmReload.SetBool("isFire", false);
       
    }


    [Client]
    public void Shoot()
    {

        {
            //if (!isLocalPlayer)
            //{


            Ray ray = FPSCamera.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));
            RaycastHit hitInfo;
            //ammoText.text = ("Ammo:" + magAmmo + "/" + reserveAmmo); // set the ammo text to the desired ammounts set and done by the vars
            ray.direction = (ray.direction * 100 + UnityEngine.Random.insideUnitSphere * shotRnd).normalized;
            Debug.DrawRay(ray.origin, ray.direction * weaponRange, Color.green);
            if (Input.GetButtonDown("Fire1") && magEmpty == false && audioPlay == true && isReloading == false)
                
            {
                isShooting = true;
                if (fireCountdown <= 0)
                {
                    akmReload.SetBool("isIdle", false);
                    akmReload.SetBool("isReload", false);
                    akmReload.SetBool("isFire", true);
                }
                shootAnim = false;
                StartCoroutine(fireAnim());
                StartCoroutine(fireAnimSwitch());
                fireCountdown = fireRate;
                GameObject newProjectile = Instantiate(tracer, fwd.transform.position, fwd.transform.rotation) as GameObject;
                newProjectile.GetComponent<Rigidbody>().AddForce(fwd.transform.forward.normalized * tracerPower);
                
                doneShooting();
                CmdOnShootEffect();
                //Vector3 Bolt = boltObject.transform.localPosition;
                //Bolt.z = Bolt.z - boltForce;

                //boltObject.transform.localPosition = Bolt;

                // effect and other good stuff

                // Instantiate(bulletCasing, bulletCasingSpawn.transform.position, UnityEngine.Random.rotation);
                // need to work more on bullet casing todoo <------- (MUST FIX)
                //https://www.youtube.com/watch?v=xinUEH-XGfM use that for example
                muzzleFlashEnabled = true;
                thirdpersonMuzzleFlashObject.SetActive(true);
                magAmmo -= 1;
                CameraShaker.Instance.ShakeOnce(Magnitude, Roughness, 0, FadeOutTime);
                //recoil effect kinda... might change this later on doesent actully effect the bullet or raycast <<<TODO
                //Audio.PlayOneShot(gunshotClip); //playing audio from audio source
                _audioEngine.PlaySound(4);
                //Vector3 weaponObjectLocalPosition = weaponObject.transform.localPosition;
                //weaponObjectLocalPosition.z = weaponObjectLocalPosition.z - recoilAmount;
                //weaponObject.transform.localPosition = weaponObjectLocalPosition;
                //migration Name = gameObject.AddComponent<migration>();


                if (Physics.Raycast(ray, out hitInfo))
                {
                    Debug.DrawLine(transform.position, hitInfo.point, Color.red);

                    //Debug.Log(hitInfo.point);

                    CmdOnTracerShoot(hitInfo.point);
                   //newProjectile.GetComponent<Rigidbody>().velocity = (hitInfo.point - transform.position).normalized * tracerPower;
                   // newProjectile.GetComponent<Rigidbody>().rotation = Quaternion.LookRotation(newProjectile.GetComponent<Rigidbody>().velocity);

                }




                if (Physics.Raycast(ray, out hitInfo, weaponRange, enviormentMask))
                {


                    if (hitInfo.collider.tag == "Coke")
                    {

                        //need to fix when shooting barrel behind does not add force to right position.
                        hitInfo.rigidbody.AddForceAtPosition(raycastForce * hitInfo.transform.forward, hitInfo.point);

                        Instantiate(cokeEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                        //Audio.PlayOneShot(metalimpactSound);
                        _audioEngine.PlaySound(2);
                    }


                    if (hitInfo.collider.tag == "Barrel")
                    {

                        //need to fix when shooting barrel behind does not add force to right position.
                        hitInfo.rigidbody.AddForceAtPosition(raycastForce * hitInfo.transform.forward, hitInfo.point);

                        Instantiate(metalimpactEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                        //Audio.PlayOneShot(metalimpactSound);
                        _audioEngine.PlaySound(3);
                    }
                    if (hitInfo.collider.tag == "Metal")
                    {
                        Instantiate(metalimpactEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                        //Audio.PlayOneShot(metalimpactSound);
                        _audioEngine.PlaySound(3);
                    }

                    if (hitInfo.collider.tag == "Sand")
                    {
                        Instantiate(sandimpactEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                        //Audio.PlayOneShot(sandimpactSound);
                        _audioEngine.PlaySound(2);
                    }


                    if (hitInfo.collider.tag == "Glass")
                    {
                        Instantiate(glassImpact, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                        //Audio.PlayOneShot(sandimpactSound);
                        _audioEngine.PlaySound(5);
                    }


                    if (hitInfo.collider.tag == "Rock")
                    {
                        Instantiate(rockimpactEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                        //Audio.PlayOneShot(sandimpactSound);
                        _audioEngine.PlaySound(2);
                    }


                    if (hitInfo.collider.tag == "Concrete")
                    {
                        Instantiate(concreteimpactEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                        //Audio.PlayOneShot(concreteimpactSound);
                        _audioEngine.PlaySound(1);
                    }
                }

                    if (Physics.Raycast(ray, out hitInfo, weaponRange, Playermask))
                {

                    CmdOnPlayerMaskShot(hitInfo.point, hitInfo.normal);
                    //Instantiate(playerimpactEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                    //Audio.PlayOneShot(playerimpactSound);
                    if (hitInfo.collider.tag == "Player")
                    {


                        

                        CmdPlayerShot(hitInfo.collider.name, weapon.l96Damage);
                        
                    }

                    

                }

                if (Physics.Raycast(ray, out hitInfo, weaponRange, enemyMask))
                {

                    Debug.Log("EnemyHit");
                    if (hitInfo.collider.tag == "Enemy")
                    {
                        Audio.PlayOneShot(playerimpactSound);
                        //EnemyHealth _enemy = hitInfo
                        Debug.Log("EnemyHit");
                        hitInfo.collider.gameObject.SendMessageUpwards("takeDamage", weapon.l96Damage);
                        //enem.takeDamage(weapon.AKDamage);
                    }




                }


            }
            else if (magEmpty == true)
            {
                if (Input.GetButton("Fire1"))
                {
                    Audio.PlayOneShot(noAmmo);

                }

                

            }
            return;
            //}

        }
    }

    private IEnumerator fireAnimSwitch()
    {
        yield return new WaitForSeconds(fireRate + 0.3f);
        isShooting = false;
    }

    private IEnumerator fireAnim()
    {
        yield return new WaitForSeconds(fireRate);

        akmReload.SetBool("isIdle", true);
        akmReload.SetBool("isReload", false);
        akmReload.SetBool("isFire", false);
        //isAiming = false;
        shootAnim = true;
    }

    [Command]
   void CmdOnPlayerMaskShot(Vector3 _pos, Vector3 _normal)
    {
        RpcOnPlayerMaskShot(_pos, _normal);
    }


    [ClientRpc]
    void RpcOnPlayerMaskShot(Vector3 _pos, Vector3 _normal)
    {
        Instantiate(playerimpactEffect, _pos, Quaternion.LookRotation(_normal));
        
    }

    [Command]
    void CmdOnShootEffect()
    {
        RpcOnShootEffect();
    }


    [ClientRpc]
    void RpcOnShootEffect()
    {
        thirdpersonMuzzleFlashObject.SetActive(true);
        
    }

    [Command]
     void CmdOnTracerShoot(Vector3 _hit)
    {
        RpcOnTracerShoot(_hit);

    }

    [ClientRpc]
    void RpcOnTracerShoot(Vector3 _hit)
    {
        GameObject newProjectile = Instantiate(tracer, fwd.transform.position, fwd.transform.rotation) as GameObject;
        newProjectile.GetComponent<Rigidbody>().AddForce(fwd.transform.forward.normalized * tracerPower);
        newProjectile.GetComponent<Rigidbody>().velocity = (_hit - transform.position).normalized * tracerPower;
        newProjectile.GetComponent<Rigidbody>().rotation = Quaternion.LookRotation(newProjectile.GetComponent<Rigidbody>().velocity);

    }

    [Command] 
     public void CmdPlayerShot(string _playerID, int _damage)
    {
        
        Debug.Log(_playerID + "has been shot");
        Player _player = GameManager1.GetPlayer(_playerID);
        _player.RpcTakeDamage(_damage);

    }


    private void doneShooting()
    {
        //StartCoroutine(boltShootingTimer());
        

    }

    [Command]
     void CmdNoShootEffect()
    {
        RpcNoShootEffect();
    }
    


    [ClientRpc]
    void RpcNoShootEffect()
    {
        thirdpersonMuzzleFlashObject.SetActive(false);
    }

   // private IEnumerator boltShootingTimer()
    //{
        //yield return new WaitForSeconds(boltTime);
        //Vector3 Bolt = boltObject.transform.localPosition;
        //Bolt.z = Bolt.z + boltForce;

        //boltObject.transform.localPosition = Bolt;
    //}

    private void FixedUpdate()
    {

        if (Input.GetKey(KeyCode.LeftShift) && isAiming)
        {


            shiftaim();



        }
         else isshiftAiming = false;

      


        


       


        if (isAiming == true)
        {

            weaponObject.transform.localPosition = Vector3.Lerp(weaponObject.transform.localPosition, AimPos, Time.deltaTime * aimSpeed);
            FPSCamera.fieldOfView = Mathf.Lerp(FPSCamera.fieldOfView, aimFOV, Time.deltaTime * fovSpeed);
        }

        else if (isAiming == false)
        {

            weaponObject.transform.localPosition = Vector3.Lerp(weaponObject.transform.localPosition, normalPosition, Time.deltaTime * aimSpeed);
            FPSCamera.fieldOfView = Mathf.Lerp(FPSCamera.fieldOfView, defaultFov, Time.deltaTime * fovSpeed);
        }




    }







}

