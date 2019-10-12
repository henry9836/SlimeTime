using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DyanmicControllers
{
    public static int controlNum = -9999; //-9999 as a default value

    public static int FindControllers()
    {
        controlNum = 0;
        try
        {
            if (Input.GetJoystickNames()[0] != null)
            {
                controlNum++;
            }
            if (Input.GetJoystickNames()[1] != null)
            {
                controlNum++;
            }
            if (Input.GetJoystickNames()[2] != null)
            {
                controlNum++;
            }
            if (Input.GetJoystickNames()[3] != null)
            {
                controlNum++;
            }
        }
        catch (System.IndexOutOfRangeException e)
        {
            Debug.Log("Run out of controllers to find");
        }

        Debug.Log("Controllers Found: " + controlNum);

        return controlNum;
    }
}
