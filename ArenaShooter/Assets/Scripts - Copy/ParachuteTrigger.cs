using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParachuteTrigger : MonoBehaviour {

    public bool airControll;

	// Use this for initialization
	void Start () {
		

        

	}
	
	// Update is called once per frame
	void Update () {
		



	}



    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {

            airControll = true;


        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player") {
            airControll = false;

        }

    }


}
