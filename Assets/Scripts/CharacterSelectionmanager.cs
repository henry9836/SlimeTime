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
            GameObject.Find("players").transform.GetChild(i).GetComponent<CharacterSelection>().amvalid = true;
            if (GameObject.Find("players").transform.GetChild(i).GetComponent<CharacterSelection>().isselected == false)
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
            else
            {
                countdowntimer -= Time.deltaTime;

            }
            countdown = false;


        }

        if (countdown == true)
        {
            countdowntimer += Time.deltaTime;
            if (countdowntimer >= fadetime)
            {
                for (int i = 0; i < playercount; i++)
                {
                   characterSetter.playerSelections.Add(GameObject.Find("players").transform.GetChild(i).GetComponent<CharacterSelection>().currentSelection);
                }
                SceneManager.LoadScene(2);
            }
        }

        if (countdowntimer != 0.0f)
        {
            GameObject.Find("timer").GetComponent<Text>().text = Mathf.CeilToInt(((fadetime) - countdowntimer)).ToString("0");
        }
        else
        {
            GameObject.Find("timer").GetComponent<Text>().text = "";

        }

        GameObject.Find("FADE").GetComponent<Image>().color = new Color(GameObject.Find("FADE").GetComponent<Image>().color.r, GameObject.Find("FADE").GetComponent<Image>().color.g, GameObject.Find("FADE").GetComponent<Image>().color.b, Mathf.Pow(Mathf.Sin((countdowntimer / fadetime) * (Mathf.PI / 2)), 2));

    }
}
