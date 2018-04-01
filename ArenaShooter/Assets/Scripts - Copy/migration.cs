using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class migration : NetworkBehaviour {


    private GameObject playerCharacter;

    // Use this for initialization
    void Start () {
       
      
    }
	
	// Update is called once per frame
	void Update () {







    }
    [Command]
     public void CmdPlayerShot(string _playerID)
    {
        Debug.Log(_playerID + "has been shot");
    }
}
