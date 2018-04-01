using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deathscript : MonoBehaviour {

	// Use this for initialization
	void Awake () {
        Destroy(gameObject, 30);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
