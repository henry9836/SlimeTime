using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InidactorSystem : MonoBehaviour
{
    public List<GameObject> arrows;
    public float radius = 10.0f;

    private void FixedUpdate()
    {
        //Disable visile for all arrows
        for (int i = 0; i < arrows.Count; i++)
        {
            arrows[i].GetComponent<Image>().enabled = false;
            arrows[i].GetComponent<RectTransform>().localPosition = Vector3.zero;
        }

        //If we have less slimes than arrows
        if (GameObject.FindGameObjectsWithTag("BULLETIGNORESLIME").Length < (arrows.Count + 1) && GetComponent<GameManager>().remainingSpawn < (arrows.Count + 1))
        {
            //Get the slime objects
            GameObject[] slimes = GameObject.FindGameObjectsWithTag("BULLETIGNORESLIME");

            //Get info from the camera
            Vector3 bl = GameObject.Find("Main Camera").GetComponent<CameraControlr>().bl;
            Vector3 tr = GameObject.Find("Main Camera").GetComponent<CameraControlr>().tr;

            //for each slime
            for (int i = 0; i < slimes.Length; i++)
            {
                //Raycast a point onto thing

                int layerMask = 1 << 12;
                RaycastHit slimeRay;
                Physics.Raycast(slimes[i].transform.position, GameObject.Find("Main Camera").transform.TransformDirection(Vector3.forward), out slimeRay, Mathf.Infinity, layerMask);
                Debug.DrawLine(slimes[i].transform.position, slimeRay.point, Color.cyan);

                //Outside Camera

                Debug.Log("my Pos is:" + slimeRay.point + " the camera tr: " + tr + " bl: " + bl);

                if (slimeRay.point.x < bl.x || slimeRay.point.z < bl.z || slimeRay.point.x > tr.x || slimeRay.point.z > tr.z)
                {
                    
                    //Get Direction Vector of player COM to Slime
                    Vector3 dir = new Vector3(GameObject.Find("Main Camera").GetComponent<CameraControlr>().playersCOM.x - slimes[i].transform.position.x, GameObject.Find("Main Camera").GetComponent<CameraControlr>().playersCOM.z - slimes[i].transform.position.z, 0.0f).normalized;
                
                    //The above is backwards so *= -1;
                    dir *= -1;


                    //Move arrow to position based on dir
                    arrows[i].GetComponent<RectTransform>().localPosition += dir * radius;

                    //Look at dir
                    float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                    arrows[i].GetComponent<RectTransform>().localRotation = Quaternion.AngleAxis(angle, Vector3.forward);

                    ////Draw arrow
                    arrows[i].GetComponent<Image>().enabled = true;

                }
            }
        }

    }
}
