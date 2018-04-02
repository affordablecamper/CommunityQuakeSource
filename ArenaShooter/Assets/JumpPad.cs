using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour {
    private Vector3 motion = Vector3.zero;
    public float thrust;
    public float gravity = 20.00f;
    // Use this for initialization
	void Start () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Player Thrusted");
        var PlayerController = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController>();

        motion.y = thrust;
    
        motion.y -= gravity * Time.deltaTime;
        PlayerController.Move(motion* Time.deltaTime);






    }
}
