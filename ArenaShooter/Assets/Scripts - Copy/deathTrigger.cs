using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deathTrigger : MonoBehaviour {
    public bool diedFalling;
	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("deathbytrigger");
            var PlayerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            diedFalling = true;
            PlayerScript.DeathByTrigger();
            

        }

         

    }

    private void OnTriggerExit()
    {


    }

}
