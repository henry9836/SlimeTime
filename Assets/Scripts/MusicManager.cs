using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{

    private SceneSwitcher scene;

    // Start is called before the first frame update
    void Awake()
    {
        scene = FindObjectOfType<SceneSwitcher>();

        if (scene.curScene == "MainMenu" || scene.curScene == "CharacterSelection")
        {
            DontDestroyOnLoad(this.gameObject);
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}
