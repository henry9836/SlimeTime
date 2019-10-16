using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public GameObject mainCamera;
    public GameObject bar;
    public List<GameObject> barsList; 

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GameObject.Find("Main Camera");
    }

    // Update is called once per frame
    void Update()
    {

        GameObject[] validPlayers = GameObject.FindGameObjectsWithTag("Player");
        List<GameObject> Players = new List<GameObject>();
        for (int i = 0; i < validPlayers.Length; i++)
        {
            if (validPlayers[i].GetComponent<PlayerController>().health > 0)
            {
                Players.Add(validPlayers[i]);
                barsList.Add(bar);
            }
        }

        for (int i = 0; i < barsList.Count; i++)
        {
            gameObject.transform.GetChild(7).GetChild(i).gameObject.SetActive(true);
        }


        for (int i = 0; i < Players.Count; i++)
        {
            int layerMask = 1 << 12;
            RaycastHit playerpos;
            Physics.Raycast(Players[i].gameObject.transform.position, GameObject.Find("Main Camera").transform.TransformDirection(Vector3.forward), out playerpos, Mathf.Infinity, layerMask);
            Debug.DrawLine(Players[i].transform.position, playerpos.point, Color.cyan);

            Vector3 dir = new Vector3(playerpos.point.x, playerpos.point.z, 0.0f);

            barsList[i].GetComponent<RectTransform>().localPosition = dir;
        }

        //transform.LookAt(mainCamera.transform.position);

    }
}
