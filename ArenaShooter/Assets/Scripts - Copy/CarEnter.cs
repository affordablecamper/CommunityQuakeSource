using UnityEngine;
using UnityEngine.Networking;
public class CarEnter : NetworkBehaviour {




    public Behaviour[] carScripts;
    private bool enteredTrig;
    //public Player player;
    public GameObject carCam;
    private bool inCar = false;
    [SerializeField]
    private GameObject gameObjectPlayer;
    [SerializeField]
    private GameObject targetVehicleGameObject;
    private void Start()
    {

       


    }

    void OnTriggerEnter(Collider other)
    {
        

        if (other.gameObject.tag == "Player") {


            gameObjectPlayer = GameObject.FindGameObjectWithTag("Player");

            enteredTrig = true;
            
            
       

        }


    }




    [Command]
    void CmdRequestControlOfVehicle()
    {
        NetworkIdentity targetVehicleNetId = targetVehicleGameObject.GetComponent<NetworkIdentity>();
        targetVehicleNetId.AssignClientAuthority(this.GetComponent<NetworkIdentity>().connectionToClient);

        RpcOnVehicleControlRequestGranted(targetVehicleGameObject);
    }

    [ClientRpc]
    void RpcOnVehicleControlRequestGranted(GameObject targetVehicleGameObject)
    {
        //followcarScript.enabled = true;
        
        gameObjectPlayer.SetActive(false);








        carCam.SetActive(true);
        bool[] wasEnabled;
        wasEnabled = new bool[carScripts.Length];
        for (int i = 0; i < wasEnabled.Length; i++)
        {

            wasEnabled[i] = carScripts[i].enabled = true;

        }








        inCar = true;
    }



    //[Command]
    //void CmdonPlayerEnter() {

        // gameObjectPlayer = GameObject.FindGameObjectWithTag("Player");
        
       // NetworkIdentity targetPlayerNetId = gameObjectPlayer.GetComponent<NetworkIdentity>();
        //targetPlayerNetId.RemoveClientAuthority(gameObjectPlayer.GetComponent<NetworkIdentity>().connectionToClient);




   // }
    
    
    
    
    

    

    private void Update()
    {


        if (Input.GetKeyDown(KeyCode.E) && enteredTrig == true && inCar == false) {
            //CmdonPlayerEnter();
            CmdRequestControlOfVehicle();
            
        }








        if (Input.GetKeyDown(KeyCode.Space) &&inCar == true)
        {



            //followcarScript.enabled = false;
            gameObjectPlayer.SetActive(true);
            






            carCam.SetActive(false);



            bool[] wasEnabled;


            wasEnabled = new bool[carScripts.Length];
            for (int i = 0; i < wasEnabled.Length; i++)
            {
                
                wasEnabled[i] = carScripts[i].enabled = false;

            }




            inCar = false;
        }







    }



    void OnTriggerExit(Collider other)
    {
        
        
        if (other.gameObject.tag == "Player") {
            enteredTrig = false;
            
            

        }


    }












}
