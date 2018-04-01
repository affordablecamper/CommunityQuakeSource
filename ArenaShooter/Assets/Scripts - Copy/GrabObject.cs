using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabObject : MonoBehaviour {

    public Camera cam;

	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.F)) {

            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100)) {
                Debug.Log(hit.transform);

               

                    

                   Interactable interactable = hit.collider.gameObject.GetComponent<Interactable>();
                    if (interactable != null) {


                        interactable.Interact();

                    
                }


            }


        }

	}
}
