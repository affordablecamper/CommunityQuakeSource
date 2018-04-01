using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deathTrigger : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            
            var PlayerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            PlayerScript.DeathByTrigger();
        }

         

    }

    private void OnTriggerExit()
    {


    }

}
