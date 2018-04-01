using System;
using UnityEngine;

public class reloading : MonoBehaviour {
    static Animator anim;
    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        anim.SetBool("isReloading", false);
    }
	
	// Update is called once per frame
	void Update () {
		


	}

    internal void Reloading()
    {

        anim.SetBool("isReloading", true);

    }

    internal void NotReloading()
    {
        anim.SetBool("isReloading", false);
    }
}
