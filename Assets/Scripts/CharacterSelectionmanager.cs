using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterSelectionmanager : MonoBehaviour
{
    public int playercount = 0;
    public int readycount = 0;
    public bool countdown = false;
    public float countdowntimer = 0.0f;
    public float fadetime = 3.0f;
    void Update()
    {
        playercount = DyanmicControllers.FindControllers();
        readycount = 0;

        for (int i = 0; i < playercount; i++)
        {
            GameObject.Find("Canvas").transform.GetChild(0).transform.GetChild(i).GetComponent<CharacterSelection>().amvalid = true;
            if (GameObject.Find("Canvas").transform.GetChild(0).transform.GetChild(i).GetComponent<CharacterSelection>().isselected == false)
            {
                readycount += 1;
            }
        }
        if (readycount == playercount)
        {
            countdown = true;
        }
        else
        {
            if (countdowntimer <= 0.0f)
            {
                countdowntimer = 0.0f;
            }
            countdowntimer -= Time.deltaTime;
            countdown = false;
            GameObject.Find("timer").GetComponent<Text>().text = "";
        }

        if (countdown == true)
        {
            countdowntimer += Time.deltaTime;
            GameObject.Find("timer").GetComponent<Text>().text = (((fadetime) - countdowntimer) + 1).ToString("0");
            if (countdowntimer >= fadetime)
            {
                SceneManager.LoadScene(2);
            }
        }

        GameObject.Find("FADE").GetComponent<Image>().color = new Color(GameObject.Find("FADE").GetComponent<Image>().color.r, GameObject.Find("FADE").GetComponent<Image>().color.g, GameObject.Find("FADE").GetComponent<Image>().color.b, Mathf.Pow(Mathf.Sin((countdowntimer / fadetime) * (Mathf.PI / 2)), 2));

    }
}
