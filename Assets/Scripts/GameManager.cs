using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int remainingSpawn;
    public float grace = 5.0f;
    public float gracetimer;
    public bool gameover;


    GameObject[] players;

    private bool gameoverLock;

    // Start is called before the first frame update
    void Start()
    {

        gameoverLock = false;

        players = GameObject.FindGameObjectsWithTag("Player");

        if (players.Length <= 0)
        {
            Debug.LogWarning("GameManager Cannot Find Any Players!");
        }

        StartCoroutine(checkPlayers());

    }

    public void SlimeKilled()
    {
        remainingSpawn--;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //If all players are dead
        if (gameover)
        {
            Debug.Log("GAMEOVER");
            if (!gameoverLock)
            {
                StartCoroutine(returnToMain());
            }
        }

        if (remainingSpawn == 0 && !gameover) // no slimes left (need to manage that here)
        {
            gracetimer -= Time.deltaTime;
            if (gracetimer <= 0.0f)
            {
                gracetimer = grace;
                GameObject.Find("GameManager").GetComponent<slimeSpawner>().timer = 0.0f;
                GameObject.Find("GameManager").GetComponent<slimeSpawner>().isSpawning = true;
                GameObject.Find("GameManager").GetComponent<slimeSpawner>().onceSpawnng = true;
            }
        }
    }

    //Check players for death
    IEnumerator checkPlayers()
    {
        while (true)
        {
            bool maybeGameover = true;
            for (int i = 0; i < players.Length; i++)
            {
                if (players[i].GetComponent<PlayerController>().health > 0)
                {
                    maybeGameover = false;
                    break;
                }
            }
            gameover = maybeGameover;
            yield return null;
        }
        
    }

    //Returns to main menu
    IEnumerator returnToMain()
    {
        gameoverLock = true;
        GetComponent<SceneSwitcher>().SceneSwitch("MainMenu");
        yield return null;
        //SceneManager.LoadScene("MainMenu");
    }

}
