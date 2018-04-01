using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cokeGrab : MonoBehaviour {

    public AudioSource source;
    public AudioClip clip;
    public GameObject cokeObject;
    public bool cokeIsGrabbed;
    public bool canPressE;
    public void Start()
    {
        cokeIsGrabbed = false;
    }


    public void OnTriggerEnter(Collider other)
    {
        
        if (other.tag == "Player")
        {
            canPressE = true;
            

        }

    }

    public void OnTriggerExit(Collider other)
    {
        canPressE = false;
    }


    public void Update()
    {


        if (cokeIsGrabbed == true)
            cokeObject.SetActive(false);

        if (canPressE == true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
               
                cokeIsGrabbed = true;
                source.PlayOneShot(clip);
            }

        }
    }

}
