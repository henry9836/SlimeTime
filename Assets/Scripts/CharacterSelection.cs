﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelection : MonoBehaviour
{
    public float holdTimer = 0.0f;
    public float holdlengh = 3.0f;
    public bool lockedIn = false;
    public AudioClip lockSound;
    private bool lockSoundPlayed;
    public int currentSelection = 10;
    public GameObject attachedPlayer;

    public enum PLAYERin
    {
        UNASSIGNED,
        PLAYER1,
        PLAYER2,
        PLAYER3,
        PLAYER4
    }

    public PLAYERin playerType = PLAYERin.UNASSIGNED;

    void Start()
    {
        bool playable = false;
        //0-3
        while (playable == false)
        {
            playable = true;
            currentSelection = Random.Range(0,4);

            for (int i = 0; i < 4; i++)
            {
                if (GameObject.Find("Canvas").transform.GetChild(1).GetChild(i).GetComponent<CharacterSelection>().playerType != this.playerType)
                {
                    if (GameObject.Find("Canvas").transform.GetChild(1).GetChild(i).GetComponent<CharacterSelection>().currentSelection == currentSelection)
                    {
                        playable = false;
                    }
                }
            }
        }

       switch (currentSelection)
        {
            case 0:
                transform.localPosition = new Vector3(-556f, 0, 0);
                break;

            case 1:
                transform.localPosition = new Vector3(-185f, 0, 0);
                break;

            case 2:
                transform.localPosition = new Vector3(185f, 0, 0);
                break;

            case 3:
                transform.localPosition = new Vector3(556f, 0, 0);
                break;
        }

        //currentSelection = (int)playerType - 1;
    }

    void Update()
    {
        GameObject.Find("Canvas").transform.GetChild(1).GetChild((int)playerType - 1).gameObject.GetComponent<Image>().fillAmount = holdTimer / holdlengh;


        if ((Input.GetAxisRaw("P" + (int)playerType + "SHOOT") != 0 || Input.GetButton("P" + (int)playerType + "SHOOTALT")))
        {
            holdTimer += Time.deltaTime;
            if (holdTimer >= holdlengh)
            {
                holdTimer = holdlengh;
            }
        }

        if (Input.GetAxisRaw("P" + (int)playerType + "SHOOT") == 0)
        {
            if (!lockedIn)
            {
            holdTimer -= Time.deltaTime;
            }

            if (holdTimer <= 0.0f)
            {
                holdTimer = 0.0f;
            }
        }

        if (holdTimer >= holdlengh)
        {
            lockedIn = true;
        }

        if (lockedIn)
        {
            GetComponent<Image>().color = new Color(0.67f, 1.0f, 0.8f, 0.2f);

            if (!lockSoundPlayed)
            {
                GetComponent<AudioSource>().clip = lockSound;
                GetComponent<AudioSource>().Play();

                lockSoundPlayed = true;
            }
        }
    }
}
