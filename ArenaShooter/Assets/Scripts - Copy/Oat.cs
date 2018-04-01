using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oat : MonoBehaviour {

    DayNightCycle cycle;
    PlayerInvData data;
    int seedadd;
    public GameObject plant;
    public GameObject cycle1;
    public GameObject cycle2;
    public GameObject cycle3;
    bool harvestable;
    [SerializeField]
    GameObject player;
    public GameObject cycleObj;
    // Use this for initialization
    void Start () {
        cycleObj = GameObject.FindGameObjectWithTag("Sun");
        cycle = cycleObj.GetComponent<DayNightCycle>();

    }
	
	// Update is called once per frame
	void Update () {
        
        if (cycle.days == 1)
        {
            
            cycle1.SetActive(true);


        }

        if (cycle.days == 2)
        {

            cycle1.SetActive(false);
            cycle2.SetActive(true);
        }

        if (cycle.days == 3)
        {
            cycle1.SetActive(false);
            cycle2.SetActive(false);
            cycle3.SetActive(true);
            harvestable = true;
        }

        if (cycle.days > 3)
        {
            Destroy(plant);
        }

        if (harvestable == true)
        {
            player = GameObject.FindGameObjectWithTag("Player").gameObject;
            data = player.GetComponent<PlayerInvData>();
            data.food += 20;
            seedadd = Random.Range(1, 3);
            data.seeds = seedadd;
          

        }
    }
}
