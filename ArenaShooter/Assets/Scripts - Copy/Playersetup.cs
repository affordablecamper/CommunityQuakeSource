using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


[RequireComponent(typeof(Player))]

public class Playersetup : NetworkBehaviour {
    [SerializeField]
    Behaviour[] componentstoDisable;
    [SyncVar]
    
    [SerializeField]
    string remoteLayerName = "RemotePlayer";
    [SerializeField]
    string remoteworldModel = "WorldModel";
    [SerializeField]
    string viewworldModel = "ViewModel";
    [SerializeField]
    public GameObject  _worldModel;
    public bool _charDead;
    public GameObject _viewModel;
    public CharacterController playerController;
    //[SerializeField]
   // GameObject _Weapon;
    void Start () {
        

        if (!isLocalPlayer) {
            AssignRemoteLayer();
            DisableComponents();
            
            ChangeLayersRemote(_worldModel, LayerMask.NameToLayer(remoteworldModel));
            ChangeLayersRemote(_viewModel, LayerMask.NameToLayer("CantSee"));
          
            ChangeLayers1(_viewModel, LayerMask.NameToLayer("CantSee"));
           
        }

        if (isLocalPlayer) {


           
            AssignLocalLayer();
           
            ChangeLayers(_viewModel, LayerMask.NameToLayer(viewworldModel));
            ChangeLayersRemote(_worldModel, LayerMask.NameToLayer("CantSeeWorldModel"));
           
        }



        GetComponent<Player>().Setup();

    }



    public static void ChangeLayersLocal(GameObject go, int layer)
    {
        go.layer = layer;
        foreach (Transform child in go.transform)
        {
            ChangeLayers(child.gameObject, layer);
        }
    }



    public static void ChangeLayersRemote(GameObject go, int layer)
    {
        go.layer = layer;
        foreach (Transform child in go.transform)
        {
            ChangeLayers(child.gameObject, layer);
        }
    }


    public static void ChangeLayers(GameObject go, int layer)
    {
        go.layer = layer;
        foreach (Transform child in go.transform)
        {
            ChangeLayers(child.gameObject, layer);
        }
    }




    public static void ChangeLayers1(GameObject go, int layer)
    {
        go.layer = layer;
        foreach (Transform child in go.transform)
        {
            ChangeLayers(child.gameObject, layer);
        }
    }


    private void AssignLocalLayer()
    {



        //_viewModel.layer = LayerMask.NameToLayer(viewworldModel);
        _worldModel.layer = LayerMask.NameToLayer("CantSeeWorldModel");
        //_Weapon.layer = LayerMask.NameToLayer(viewworldModel);
    }

    public override void OnStartClient()
    {
        base.OnStartClient();

        string _netID = GetComponent<NetworkIdentity>().netId.ToString();
        Player _player = GetComponent<Player>();
        GameManager1.RegisterPlayer(_netID, _player);
        
    }

    void DisableComponents () {

        _charDead = true;
        for (int i = 0; i < componentstoDisable.Length; i++)
        {
            
            componentstoDisable[i].enabled = false;

        }

    }

   // public override void PreStartClient()
   // {
       // base.PreStartClient();
        //playerAnim.GetComponent<NetworkAnimator>().SetParameterAutoSend(0, true);
    //}

    void OnDisable()
    {

        GameManager1.UnRegisterPlayer(transform.name);


    }

    void AssignRemoteLayer() {

        gameObject.layer = LayerMask.NameToLayer(remoteLayerName);
       
        
    }


    public static void ChangeLayersRemotee(GameObject go, int layer)
    {
        go.layer = layer;
        foreach (Transform child in go.transform)
        {
            ChangeLayers(child.gameObject, layer);
        }
    }


    // Update is called once per frame
    void Update () {

        if (_charDead)
        {

            playerController.enabled = false;
        }

    }
}
