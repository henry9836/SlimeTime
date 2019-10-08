using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchController : MonoBehaviour
{
    Rigidbody rb;

    float launchAngle = 45;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Launch(Vector3 target)
    {

        //Face the target
        Vector3 lookVec = new Vector3(target.x, transform.position.y, target.z);

        transform.LookAt(lookVec);

        //Get Distance To Target
        float distance = Vector3.Distance(transform.position, target);

        // calculate initival velocity required to land the cube on target using the formula (9)
        float Vi = Mathf.Sqrt(distance * -Physics.gravity.y / (Mathf.Sin(Mathf.Deg2Rad * launchAngle * 2)));
        float Vy, Vz;   // y,z components of the initial velocity

        Vy = Vi * Mathf.Sin(Mathf.Deg2Rad * launchAngle);
        Vz = Vi * Mathf.Cos(Mathf.Deg2Rad * launchAngle);

        // create the velocity vector in local space
        Vector3 localVelocity = new Vector3(0f, Vy, Vz);

        // transform it to global vector
        Vector3 globalVelocity = transform.TransformVector(localVelocity);

        // launch the cube by setting its initial velocity
        GetComponent<Rigidbody>().velocity = globalVelocity;


    }
}
