using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerManager : MonoBehaviour
{

    #region Singleton

    public static playerManager instance;


    private void Awake()
    {

        instance = this;

    }

    #endregion
    int length;
    public GameObject __player;


    private void Start()
   {

        if (__player == null)
            //length += 1;


        for (int i = 0; i < length; i--)
       {
            
      }

    }
}

