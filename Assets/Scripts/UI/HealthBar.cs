using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    public GameObject player;


    private void Start()
    {
        gameObject.GetComponent<CanvasGroup>().alpha = 0.5f;
    }

    private void FixedUpdate()
    {
        //get hp
        float hp = player.GetComponent<PlayerController>().health;
        //select health bar according which player we are
        GameObject.Find("Healthbars").transform.GetChild((int)player.GetComponent<PlayerController>().charcterType).gameObject.GetComponent<Image>().fillAmount = hp / 100.0f;

        //Debug.Log("player is: " + player.name + " and his cntroller is: " + player.GetComponent<PlayerController>().controllerNotBound + " I think player num is: " + player.GetComponent<PlayerController>().playerType);
        //If our player is valid
        if (!player.GetComponent<PlayerController>().controllerNotBound)
        {
            //Debug.Log("Setting player " + player.GetComponent<PlayerController>().playerType + "'s image which is: " + GameObject.Find("Healthbars").transform.GetChild((int)player.GetComponent<PlayerController>().charcterType).gameObject.name);
            //this.gameObject.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, gameObject.GetComponent<Image>().color.a);
            //make image trans
            GameObject.Find("Healthbars").transform.GetChild((int)player.GetComponent<PlayerController>().charcterType).gameObject.GetComponent<CanvasGroup>().alpha = 1.0f;
        }
        else
        {
            gameObject.GetComponent<CanvasGroup>().alpha = 0.5f;

        }

        if (hp <= 0f)
        {
            //gameObject.GetComponent<CanvasGroup>().alpha = 0.5f;
        }
    }

    //public GameObject mainCamera;
    //public GameObject bar;
    //public List<GameObject> barsList;
    //public int validPlayers;
    //public List<GameObject> Players;

    //void Start()
    //{
    //    DyanmicControllers.FindControllers();

    //    mainCamera = GameObject.Find("Main Camera");

    //    validPlayers = DyanmicControllers.controlNum;
    //    Players = new List<GameObject>();

    //    for (int i = 0; i < validPlayers; i++)
    //    {
    //        if (GameObject.Find("players").transform.GetChild(i).GetComponent<PlayerController>().health > 0)
    //        {
    //            Players.Add(GameObject.Find("players").transform.GetChild(i).gameObject);
    //            barsList.Add(gameObject.transform.GetChild(7).GetChild(i).gameObject);
    //            Debug.Log("hp added");
    //        }
    //    }

    //    for (int i = 0; i < barsList.Count; i++)
    //    {
    //        barsList[i].SetActive(true);
    //    }
    //}

    //void Update()
    //{

    //    for (int i = 0; i < Players.Count; i++)
    //    {
    //        int layerMask = 1 << 12;
    //        RaycastHit playerpos;
    //        Physics.Raycast(Players[i].gameObject.transform.position, GameObject.Find("Main Camera").transform.TransformDirection(Vector3.forward), out playerpos, Mathf.Infinity, layerMask);
    //        Debug.DrawLine(Players[i].transform.position, playerpos.point, Color.cyan);

    //        Vector3 position = new Vector3(playerpos.point.x, playerpos.point.z, 0.0f).normalized;

    //        barsList[i].GetComponent<RectTransform>().localPosition = position * GameObject.Find("Main Camera").GetComponent<Camera>().orthographicSize;
    //    }

    //    //transform.LookAt(mainCamera.transform.position);

    //}
}
