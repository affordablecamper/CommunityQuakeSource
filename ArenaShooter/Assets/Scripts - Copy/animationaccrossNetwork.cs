using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationaccrossNetwork : MonoBehaviour {
    private GameObject playerCharacter;
    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void LongReload()
    {
        
        
            playerCharacter = GameObject.FindGameObjectWithTag("Anim");
            playerCharacter.GetComponent<Animation>().Play("Reload");
        }
        

    public void ShortReload()
    {
        playerCharacter = GameObject.FindGameObjectWithTag("Anim");
        playerCharacter.GetComponent<Animation>().Play("Tactical Reload");
    }
}
