using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEjectmanage : MonoBehaviour
{

    public float RandomMovement;
    public float randomForce;
    public float randomMovementX;
    public float randomMovementY;
    private Rigidbody rb;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        Destroy(gameObject, 3);
        RandomMovement =  Random.Range(randomMovementX, randomMovementY);
        randomForce = Random.Range(-1, -2);
        rb.AddRelativeForce(55, randomForce, 0);
        rb.AddRelativeTorque(RandomMovement, RandomMovement, 2);
        rb.AddRelativeForce(Random.Range(-100, 100),
                                        Random.Range(-10, 10),
                                        Random.Range(1500, 500));

        rb.AddRelativeTorque(Random.Range(-50, 50),
                                                Random.Range(-50, 50),
                                                Random.Range(-50, 50));

    }



}
