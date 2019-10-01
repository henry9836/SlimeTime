using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class selectingcharacter : MonoBehaviour
{
    public float timer1 = 0.0f;
    public float timer2 = 0.0f;
    public float timer3 = 0.0f;
    public float timer4 = 0.0f;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "P1")
        {
            timer1 += Time.deltaTime;
            GameObject.Find("playerholder").transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>().fillAmount = (timer1 / 3.0f) * 0.25f;
            GameObject.Find("playerholder").transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<Image>().fillAmount = (timer1 / 3.0f) * 0.25f;
            GameObject.Find("playerholder").transform.GetChild(0).GetChild(0).GetChild(2).GetComponent<Image>().fillAmount = (timer1 / 3.0f) * 0.25f;
            GameObject.Find("playerholder").transform.GetChild(0).GetChild(0).GetChild(3).GetComponent<Image>().fillAmount = (timer1 / 3.0f) * 0.25f;

            if (timer1 >= 3.0f)
            {
                Debug.Log("player 1 set");
                SelectManager.selection = (int)collision.GetComponent<PlayerController>().playerType-1;
                SelectManager.playerchoice = transform.GetSiblingIndex();
            }
        }
        if (collision.tag == "P2")
        {
            timer2 += Time.deltaTime;
            GameObject.Find("playerholder").transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<Image>().fillAmount = (timer2 / 3.0f) * 0.25f;
            GameObject.Find("playerholder").transform.GetChild(1).GetChild(0).GetChild(1).GetComponent<Image>().fillAmount = (timer2 / 3.0f) * 0.25f;
            GameObject.Find("playerholder").transform.GetChild(1).GetChild(0).GetChild(2).GetComponent<Image>().fillAmount = (timer2 / 3.0f) * 0.25f;
            GameObject.Find("playerholder").transform.GetChild(1).GetChild(0).GetChild(3).GetComponent<Image>().fillAmount = (timer2 / 3.0f) * 0.25f;


            if (timer2 >= 3.0f)
            {
                Debug.Log("player 2 set");

                SelectManager.selection = (int)collision.GetComponent<PlayerController>().playerType - 1;
                SelectManager.playerchoice = transform.GetSiblingIndex();
            }
        }
        if (collision.tag == "P3")
        {
            timer3 += Time.deltaTime;
            GameObject.Find("playerholder").transform.GetChild(2).GetChild(0).GetChild(0).GetComponent<Image>().fillAmount = (timer3 / 3.0f) * 0.25f;
            GameObject.Find("playerholder").transform.GetChild(2).GetChild(0).GetChild(1).GetComponent<Image>().fillAmount = (timer3 / 3.0f) * 0.25f;
            GameObject.Find("playerholder").transform.GetChild(2).GetChild(0).GetChild(2).GetComponent<Image>().fillAmount = (timer3 / 3.0f) * 0.25f;
            GameObject.Find("playerholder").transform.GetChild(2).GetChild(0).GetChild(3).GetComponent<Image>().fillAmount = (timer3 / 3.0f) * 0.25f;

            if (timer3 >= 3.0f)
            {
                Debug.Log("player 3 set");

                SelectManager.selection = (int)collision.GetComponent<PlayerController>().playerType - 1;
                SelectManager.playerchoice = transform.GetSiblingIndex();
            }
        }
        if (collision.tag == "P4")
        {
            timer4 += Time.deltaTime;
            GameObject.Find("playerholder").transform.GetChild(3).GetChild(0).GetChild(0).GetComponent<Image>().fillAmount = (timer4 / 3.0f) * 0.25f;
            GameObject.Find("playerholder").transform.GetChild(3).GetChild(0).GetChild(1).GetComponent<Image>().fillAmount = (timer4 / 3.0f) * 0.25f;
            GameObject.Find("playerholder").transform.GetChild(3).GetChild(0).GetChild(2).GetComponent<Image>().fillAmount = (timer4 / 3.0f) * 0.25f;
            GameObject.Find("playerholder").transform.GetChild(3).GetChild(0).GetChild(3).GetComponent<Image>().fillAmount = (timer4 / 3.0f) * 0.25f;

            if (timer4 >= 3.0f)
            {
                Debug.Log("player 4 set");

                SelectManager.selection = (int)collision.GetComponent<PlayerController>().playerType - 1;
                SelectManager.playerchoice = transform.GetSiblingIndex();
            }
        }

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("Goodbye :(");
        if (other.tag == "P1")
        {
            Debug.Log("player 1 unset");
            GameObject.Find("playerholder").transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>().fillAmount = 0.0f;
            GameObject.Find("playerholder").transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<Image>().fillAmount = 0.0f;
            GameObject.Find("playerholder").transform.GetChild(0).GetChild(0).GetChild(2).GetComponent<Image>().fillAmount = 0.0f;
            GameObject.Find("playerholder").transform.GetChild(0).GetChild(0).GetChild(3).GetComponent<Image>().fillAmount = 0.0f;

            SelectManager.selection = (int)other.GetComponent<PlayerController>().playerType - 1;
            SelectManager.playerchoice = 420;
            timer1 = 0.0f;
        }
        if (other.tag == "P2")
        {
            Debug.Log("player 2 unset");
            GameObject.Find("playerholder").transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<Image>().fillAmount = 0.0f;
            GameObject.Find("playerholder").transform.GetChild(1).GetChild(0).GetChild(1).GetComponent<Image>().fillAmount = 0.0f;
            GameObject.Find("playerholder").transform.GetChild(1).GetChild(0).GetChild(2).GetComponent<Image>().fillAmount = 0.0f;
            GameObject.Find("playerholder").transform.GetChild(1).GetChild(0).GetChild(3).GetComponent<Image>().fillAmount = 0.0f;


            SelectManager.selection = (int)other.GetComponent<PlayerController>().playerType - 1;
            SelectManager.playerchoice = 420;

            timer2 = 0.0f;
        }
        if (other.tag == "P3")
        {
            Debug.Log("player 3 unset");
            GameObject.Find("playerholder").transform.GetChild(2).GetChild(0).GetChild(0).GetComponent<Image>().fillAmount = 0.0f;
            GameObject.Find("playerholder").transform.GetChild(2).GetChild(0).GetChild(1).GetComponent<Image>().fillAmount = 0.0f;
            GameObject.Find("playerholder").transform.GetChild(2).GetChild(0).GetChild(2).GetComponent<Image>().fillAmount = 0.0f;
            GameObject.Find("playerholder").transform.GetChild(2).GetChild(0).GetChild(3).GetComponent<Image>().fillAmount = 0.0f;


            SelectManager.selection = (int)other.GetComponent<PlayerController>().playerType - 1;
            SelectManager.playerchoice = 420;

            timer3 = 0.0f;
        }
        if (other.tag == "P4")
        {
            Debug.Log("player 4 unset");
            GameObject.Find("playerholder").transform.GetChild(3).GetChild(0).GetChild(0).GetComponent<Image>().fillAmount = 0.0f;
            GameObject.Find("playerholder").transform.GetChild(3).GetChild(0).GetChild(1).GetComponent<Image>().fillAmount = 0.0f;
            GameObject.Find("playerholder").transform.GetChild(3).GetChild(0).GetChild(2).GetComponent<Image>().fillAmount = 0.0f;
            GameObject.Find("playerholder").transform.GetChild(3).GetChild(0).GetChild(3).GetComponent<Image>().fillAmount = 0.0f;

            SelectManager.selection = (int)other.GetComponent<PlayerController>().playerType - 1;
            SelectManager.playerchoice = 420;

            timer4 = 0.0f;
        }
    }
}
