using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class WalkAnim : NetworkBehaviour {

    public Animator anim;

	// Use this for initialization
	void Start () {
        




    }
	
	// Update is called once per frame
	void Update () {





        //forwards
        if (!Input.GetKey(KeyCode.W))
        {
            anim.SetBool("isIdle", true);
            anim.SetBool("isWalkingForward", false);
            anim.SetBool("isWalkingBackwards", false);
            anim.SetBool("isWalkingLeft", false);
            anim.SetBool("isWalkingRight", false);
            anim.SetBool("isJumping", false);

        }


        //left
        if (!Input.GetKey(KeyCode.A))
        {

            anim.SetBool("isIdle", true);
            anim.SetBool("isWalkingForward", false);
            anim.SetBool("isWalkingBackwards", false);
            anim.SetBool("isWalkingLeft", false);
            anim.SetBool("isWalkingRight", false);
            anim.SetBool("isJumping", false);

        }


        //right
        if (!Input.GetKey(KeyCode.D))
        {
            anim.SetBool("isIdle", true);
            anim.SetBool("isWalkingForward", false);
            anim.SetBool("isWalkingBackwards", false);
            anim.SetBool("isWalkingLeft", false);
            anim.SetBool("isWalkingRight", false);
            anim.SetBool("isJumping", false);
        }



        //backwards
        if (!Input.GetKey(KeyCode.S))
        {

            anim.SetBool("isIdle", true);
            anim.SetBool("isWalkingForward", false);
            anim.SetBool("isWalkingBackwards", false);
            anim.SetBool("isWalkingLeft", false);
            anim.SetBool("isWalkingRight", false);
            anim.SetBool("isJumping", false);

        }






        //forwards
        if (Input.GetKey(KeyCode.W))
        {

            anim.SetBool("isIdle", false);
            anim.SetBool("isWalkingForward", true);
            anim.SetBool("isWalkingBackwards", false);
            anim.SetBool("isWalkingLeft", false);
            anim.SetBool("isWalkingRight", false);
            anim.SetBool("isJumping", false);

        }


        //left
        if (Input.GetKey(KeyCode.A))
        {


            anim.SetBool("isIdle", false);
            anim.SetBool("isWalkingForward", false);
            anim.SetBool("isWalkingBackwards", false);
            anim.SetBool("isWalkingLeft", true);
            anim.SetBool("isWalkingRight", false);
            anim.SetBool("isJumping", false);
        }


        //right
        if (Input.GetKey(KeyCode.D))
        {


            anim.SetBool("isIdle", false);
            anim.SetBool("isWalkingForward", false);
            anim.SetBool("isWalkingBackwards", false);
            anim.SetBool("isWalkingLeft", false);
            anim.SetBool("isWalkingRight", true);
            anim.SetBool("isJumping", false);
        }



        //backwards
        if (Input.GetKey(KeyCode.S))
        {

            anim.SetBool("isIdle", false);
            anim.SetBool("isWalkingForward", false);
            anim.SetBool("isWalkingBackwards", true);
            anim.SetBool("isWalkingLeft", false);
            anim.SetBool("isWalkingRight", false);
            anim.SetBool("isJumping", false);

        }

      //  if (Input.GetKey(KeyCode.Space))
      //  {

         //   anim.SetBool("isIdle", false);
         //   anim.SetBool("isWalkingForward", false);
         //   anim.SetBool("isWalkingBackwards", false);
         //   anim.SetBool("isWalkingLeft", false);
         //   anim.SetBool("isWalkingRight", false);
          //  anim.SetBool("isJumping", true);

       // }

        //if (!Input.GetKey(KeyCode.Space))
       // {
           // anim.SetBool("isIdle", true);
            //anim.SetBool("isWalkingForward", false);
           // anim.SetBool("isWalkingBackwards", false);
           // anim.SetBool("isWalkingLeft", false);
           // anim.SetBool("isWalkingRight", false);
           // anim.SetBool("isJumping", false);

       // }


    }
}
