using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GameScreenStates : MonoBehaviour
{

    private GameObject gameManagerObject;
    private GameManager gameManager;

    private bool isNull = true;

    public CanvasGroup tutorial;
    public int tutorialState = 0;
    public int tutorialStateCur = 0;
    public float tutorialTimer = 13.0f;

    public int screenState = -1;
    private int screenStateCur = -1;
    // 0 - Normal gameplay with slime counter
    // 1 - Wave end text
    // 2 - Next wave countdown
    // 3 - Wave start text

    public GameObject[] screens;

    void Start()
    {
        if (gameManagerObject == null)
        {
            gameManagerObject = GameObject.Find("GameManager");
        }

        if (gameManagerObject != null)
        {
            isNull = false;
            gameManager = gameManagerObject.GetComponent<GameManager>();
        }

        tutorial = transform.Find("Tutorials").GetComponent<CanvasGroup>();

        screenState = 0;
        screenStateCur = 0;
    }

    void Update()
    {
        // Tutorials
        tutorialTimer -= Time.deltaTime;
        tutorialTimer = Mathf.Clamp(tutorialTimer, 0, 13.0f);


        if (tutorialState != tutorialStateCur)
        {
            switch (tutorialState)
            {
                case 1:
                    tutorial.DOFade(1f, 0.3f);
                    break;

                case 2:
                    tutorial.DOFade(0f, 0.3f);
                    break;
            }

            tutorialStateCur = tutorialState;
        }
        else
        {
            if (tutorialTimer <= 10 && tutorialState == 0)
            {
                tutorialState = 1;
            }

            if (tutorialTimer <= 0 && tutorialState == 1)
            {
                tutorialState = 2;
            }
        }


        // Game screens
        if (!isNull)
        {
            if (screenState != screenStateCur)
            {
                // Deactivate old screen
                switch (screenStateCur)
                {
                    case 0:
                        screens[0].SetActive(false);
                        break;

                    case 1:
                        screens[1].GetComponent<WaveComplete>().End();
                        break;

                    case 2:
                        screens[2].GetComponent<WaveCountdown>().End();
                        break;

                }

                switch (screenState)
                {
                    case 0:
                        screens[0].SetActive(true);
                        break;

                    case 1:
                        screens[1].GetComponent<WaveComplete>().Begin();
                        break;

                    case 2:
                        screens[2].GetComponent<WaveCountdown>().Begin();
                        break;
                }

                screenStateCur = screenState;
            }
            else
            {
                if (screenState == 0 && gameManager.remainingSpawn <= 0)
                {
                    screenState = 1;
                }

                if (screenState == 1 && gameManager.gracetimer <= gameManager.grace - 3f)
                {
                    screenState = 2;
                }

                if (screenState == 2 && gameManager.gracetimer <= 0.1f)
                {
                    screenState = 0;
                }
            }
        }
       
    }
}
