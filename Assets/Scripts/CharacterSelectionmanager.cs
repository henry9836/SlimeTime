using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterSelectionmanager : MonoBehaviour
{
    //aracher, mager, worrier, bard

    public int playercount = 0;
    public int readycount = 0;
    public bool countdown = false;
    public float countdowntimer = 0.0f;
    public float fadetime = 3.0f;
    public int disabledCount = 4;

    void Update()
    {
        playercount = DyanmicControllers.FindControllers();
        disabledCount = 4 - playercount;
        readycount = 0;

        for (int i = 0; i < playercount; i++)
        {
            if (GameObject.Find("players").transform.GetChild(i).GetComponent<CharacterSelection>().holdTimer >= 3.0)
            {
                readycount += 1;
            }
        }

        if (readycount == playercount)
        {
            countdown = true;
        }

        for (int i = 0; i < disabledCount; i++)
        {
            GameObject.Find("players").transform.GetChild(3 - i).GetComponent<Image>().color = new Color(0.0f, 0.0f, 0.0f, 0.5f);
            GameObject.Find("players").transform.GetChild(3 - i).GetComponent<CharacterSelection>().holdTimer = 3.0f;

        }

        if (countdown == true)
        {
            for (int i = 0; i < playercount; i++)
            {
               GameObject.Find("players").transform.GetChild(i).GetComponent<CharacterSelection>().holdTimer = 3.0f;
 
            }

            countdowntimer += Time.deltaTime;
            if (countdowntimer >= fadetime)
            {
                for (int i = 0; i < playercount; i++)
                {
                   characterSetter.playerSelections.Add(GameObject.Find("players").transform.GetChild(i).GetComponent<CharacterSelection>().currentSelection);
                }
                //SceneManager.LoadScene(2);
                FindObjectOfType<SceneSwitcher>().SceneSwitch("Game");
            }
        }

        //fade
        //GameObject.Find("FADE").GetComponent<Image>().color = new Color(GameObject.Find("FADE").GetComponent<Image>().color.r, GameObject.Find("FADE").GetComponent<Image>().color.g, GameObject.Find("FADE").GetComponent<Image>().color.b, Mathf.Pow(Mathf.Sin((countdowntimer / fadetime) * (Mathf.PI / 2)), 2));

    }
}
