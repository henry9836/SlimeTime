using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    void Update()
    {
        if (Input.anyKey == true)
        {
            SceneManager.LoadScene(1);
        }
    }
}
