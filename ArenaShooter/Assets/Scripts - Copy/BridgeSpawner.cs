using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeSpawner : MonoBehaviour {

    public GameObject[] theBridge;
    public Transform player;
    public float spawnZ = 0.0f;
    public float tileLength = 12.0f;
    public int amnTilesOnScreen = 1;
    private float safeZone = 63f;
    private float timer;
    private float time = 10f;
    private List<GameObject> activeTiles;
    // Use this for initialization
	void Start () {

        player = GameObject.FindGameObjectWithTag("Player").transform;
            


	}

    // Update is called once per frame
    void Update() {


        if (timer <= 0) {


            DeleteTile();
            timer = time;
        }
        timer = Time.deltaTime;
        if (player.position.z - safeZone > (spawnZ - amnTilesOnScreen * tileLength)) {


            SpawnTile();
            
        }
        

    }

    void SpawnTile() {
        activeTiles = new List<GameObject>();
        GameObject go;
        go = Instantiate(theBridge[0]) as GameObject;
        go.transform.SetParent(transform);
        go.transform.position = Vector3.forward * spawnZ;
        spawnZ += tileLength;
        activeTiles.Add(go);

    }
    private void DeleteTile() {

        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0); 

    }
}
