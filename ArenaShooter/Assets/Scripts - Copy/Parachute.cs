using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class Parachute : MonoBehaviour {


    public RigidbodyFirstPersonController.AdvancedSettings r;
    private ParachuteTrigger trig;
    //public ParachuteTrigger trig;
    // Use this for initialization
    void Start () {

        r.airControl = true;


	}
	
	// Update is called once per frame
	void Update () {


        GameObject[] gos = GameObject.FindGameObjectsWithTag("aircontrol");
        foreach (GameObject go in gos)
        {
            trig = go.GetComponent<ParachuteTrigger>();
        }


        if (trig.airControll == true)
         {

         r.airControl = true;



         }
        else r.airControl = false;


    }
}
