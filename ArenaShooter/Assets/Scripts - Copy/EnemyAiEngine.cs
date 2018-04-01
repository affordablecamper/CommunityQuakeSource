using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Networking;
public class EnemyAiEngine : NetworkBehaviour {
    [Header("AnimationStuff")]

    public Animator Anim;
    [Space]
    [Header("GameOjectInfo")]
    public GameObject[] gos1;
    public GameObject[] gos;
    public GameObject Enemy;
    public GameObject muzzleFlash;
    public Transform shootPos;
    // public GameObject closestPlayer;
    [Space]


    [Header("Bools")]

    [Space]
    [SerializeField]
    private bool findPlayer;
    public bool canShoot = true;
    public bool muzzleFlashEnable;
    
    [Header("Floats")]

    [Space]
    public float lookRadius = 10f;
    private float fireCountdown = 0f;
    public float fireRate = 1f;
    public float muzzleflashtime = 0.1f;
    private float muzzleFlashTimerStart;
    public float shootdistance;
    //public float detectDistance;
    public float maxRadius;
    // private float distance ;
    

   

    [Header("Integer")]

    [Space]
    public int Damage;

    [Header("AI Info")]

    [Space]
    [SerializeField]
    Transform target;
    NavMeshAgent agent;


    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Found player");
            findPlayer = true;

        }
        

    }
    

    
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {

           
            findPlayer = false;

        }

    }
    




    




    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        Anim = GetComponent<Animator>();
        muzzleFlashTimerStart = muzzleflashtime;


    }

    void Update()
    {
        shootPos.transform.LookAt(target);
        Vector3 enemVec = (Enemy.transform.position);



        if (findPlayer == true) {


            gos1 = GameObject.FindGameObjectsWithTag("AllyAI");
            gos = GameObject.FindGameObjectsWithTag("Player") ;
            GameObject closest = null;
            float distance = Mathf.Infinity;
            Vector3 position = transform.position;
            foreach (GameObject go in gos)
            {
                Debug.Log("Found player");

                Vector3 diff = go.transform.position - position;
                float curDistance = diff.sqrMagnitude;
                if (curDistance < distance)
                {
                    closest = go;
                    distance = curDistance;
                  
                    target = closest.transform;
                }
            }


            foreach (GameObject go in gos1)
            {

                Debug.Log("Found AI");
                Vector3 diff = go.transform.position - position;
                float curDistance = diff.sqrMagnitude;
                if (curDistance < distance)
                {
                    closest = go;
                    distance = curDistance;

                    target = closest.transform;
                }
            }


        }

        if (findPlayer == false)
            target = null;


       // var all = Physics.SphereCastAll(enemVec, lookRadius, Enemy.transform.forward, maxRadius);

        //foreach (var hit in all)
       // {

           // if (hit.collider.tag == "Player")
           // {
                
                  //  target = hit.transform;
                  //  findPlayer = true;
                    //distance = Vector3.Distance(hit.transform.position, - this.transform.position);


                
           // }
       // }







            if (muzzleFlashEnable == true)
            {

                muzzleFlash.SetActive(true);
                muzzleflashtime -= Time.deltaTime;


            }


            if (muzzleflashtime <= 0)
            {
                muzzleFlash.SetActive(false);
                muzzleFlashEnable = false;
                muzzleflashtime = muzzleFlashTimerStart;



            }



            if (fireCountdown <= 0f)
            {


                canShoot = true;


                fireCountdown = 1f / fireRate;


            }
            else canShoot = false;

            fireCountdown -= Time.deltaTime;


            if (findPlayer == true)
            {



                Vector3 direction = (target.transform.position - transform.position).normalized;
                Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
                float __distance = Vector3.Distance(target.transform.position, transform.position);


                if (__distance <= lookRadius)
                {

                    //CmdOnMove();
                     agent.SetDestination(target.position);
                    Anim.SetBool("foundPlayer", true);
                    agent.isStopped = false;

                }


                if (__distance >= lookRadius)
                    Anim.SetBool("foundPlayer", false);


                if (__distance <= agent.stoppingDistance + shootdistance)
                {

                   // CmdOnStop();
                    agent.isStopped = true;
                    Anim.SetBool("foundPlayer", false);



                    if (Anim.GetCurrentAnimatorStateInfo(0).IsName("idle_aiming"))
                    {
                        onShoot();
                    }








                }

            }
        
    }
    //[Command]
    //private void CmdOnStop()
    //{
      //  RpcOnStop();
    //}

   // [ClientRpc]
   // private void RpcOnStop()
   // {
      //  agent.isStopped = true;
   // }

   // [Command]
   // private void CmdOnMove()
   // {

        //RpcOnMove();
       // agent.SetDestination(target.position);

   // }

    //[ClientRpc]
   // private void RpcOnMove()
   // {
      //  agent.isStopped = false;
        //agent.SetDestination(target.position);
     
   // }

    private void onShoot()
    {
         
        if (canShoot == true)
        {
            Vector3 fwd = Enemy.transform.TransformDirection(Vector3.forward);
            Debug.DrawRay(Enemy.transform.position, fwd * 50, Color.red);
            RaycastHit hitInfo;
            muzzleFlashEnable = true;
            
            if (Physics.Raycast(shootPos.transform.position, shootPos.transform.forward, out hitInfo, 50))
            {
                if (hitInfo.collider.tag == "Player")
                {




                    
                    
                        CmdPlayerShot(hitInfo.collider.name, Damage);


                    }
                }
            if (Physics.Raycast(shootPos.transform.position, shootPos.transform.forward, out hitInfo, 50))
            {
                if (hitInfo.collider.tag == "AllyAI")
                {






                    hitInfo.collider.gameObject.SendMessageUpwards("takeDamage", Damage);


                }
            }



        }
    }

    [Command]
     void CmdPlayerShot(string _playerID, int _damage)
    {
        Debug.Log(_playerID + "has been shot");
        Player _player = GameManager1.GetPlayer(_playerID);
        _player.RpcTakeDamage(_damage);
    }

    void FaceTarget()
    {
       
        

    }


    private void OnDrawGizmosSelected()
    {


        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }

}
