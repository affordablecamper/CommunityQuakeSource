using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enableLvlCam : MonoBehaviour
{

    public GameObject lvlChangeCam;
    






    public void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Player")
        {

            lvlChangeCam.SetActive(true);



        }

        
    }

    public void OnTriggerExit(Collider other)
    {
        Debug.Log("exit");

        if (other.gameObject.tag == "Player")
        {
            lvlChangeCam.SetActive(false);




        }

    }




}


        


