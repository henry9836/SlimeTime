using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControlr : MonoBehaviour
{

    private float safezone = 5.0f;
    private float maxZoonOut = 1.0f;
    private Vector3 velocity = Vector3.zero;
    void Update()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        List<GameObject> validPlayers = new List<GameObject>();
        for (int i = 0; i < players.Length; i++)
        {
            if (players[i].GetComponent<PlayerController>().health > 0)
            {
                validPlayers.Add(players[i]);
            }
        }

        Vector3 playersCOM = COM(validPlayers);

        Debug.DrawLine(playersCOM, new Vector3(playersCOM.x, playersCOM.y + 50, playersCOM.z));

        //float camY = 88.6f;
        //float camZ = -87.4f;

        float camY = 170f;
        float camZ = -190f;


        //float camZ = Mathf.Sqrt(Mathf.Abs(((Mathf.Abs(transform.position.z - playersCOM.z))*(Mathf.Abs(transform.position.z - playersCOM.z)))-(camY * camY)));

        transform.LookAt(playersCOM);

        float transitionSpeed = 0.2f;

        //Smooth out the camera movement with Lerp

        //transform.position = new Vector3(playersCOM.x, camY, camZ);

        //transform.position = Vector3.Lerp(transform.position, new Vector3(playersCOM.x, camY, camZ), Time.deltaTime * transitionSpeed); 

        transform.position = Vector3.SmoothDamp(transform.position, new Vector3(playersCOM.x, camY, camZ), ref velocity, transitionSpeed);

        float camFOV = FOV(validPlayers);

        //Smooth out the camera FOV with Lerp

        gameObject.GetComponent<Camera>().orthographicSize = camFOV;

        //gameObject.GetComponent<Camera>().orthographicSize = Mathf.Lerp(gameObject.GetComponent<Camera>().orthographicSize, camFOV, Time.deltaTime * transitionSpeed);

    }

    Vector3 COM(List<GameObject> players)
    {
        float xpos = 0.0f;
        float ypos = 0.0f;
        float zpos = 0.0f;

        for (int i = 0; i < players.Count; i++)
        {
            xpos += players[i].transform.position.x;
        }
        xpos /= players.Count;

        for (int i = 0; i < players.Count; i++)
        {
            ypos += players[i].transform.position.y;
        }
        ypos /= players.Count;

        for (int i = 0; i < players.Count; i++)
        {
            zpos += players[i].transform.position.z;
        }
        zpos /= players.Count;

        return (new Vector3(xpos, ypos, zpos));
    }

    float FOV(List<GameObject> players)
    {
        return (playertoposition(players));
    }

    public Vector3 FindNearestPointOnLine(Vector3 origin, Vector3 direction, Vector3 point)
    {
        direction.Normalize();
        Vector3 lhs = point - origin;
        float dotP = Vector3.Dot(lhs, direction);
        return (origin + direction * dotP);
    }

    public float playertoposition(List<GameObject> players)
    {

        int layerMask = 1 << 12;

        RaycastHit straightHIT;
        Physics.Raycast(gameObject.transform.localPosition, transform.TransformDirection(Vector3.forward), out straightHIT, Mathf.Infinity, layerMask);
        Debug.DrawRay(gameObject.transform.position, transform.TransformDirection(Vector3.forward) * straightHIT.distance, Color.white);

        List<Vector2> pos = new List<Vector2>();
        float zoom = gameObject.GetComponent<Camera>().orthographicSize;
        float xdiff = gameObject.GetComponent<Camera>().orthographicSize * (16.0f / 9.0f);
        float ydiff = (gameObject.GetComponent<Camera>().orthographicSize) * Mathf.Cos(gameObject.transform.rotation.x);
        float zdiff = (gameObject.GetComponent<Camera>().orthographicSize) * Mathf.Sin(gameObject.transform.rotation.x);

        RaycastHit straightHITtr;
        Vector3 position = new Vector3(transform.localPosition.x + xdiff, transform.localPosition.y + ydiff, transform.localPosition.z + zdiff);
        Physics.Raycast(position, transform.TransformDirection(Vector3.forward), out straightHITtr, Mathf.Infinity, layerMask);
        Debug.DrawRay(position, transform.TransformDirection(Vector3.forward) * straightHITtr.distance, Color.red);

        RaycastHit straightHITbl;
        position = new Vector3(transform.localPosition.x - xdiff, transform.localPosition.y - ydiff, transform.localPosition.z - zdiff);
        Physics.Raycast(position, transform.TransformDirection(Vector3.forward), out straightHITbl, Mathf.Infinity, layerMask);
        Debug.DrawRay(position, transform.TransformDirection(Vector3.forward) * straightHITbl.distance, Color.yellow);

        for (int i = 0; i < players.Count; i++)
        {
            if (players[i].GetComponent<PlayerController>().health > 0)
            {
                RaycastHit tempr;
                Physics.Raycast(players[i].transform.position, transform.TransformDirection(Vector3.forward), out tempr, Mathf.Infinity, layerMask);
                Debug.DrawRay(players[i].transform.position, transform.TransformDirection(Vector3.forward) * tempr.distance, Color.cyan);

                pos.Add((new Vector2(tempr.point.x, tempr.point.z) - new Vector2(straightHIT.point.x, straightHIT.point.z)));

                pos[pos.Count - 1] = new Vector2(Mathf.Abs(pos[pos.Count - 1].x), Mathf.Abs(pos[pos.Count - 1].y));
            }
        }


        Vector2 relitivetr = new Vector2(straightHITtr.point.x - straightHIT.point.x , straightHITtr.point.z - straightHIT.point.z);

        bool zoomLock = false;
        bool zoomIn = false;
        bool zoomOut = false;
        bool stay = false;
        for (int i = 0; i < pos.Count; i++)
        {

            //Debug.Log("POS: " + pos[i].x +":" + pos[i].y + ";tr: " + relitivetr);
            if (pos[i].x > relitivetr.x - safezone)
            {
                zoomLock = true;
                zoomOut = true;
                zoomIn = false;
                //Debug.Log("x hit");
                break;
                //TODO: zoooom
            }
            else if (pos[i].y > relitivetr.y - safezone)
            {
                zoomLock = true;
                zoomOut = true;
                zoomIn = false;
                //Debug.Log("Y hit");
                break;
                //TODO: zoooom
            }
            else if ((pos[i].x < (relitivetr.x - safezone)) && (pos[i].y < (relitivetr.y - safezone)))
            {
                zoomLock = true;
                zoomIn = true;
            }
            if (pos[i].y > (relitivetr.y - (safezone *2)))
            {
                stay = true;
            }
            else if (pos[i].x > (relitivetr.x - (safezone * 2)))
            {
                stay = true;
            }
        }


        if (zoomLock)
        {
            if (zoomIn && !zoomOut && !stay)
            {
                zoom -= 5f * Time.deltaTime;
            }
            else if (zoomOut)
            {
                zoom += 5f * Time.deltaTime;
            }
        }
        

        return (zoom);
    }


    //Vector3 temp = FindNearestPointOnLine(lineposition, transform.TransformDirection(Vector3.forward), players[i].transform.position);
    //Debug.DrawLine(temp, players[i].transform.position);


}
