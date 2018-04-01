using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpSounds : MonoBehaviour {

    public AudioSource Audio;
    public AudioClip jumpAudio;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {


        if (Input.GetKey(KeyCode.Space)) {

            Audio.PlayOneShot(jumpAudio);

        }
	}
}
