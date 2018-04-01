using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class NetworkLevelChange : MonoBehaviour
{

    public NetworkManager nW;
    private void OnTriggerEnter(Collider other)
    {

        NetworkManager.singleton.ServerChangeScene("Level1");
    }


}
