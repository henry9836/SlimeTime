using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LAZERNODE : MonoBehaviour
{
    public struct ScanInfo {
        public Vector3 postion;
        public string collisionTag;
    };
    public ScanInfo Scan()
    {
        ScanInfo result = new ScanInfo();

        result.postion = new Vector3(0.0f,0.0f,0.0f);
        result.collisionTag = "Unknown";


        return (result);
    }
}
