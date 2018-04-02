using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class RocketProj : NetworkBehaviour
{


    public GameObject explosionEffect;

    public float proximity;
    public float Splashdamage;
    public float CritHitDamage;
    public float splashRadius;
    public RaycastHit hitInfo;
    public Collider[] hitColliders;
   
    private Vector3 midScreen;

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("test");
        var tempexpEffect = Instantiate(explosionEffect, this.transform.position, this.transform.rotation);
        Destroy(tempexpEffect, 1);  

        
        if (collision.collider.tag == "Player")
        {

            Debug.Log("hit Player");

            




            hitColliders = Physics.OverlapSphere(hitInfo.point, splashRadius);
            int i = 0;
            while (i < hitColliders.Length)
            {
                if (hitColliders[i].tag == "Player")
                {

                    proximity = (collision.transform.position - collision.collider.transform.position).magnitude;
                    Splashdamage = CritHitDamage - (proximity / splashRadius);
                    UnityEngine.Debug.Log("Rocket Splash hit a player");
                    CmdPlayerShot(hitColliders[i].name, Splashdamage);
                }

                i++;
            }
        }

    }
    [Command]
    public void CmdPlayerShot(string _playerID, float _damage)
    {

        UnityEngine.Debug.Log(_playerID + " has been shot");
        Player _player = GameManager1.GetPlayer(_playerID);
        _player.RpcTakeDamage(_damage);

        Destroy(this.gameObject);
    }



    private void OnDrawGizmos()
    {

        Gizmos.color = Color.green;

        Gizmos.DrawSphere(hitInfo.point, splashRadius);

    }
}
