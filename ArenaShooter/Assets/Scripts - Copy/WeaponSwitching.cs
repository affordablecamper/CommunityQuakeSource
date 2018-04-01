using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class WeaponSwitching : NetworkBehaviour {

    public GameObject pick;
    //public Harvesting pickScript;
    public GameObject weapon1;
    public Shooting gunScript1;
    public GameObject weapon2;
    public l115a3 gunScript2;
    public GameObject worldviewGun;
    public GameObject worldviewGun2;
    private float switchCountDown;
    public float switchCoolDown;
    public Animator anim1;
    public Animator anim2;
    public void Start()
    {

        //pickScript = GetComponent<Harvesting>();
        gunScript1 = GetComponent<Shooting>();
        gunScript2 = GetComponent<l115a3>();

    }


    // Update is called once per frame
    void Update () {
        if (Input.GetKey(KeyCode.Tab))
        {
            pick.SetActive(false);
            //pickScript.enabled = true;
            weapon1.SetActive(false);
            gunScript1.enabled = false;
            weapon2.SetActive(false);
            gunScript2.enabled = false;

        }
        

            if (switchCountDown <= 0)
        {





            if (isLocalPlayer)
            {
                if (Input.GetKeyDown(KeyCode.F1))
                {


                    pick.SetActive(false);
                    //pickScript.enabled = true;
                    weapon1.SetActive(false);
                    gunScript1.enabled = false;
                    weapon2.SetActive(false);
                    gunScript2.enabled = false;

                }
                if (Input.GetKeyDown(KeyCode.Q) && gunScript1.isAiming == false && gunScript2.isAiming == false && gunScript1.isReloading == false && gunScript2.isReloading == false && gunScript2.isShooting == false) {

                    pick.SetActive(true);
                    //pickScript.enabled = true;
                    weapon1.SetActive(false);
                    gunScript1.enabled = false;
                    weapon2.SetActive(false);
                    gunScript2.enabled = false;
                }


                if (Input.GetAxis("Mouse ScrollWheel") > 0f && gunScript1.isAiming == false && gunScript2.isAiming == false && gunScript1.isReloading == false && gunScript2.isReloading == false && gunScript2.isShooting == false)
                {





                    switchCountDown = switchCoolDown;
                    weapon1.SetActive(false);
                    gunScript1.enabled = false;
                    weapon2.SetActive(true);
                    gunScript2.enabled = true;
                    CmdWorldModelChange1();
                    pick.SetActive(false);
                    anim2.SetBool("switchtoSniper", true);


                    StartCoroutine(doneAnim2());



                    //pickScript.enabled = false;
                }





                if (Input.GetAxis("Mouse ScrollWheel") < 0f && gunScript1.isAiming == false && gunScript2.isAiming == false && gunScript1.isReloading == false && gunScript2.isReloading == false && gunScript2.isShooting == false)
                {

                    switchCountDown = switchCoolDown;
                    weapon1.SetActive(true);
                    gunScript1.enabled = true;
                    weapon2.SetActive(false);
                    gunScript2.enabled = false;
                    CmdWorldModelChange2();
                    pick.SetActive(false);
                    anim1.SetBool("switchtoAk", true);
                   
                    StartCoroutine(doneAnim1());




                    //pickScript.enabled = false;
                }

            }


        }

        switchCountDown -= Time.deltaTime;
    }

    private IEnumerator doneAnim2()
    {
        yield return new WaitForSeconds(.5f);
        anim2.SetBool("switchtoSniper", false);
        Debug.Log("done");
    }

    private IEnumerator doneAnim1()
    {

        yield return new WaitForSeconds(.8f);
        anim1.SetBool("switchtoAk", false);
        Debug.Log("done");
    }

    [Command]
    void CmdWorldModelChange2()
    {
        RpcWorldModelChange2();
    }

    [ClientRpc]
    void RpcWorldModelChange2()
    {
        worldviewGun.SetActive(true);
        worldviewGun2.SetActive(false);

    }

    [Command]
   void CmdWorldModelChange1()
    {
        RpcWorldModelChange1();
    }

    [ClientRpc]
    void RpcWorldModelChange1()
    {
        worldviewGun.SetActive(false);
        worldviewGun2.SetActive(true);
    }
}
