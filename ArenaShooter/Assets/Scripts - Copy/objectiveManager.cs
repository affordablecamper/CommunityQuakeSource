using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectiveManager : MonoBehaviour {

    public cokeGrab coke;
    //public GameObject SpawnEnemys;
    public GameObject cokeUI;
    public GameObject carUI;
    [SerializeField]
    private bool cokeObjective;
    // Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {


        if (coke.cokeIsGrabbed == true)
        {

            cokeObjective = true;
            cokeUI.SetActive(false);


        }

        if (cokeObjective == true) {

            //SpawnEnemys.SetActive(true);
            carUI.SetActive(true);




        }



	}
}
