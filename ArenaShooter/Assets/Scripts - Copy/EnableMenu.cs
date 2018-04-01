using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableMenu : MonoBehaviour {

    public GameObject UIMENU;

	// Use this for initialization
	void Start () {

        UIMENU.SetActive(false);
        

    }

    // Update is called once per frame
    void Update () {


        if (Input.GetKey(KeyCode.Tab))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            UIMENU.SetActive(true);

        }
        else {

            UIMENU.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;

        }
      

	}
}
