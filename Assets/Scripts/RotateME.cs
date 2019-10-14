using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateME : MonoBehaviour
{
    public Vector3 rotDir = new Vector3(0.0f, 1.0f, 0.0f);
    public float rotSpeed = 1.0f;
    void Update()
    {
        transform.Rotate(rotDir * rotSpeed * Time.deltaTime);
    }
}
