using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class characterSeclectManager : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        SelectManager.selection = 0;
        if (SelectManager.playerchoice != 420)
        {
            SelectManager.selection = 1;
            if (SelectManager.playerchoice != 420)
            {
                SelectManager.selection = 2;
                if (SelectManager.playerchoice != 420)
                {
                    SelectManager.selection = 3;
                    if (SelectManager.playerchoice != 420)
                    {
                        SceneManager.LoadScene(2);
                    }
                }
            }
        }
    }
}
