using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GameScreenStates : MonoBehaviour
{
    public int screenState = -1;
    private int screenStateCur = -1;

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
            if (screenStateCur >= 0)
            { // Do not allow array index out of range. Deactivate old screen
                screens[screenStateCur].SetActive(false);
            }

            // Activate new screen
            screens[screenState].SetActive(true);

            screenStateCur = screenState;
        }
    }
}
