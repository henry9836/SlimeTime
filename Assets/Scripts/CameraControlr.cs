using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControlr : MonoBehaviour
{


    void Update()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        RaycastHit straightHIT;
        Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out straightHIT, Mathf.Infinity);
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * straightHIT.distance, Color.red);


        RaycastHit player1HIT;
        Physics.Linecast(transform.position, players[0].GetComponent<Renderer>().bounds.center, out player1HIT);
        Debug.DrawLine(transform.position, players[0].GetComponent<Renderer>().bounds.center, Color.red);

        RaycastHit player1HITTOStraight;
        Physics.Linecast(straightHIT.point, players[0].GetComponent<Renderer>().bounds.center, out player1HITTOStraight);
        Debug.DrawLine(straightHIT.point, players[0].GetComponent<Renderer>().bounds.center, Color.red);

        float z = straightHIT.distance;
        float y = player1HIT.distance;
        float x = player1HITTOStraight.distance;

        float angle = (Mathf.Acos((Mathf.Pow(z, 2) + Mathf.Pow(y, 2) - Mathf.Pow(x, 2)) / (2 * z * y)) * 180) / 3.1415926f;

        Debug.Log(angle);
    }
}
