using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CharacterSelection : MonoBehaviour
{

    public bool amvalid;

    public int currentSelection = 0;
    public bool isselected = false;
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
            if (isselected == true)
            {
                if (Input.GetAxisRaw("P" + (int)playerType + "HOZ") > 0 && moveable == true)
                {
                    moveable = false;
                    currentSelection++;
                    if (currentSelection == 4)
                    {
                        currentSelection = 0;
                    }
                }
                if (Input.GetAxisRaw("P" + (int)playerType + "HOZ") < 0 && moveable == true)
                {
                    moveable = false;
                    currentSelection--;
                    if (currentSelection == 0)
                    {
                        currentSelection = 4;
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
            this.gameObject.GetComponent<MeshRenderer>().
        }

    }
}
