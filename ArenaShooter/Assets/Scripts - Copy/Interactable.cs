using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour {

    public float radius;

    private void OnDrawGizmos()
    {

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);



    }

    public virtual void Interact() {

        Debug.Log("Player is interacting with " + transform.name);

    }

}
