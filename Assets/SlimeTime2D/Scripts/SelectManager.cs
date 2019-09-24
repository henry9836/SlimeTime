using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectManager : MonoBehaviour
{
    public static int p1Choice = 420, p2Choice = 420, p3Choice = 420, p4Choice = 420;
    public static int iselection = 0;
    public static int playerchoice
    {
        get
        {
            if (iselection == 0)
            {
                return p1Choice;
            }
            if (iselection == 1)
            {
                return p2Choice;
            }
            if (iselection == 2)
            {
                return p3Choice;
            }
            if (iselection == 3)
            {
                return p4Choice;
            }
            else
            {
                return (42069); // not valid
            }
        }
        set
        {
            if (iselection == 0)
            {
                p1Choice = value;
            }
            if (iselection == 1)
            {
                p2Choice = value;
            }
            if (iselection == 2)
            {
                p3Choice = value;
            }
            if (iselection == 3)
            {
                p4Choice = value;
            }
        }
    }
    public static int selection
    {
        set
        {
            iselection = value;
        }
    }

}