using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    private void FixedUpdate()
    {
        if (Input.anyKeyDown)
        {
            GetComponent<SceneSwitcher>().SceneSwitch("Game");
        }
    }
}
