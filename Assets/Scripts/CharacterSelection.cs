using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CharacterSelection : MonoBehaviour
{

    public bool amvalid = true;

    public int currentSelection = 0;
    public bool isselected = true;
    public bool moveable = true;
    public bool selectable = true;

    public enum PLAYERin
    {
        UNASSIGNED,
        PLAYER1,
        PLAYER2,
        PLAYER3,
        PLAYER4
    }

    public PLAYERin playerType = PLAYERin.UNASSIGNED;


    void Update()
    {
        if (amvalid == true)
        {
            this.gameObject.SetActive(true);

            if (isselected == true)
            {
                if (Input.GetAxisRaw("P" + (int)playerType + "HOZ") > 0 && moveable == true)
                {
                    moveable = false;
                    currentSelection++;
                    gameObject.transform.localPosition += new Vector3(466.0f, 0.0f, 0.0f);

                    if (currentSelection == 4)
                    {
                        currentSelection = 0;
                        gameObject.transform.localPosition = new Vector3(-736.0f, gameObject.transform.localPosition.y, gameObject.transform.localPosition.z); 
                    }
                }
                if (Input.GetAxisRaw("P" + (int)playerType + "HOZ") < 0 && moveable == true)
                {
                    moveable = false;
                    currentSelection--;
                    gameObject.transform.localPosition -= new Vector3(466.0f, 0.0f, 0.0f);

                    if (currentSelection == -1)
                    {
                        currentSelection = 3;
                        gameObject.transform.localPosition = new Vector3(736.0f, gameObject.transform.localPosition.y, gameObject.transform.localPosition.z);

                    }
                }
                if (Input.GetAxisRaw("P" + (int)playerType + "HOZ") == 0)
                {
                    moveable = true;
                }
            }

            if (Input.GetAxisRaw("P" + (int)playerType + "SHOOT") != 0 && selectable == true)
            {
                selectable = false;
                isselected = !isselected;
            }

            if (Input.GetAxisRaw("P" + (int)playerType + "SHOOT") == 0)
            {
                selectable = true;
            }
        }
        else
        {
            this.gameObject.SetActive(false);
        }

    }
}
