using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class portalCam : MonoBehaviour {
    [SerializeField]
    private Transform playerCam;
    public Transform portal;
    public Transform otherPortal;
    private int length;
    


    // Update is called once per frame
    void Update () {

        if (playerCam == null)
            length += 1;


        for (int i = 0; i <length ; i--)
        {
            playerCam = GameObject.FindGameObjectWithTag("MainCamera").transform;
        }



        Vector3 playerOffsetFromPortal = playerCam.position = otherPortal.position;
        transform.position = portal.position + playerOffsetFromPortal;

	}
}
