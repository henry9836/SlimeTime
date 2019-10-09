using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControlr : MonoBehaviour
{


    void Update()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");



        Vector3 playersCOM = COM(players);

        transform.position = new Vector3(playersCOM.x, 20.0f, playersCOM.z - 25.0f);

        float camFOV = FOV(players);

        gameObject.GetComponent<Camera>().orthographicSize = camFOV;

       

    }

    Vector3 COM(GameObject[] players)
    {
        float xpos = 0.0f;
        float ypos = 0.0f;
        float zpos = 0.0f;

        for (int i = 0; i < players.Length; i++)
        {
            xpos += players[i].transform.position.x;
        }
        xpos /= players.Length;

        for (int i = 0; i < players.Length; i++)
        {
            ypos += players[i].transform.position.y;
        }
        ypos /= players.Length;

        for (int i = 0; i < players.Length; i++)
        {
            zpos += players[i].transform.position.z;
        }
        zpos /= players.Length;

        return (new Vector3(xpos, ypos, zpos));
    }

    float FOV(GameObject[] players)
    {
        float xdiff = gameObject.GetComponent<Camera>().orthographicSize * (16.0f / 9.0f);
        float ydiff = (gameObject.GetComponent<Camera>().orthographicSize) * Mathf.Cos(gameObject.transform.rotation.x);
        float zdiff = (gameObject.GetComponent<Camera>().orthographicSize) * Mathf.Sin(gameObject.transform.rotation.x);

        RaycastHit straightHIT;
        Physics.Raycast(gameObject.transform.localPosition, transform.TransformDirection(Vector3.forward), out straightHIT, Mathf.Infinity);
        Debug.DrawRay(gameObject.transform.position, transform.TransformDirection(Vector3.forward) * straightHIT.distance, Color.white);

        float shortestpos = playertoposition(transform.position, players);

        /// code for raycasting off FOV lines 

        RaycastHit straightHITtr;
        Vector3 position = new Vector3(transform.localPosition.x + xdiff, transform.localPosition.y + ydiff, transform.localPosition.z + zdiff);
        Physics.Raycast(position, transform.TransformDirection(Vector3.forward), out straightHITtr, Mathf.Infinity);
        Debug.DrawRay(position, transform.TransformDirection(Vector3.forward) * straightHITtr.distance, Color.red);


        RaycastHit straightHITtl;
        position = new Vector3(transform.localPosition.x - xdiff, transform.localPosition.y + ydiff, transform.localPosition.z + zdiff);
        Physics.Raycast(position, transform.TransformDirection(Vector3.forward), out straightHITtl, Mathf.Infinity);
        Debug.DrawRay(position, transform.TransformDirection(Vector3.forward) * straightHITtl.distance, Color.green);



        RaycastHit straightHITbr;
        position = new Vector3(transform.localPosition.x + xdiff, transform.localPosition.y - ydiff, transform.localPosition.z - zdiff);
        Physics.Raycast(position, transform.TransformDirection(Vector3.forward), out straightHITbr, Mathf.Infinity);
        Debug.DrawRay(position, transform.TransformDirection(Vector3.forward) * straightHITbr.distance, Color.blue);



        RaycastHit straightHITbl;
        position = new Vector3(transform.localPosition.x - xdiff, transform.localPosition.y - ydiff, transform.localPosition.z - zdiff);
        Physics.Raycast(position, transform.TransformDirection(Vector3.forward), out straightHITbl, Mathf.Infinity);
        Debug.DrawRay(position, transform.TransformDirection(Vector3.forward) * straightHITbl.distance, Color.yellow);









        return (7.0f);
    }

    public Vector3 FindNearestPointOnLine(Vector3 origin, Vector3 direction, Vector3 point)
    {
        direction.Normalize();
        Vector3 lhs = point - origin;
        float dotP = Vector3.Dot(lhs, direction);
        return (origin + direction * dotP);
    }

    public float playertoposition(Vector3 lineposition, GameObject[] players)
    {
        float Xlongest = Mathf.Infinity;
        float Ylongest = Mathf.Infinity;

        for (int i = 0; i < players.Length; i++)
        {
            Vector3 temp = FindNearestPointOnLine(lineposition, transform.TransformDirection(Vector3.forward), players[i].transform.position);
            Debug.DrawLine(temp, players[i].transform.position);

            //if ()
            //if (Vector3.Distance(temp, players[i].transform.position) > shortdistance)
            //{
            //    shortdistance = Vector3.Distance(temp, players[i].transform.position);
            //}
        }


        return (0.0f);
    }




}
