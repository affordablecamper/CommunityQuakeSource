using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class AllySpawnManager : NetworkBehaviour {



    //public int food = 300;
    bool _enabled;
    public GameObject allyObj;
    public Camera Cam;
    public RaycastHit hitInfo;
    public PlayerInvData data;

    // Use this for initialization
    void Start () {
        _enabled = false;

	}
	
	// Update is called once per frame
	void Update () {
        Ray ray = Cam.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));
        
        if (Input.GetKeyDown(KeyCode.F1))
        {
            _enabled = true;

        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _enabled = false;

        }
            
        
        if (Input.GetButtonDown("Fire1") && _enabled == true)
        {
            if (data.food >= 1)
            {
                data.food -= 500;
                if (Physics.Raycast(ray, out hitInfo))
                {
                    
                    CmdOnSpawn();
                    
                }
            }
        }
    }
    [Command]
    private void CmdOnSpawn()
    {
        var ally = Instantiate(allyObj, hitInfo.point, Quaternion.identity);
        NetworkServer.Spawn(ally);
    }

    
    
}
