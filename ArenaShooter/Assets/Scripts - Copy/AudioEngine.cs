using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

//[RequireComponent(typeof (AudioSource))]
public class AudioEngine : NetworkBehaviour {
    public AudioSource source;
   
    public AudioClip[] clips;

	// Use this for initialization
	void Start () {
        

	}

    public void PlaySound(int id) {


        if (id >= 0 && id < clips.Length)
        {

            CmdSendServerSoundID(id);

        }


    }
    [Command]
     void CmdSendServerSoundID(int id)
    {
        RpcSendSoundIDToClients(id);
    }

    [ClientRpc]
    void RpcSendSoundIDToClients(int id)
    {
        
        source.PlayOneShot(clips[id]);

       

    }
}
