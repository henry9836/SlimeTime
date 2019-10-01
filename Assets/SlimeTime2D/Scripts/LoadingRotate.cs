using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingRotate : MonoBehaviour
{
    public float speed;

    void Update()
    {
        this.transform.Rotate(new Vector3(0.0f, 0.0f, Time.deltaTime * speed));
    }
}
