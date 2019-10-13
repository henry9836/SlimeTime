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
    public int currentWave = 1;
    public bool CanRespawn = false;
    public Vector3 respawnpos; 


    private List<GameObject> players = new List<GameObject>();
    private GameObject[] playerList;
    private bool gameoverLock;

    // Start is called before the first frame update
    void Start()
    {

        gameoverLock = false;

        playerList = GameObject.FindGameObjectsWithTag("Player");

        if (playerList.Length <= 0)
        {
            Debug.LogWarning("GameManager Cannot Find Any Players!");
        }

        //Filter which player is which

        for(int i = 0; i < playerList.Length; i++)
        {
			if (playerList[i].GetComponent<PlayerController>().playerType == PlayerController.PLAYER.PLAYER1)
			{
				players.Add(playerList[i]);
				i = 0;
			}
			if (playerList[i].GetComponent<PlayerController>().playerType == PlayerController.PLAYER.PLAYER2 && players.Count == 1){
				players.Add(playerList[i]);
				i = 0;
			}
			if (playerList[i].GetComponent<PlayerController>().playerType == PlayerController.PLAYER.PLAYER3 && players.Count == 2){
				players.Add(playerList[i]);
				i = 0;
			}
			if (playerList[i].GetComponent<PlayerController>().playerType == PlayerController.PLAYER.PLAYER4 && players.Count == 3){
				players.Add(playerList[i]);
				i = 99999;
				break;
			}
        }

        //Kill any players that have no controller bound

        DyanmicControllers.FindControllers();

        Debug.Log("There are " + DyanmicControllers.controlNum + " Controllers");

        for (int i = 1; i < players.Count + 1; i++)
        {
            if (i > DyanmicControllers.controlNum)
            {
                players[i - 1].GetComponent<PlayerController>().controllerNotBound = true;
                Debug.Log("Player: " + i + " which is " + players[i - 1].name + " is not allowed since our controller num is: " + DyanmicControllers.controlNum);
            }
        }

        StartCoroutine(checkPlayers());

    }

    public void SlimeKilled()
    {
        if (remainingSpawn > 0)
        {
            remainingSpawn--;
        }
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
            CanRespawn = true;
            gracetimer -= Time.deltaTime;
            if (gracetimer <= 0.0f)
            {
                CanRespawn = false;
                gracetimer = grace;
                currentWave++;
                GetComponent<slimeSpawner>().NextWave(currentWave);
            }
        }
    }

    //Check players for death
    IEnumerator checkPlayers()
    {
        while (true)
        {
            bool maybeGameover = true;
            for (int i = 0; i < playerList.Length; i++)
            {
                if (playerList[i].GetComponent<PlayerController>().health > 0)
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


