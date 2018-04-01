using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSway : MonoBehaviour {

    public float tiltAngle;
    public float SmoothRotation = 2;
    public float Smooth = 3;
    public float maxAmount;
    public float amount;
    private Vector3 def;
 
    void Awake()
    {
        def = transform.localPosition;
    }

    void FixedUpdate()
    {
        
        float factorX = -Input.GetAxis("Mouse X") * amount;
        float factorY = -Input.GetAxis("Mouse Y") * amount;
        if (factorX > maxAmount)
            factorX = maxAmount;

        if (factorX < -maxAmount)
            factorX = -maxAmount;

        if (factorY > maxAmount)
            factorY = maxAmount;

        if (factorY < -maxAmount)
            factorY = -maxAmount;


        Vector3 Final = new Vector3(def.x + factorX, def.y + factorY, def.z);
        transform.localPosition = Vector3.Lerp(transform.localPosition, Final, Time.deltaTime * Smooth);


        float tiltAroundZ = Input.GetAxis("Mouse Y") * tiltAngle;
        float tiltAroundX = Input.GetAxis("Mouse X") * tiltAngle;
        var target = Quaternion.Euler(tiltAroundX, 0, tiltAroundZ);
        transform.localRotation = Quaternion.Slerp(transform.localRotation, target, Time.deltaTime * SmoothRotation);
    }
}
