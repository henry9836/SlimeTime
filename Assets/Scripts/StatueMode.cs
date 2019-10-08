using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatueMode : MonoBehaviour
{

    private Transform statue;

    // Start is called before the first frame update
    void Start()
    {
        statue.position = transform.position;
        statue.rotation = transform.rotation;

        statue.localPosition = transform.localPosition;
        statue.localRotation = transform.localRotation;
        statue.localScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = statue.position;
        transform.rotation = statue.rotation;

        transform.localPosition = statue.localPosition;
        transform.localRotation = statue.localRotation;
        transform.localScale = statue.localScale;
    }
}
