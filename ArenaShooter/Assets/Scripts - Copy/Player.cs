using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class Player : NetworkBehaviour {


    public float respawnTimer;
    public Camera deathCam;
    public AudioSource source;
    public AudioClip deathnoise;
    public AudioClip deathnoise2;
    public AudioClip deathnoise3;
    public AudioClip healthnoise1;
    public AudioClip healthnoise2;
    public AudioClip healthnoise3;
    public float deathnoiseSelect;
    [SyncVar]
    private bool _isDead = false;
    public CharacterController _char;
    public GameObject viewModel;
    private bool _charDead;
    //public GameObject ragDoll;
    //public GameObject BloodSplatter;
    //public CapsuleCollider playercollider;
    public bool isDead

    {

        get { return _isDead; }
        protected set { _isDead = value; }
    }




    [SerializeField]
    private int maxHealth = 100;

    [SyncVar]
    public int currentHealth;

    private void Start()
    {

    }

    [SerializeField]
    public Behaviour[] disableOnDeath;
    [SerializeField]
    public GameObject[] _disablegameObjectOnDeath;
    public bool[] wasEnabled;
    public GameObject[] weapons;
    public Renderer[] rend;
    public void Setup()
    {


        wasEnabled = new bool[disableOnDeath.Length];
        for (int i = 0; i < wasEnabled.Length; i++)
        {

            wasEnabled[i] = disableOnDeath[i].enabled;

        }




        SetDefaults();


    }
    public void DeathByTrigger() {

        RpcTakeDamage(100);

    }

     void Update()
    {

        if (_charDead)
        {
            
            _char.enabled = false;
        }
        if (!isLocalPlayer)
            return;

        if (Input.GetKeyDown(KeyCode.K)) {

            RpcTakeDamage(999999);

        }

    }

    [ClientRpc]
    public void RpcTakeDamage(int _amount) {

        if (isDead)
            return;

            currentHealth -= _amount;

            Debug.Log(transform.name + "Now has " + currentHealth);

        if (currentHealth <= 75)
        {

            source.PlayOneShot(healthnoise1);

        }

        if (currentHealth <= 50)
        {

            source.PlayOneShot(healthnoise2);

        }

        if (currentHealth <= 25)
            {

           source.PlayOneShot(healthnoise3);

            }

        if (currentHealth <=0)
        {
            deathnoiseSelect = UnityEngine.Random.Range(1, 4);

            if (deathnoiseSelect == 1) {
                source.PlayOneShot(deathnoise);
            }
                

            if (deathnoiseSelect == 2) {
                source.PlayOneShot(deathnoise2);
            }


            if (deathnoiseSelect == 3) {
                source.PlayOneShot(deathnoise3);
            }
                

            Die();


        }

    }

    private void Die()
    {
        isDead = true;
        _charDead = true;
        for (int i = 0; i < disableOnDeath.Length; i++)
        {


            disableOnDeath[i].enabled = false;
        }

       // playercollider.enabled = false;
        for (int i = 0; i < rend.Length; i++)
        {

            rend[i].enabled = false;

        }

        if (isLocalPlayer)
        {

            deathCam.enabled = true;
        }
        for (int i = 0; i < _disablegameObjectOnDeath.Length; i++)
        {

            _disablegameObjectOnDeath[i].SetActive(false);
           

        }

       // for (int i = 0; i < fpsCam.Length; i++)
       // {

           // fpsCam[i].enabled = false;


        //}

        Collider _col = GetComponent<Collider>();

        if (_col != null)
            _col.enabled = true;


       // GameObject rg =  (GameObject)Instantiate(ragDoll, transform.position, Quaternion.identity);
       // GameObject bS = (GameObject)Instantiate(BloodSplatter, transform.position, Quaternion.identity);
        //Destroy(bS, 5);

        //Destroy(rg, 60);

        Debug.Log(transform.name + " has died");

        StartCoroutine(Respawn());

    }



 

    

  



    IEnumerator Respawn() {

        yield return new WaitForSeconds(respawnTimer);

        SetDefaults();
        Transform _spawnPoint = NetworkManager.singleton.GetStartPosition();
        transform.position = _spawnPoint.position;
        transform.rotation = _spawnPoint.rotation;

    }

    






    public void SetDefaults()
    {

        isDead = false;
        //playercollider.enabled = false;
        _charDead = false;
        currentHealth = maxHealth;

        for (int i = 0; i < disableOnDeath.Length; i++)
        {

            disableOnDeath[i].enabled = wasEnabled[i];

        }

        for (int i = 0; i < rend.Length; i++)
        {

            rend[i].enabled = true;

        }

        for (int i = 0; i < _disablegameObjectOnDeath.Length; i++)
        {

            _disablegameObjectOnDeath[i].SetActive(true);
            
           
        }

        if (isLocalPlayer)
        {

            deathCam.enabled = false;
        }

        for (int i = 0; i < weapons.Length; i++)
        {


            weapons[i].SetActive(false);
        }

        Collider _col = GetComponent<Collider>();

        if (_col != null)
            _col.enabled = true;

    }
}
