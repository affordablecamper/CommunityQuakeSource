using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle : MonoBehaviour {


    public float time;
    public Transform SunTransform;
    public Light Sun;
    public TimeSpan currenttime;
    public int days;
    public int speed;
    

    private void FixedUpdate()
    {

        ChangeTime();

    }

    private void ChangeTime()
    {
        time += Time.deltaTime * speed;
        if (time >= 86400) {

            days += 1;
            time = 0;

        }
        currenttime = TimeSpan.FromSeconds(time);
        SunTransform.rotation = Quaternion.Euler(new Vector3((time - 21600)/86400*360,0,0));
    }



    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
