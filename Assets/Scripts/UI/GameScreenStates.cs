using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GameScreenStates : MonoBehaviour
{
    public int screenState = -1;
    private int screenStateCur = -1;

    public GameObject screensPlay;
    public GameObject screensGrace;
    public GameObject screensWave;

    public GameObject[] screens;

    void Start()
    {
        screenState = 0;
        screenStateCur = 0;
    }

    void Update()
    {
        if (screenState != screenStateCur)
        {
            screens[screenStateCur].SetActive(false);
            screens[screenState].SetActive(true);

            screenStateCur = screenState;
        }
    }
}
