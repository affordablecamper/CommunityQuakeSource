using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class EnemyHealth : NetworkBehaviour {
    //public Player player;
    [SyncVar]
    private bool _isDead = false;
    [SerializeField]
    private float enemyHealth;
    [SerializeField]
    public GameObject enemyGFX;
    [SerializeField]
    public GameObject ragDoll;
    [SerializeField]
    public GameObject bloodSplatter;
    public bool isDead

    {

        get { return _isDead; }
        protected set { _isDead = value; }
    }



    public void takeDamage(int __amount) {

        if (isDead)
            return;
        enemyHealth -= __amount;
        if (enemyHealth <= 0) {



            die();

        }

    }




    public void die()
    {

        //GameObject rg = (GameObject)Instantiate(ragDoll, transform.position, Quaternion.identity);
        //GameObject bS = (GameObject)Instantiate(bloodSplatter, transform.position, Quaternion.identity);

        //enemyGFX.SetActive(false);
        //isDead = true;
        CmdEnemyOnDie();




    }
    [Command]
    public void CmdEnemyOnDie()
    {

        RpcEnemyOnDie();

    }

    [ClientRpc]
    private void RpcEnemyOnDie()
    {

        GameObject rg1 = (GameObject)Instantiate(ragDoll, transform.position, Quaternion.identity);
        Destroy(rg1, 60);


        Destroy(enemyGFX);

    }

}
