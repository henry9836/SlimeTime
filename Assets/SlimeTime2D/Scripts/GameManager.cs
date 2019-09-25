using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public bool gameTesting = false;

    public float gameTime = 0.0f;
    public float toggleRoundTime = 60.0f;
    public float maxGameTime = 120.0f;

    public bool killPhaseActive = false;
    public bool gameOver = false;

    public AudioClip bossDing;
    public AudioClip winMusic;

    public GameObject winnerParticles;

    public GameObject firstPlace;
    public GameObject secondPlace;
    public GameObject thirdPlace;
    public GameObject fourthPlace;

    public GameObject playerOne;
    public GameObject playerTwo;
    public GameObject playerThree;
    public GameObject playerFour;
    public List<GameObject> playerScores = new List<GameObject>();

    private bool sortLock = false;
    private bool fadeInLock = false;
    private GameObject lastFirst;
    private void Start()
    {
        gameTime = 0.0f;
        killPhaseActive = false;
        gameOver = false;
        sortLock = false;
        fadeInLock = false;
        StartCoroutine(GetFarming());
    }

    void AssignPlayers()
    {
        playerOne = (GameObject.FindGameObjectWithTag("P1"));
        playerTwo = (GameObject.FindGameObjectWithTag("P2"));
        playerThree = (GameObject.FindGameObjectWithTag("P3"));
        playerFour = (GameObject.FindGameObjectWithTag("P4"));
    }

    private void FixedUpdate()
    {
        AssignPlayers();
        if (SceneManager.GetActiveScene().name == "Game" || gameTesting)
        {
            gameTime += Time.deltaTime;
            if (gameTime > toggleRoundTime)
            {
                if (!gameOver)
                {
                    killPhaseActive = true;
                    if (!sortLock)
                    {
                        if (!fadeInLock)
                        {
                            GameObject.Find("Canvas").GetComponent<redfade>().Fadein();
                            fadeInLock = true;
                            StartCoroutine(GetKilling());
                        }
                        StartCoroutine(SortPlayers());
                    }
                }
            }
            if (gameTime > maxGameTime)
            {
                if (!gameOver)
                {
                    StartCoroutine(GameOver());
                }

            }
        }
    }

    IEnumerator SortPlayers()
    {
        sortLock = true;

        //Add all players to list
        playerScores.Clear();
        playerScores.Add(playerOne);
        playerScores.Add(playerTwo);
        playerScores.Add(playerThree);
        playerScores.Add(playerFour);

        bool sorted = false;

        while (!sorted)
        {
            yield return new WaitForSeconds(0.01f);
            GameObject tmp;
            if ((playerScores[0].GetComponent<PlayerManager>().currentscore > playerScores[1].GetComponent<PlayerManager>().currentscore) || (playerScores[0].GetComponent<PlayerManager>().currentscore == playerScores[1].GetComponent<PlayerManager>().currentscore))
            {
                if ((playerScores[1].GetComponent<PlayerManager>().currentscore > playerScores[2].GetComponent<PlayerManager>().currentscore) || (playerScores[1].GetComponent<PlayerManager>().currentscore == playerScores[2].GetComponent<PlayerManager>().currentscore))
                {
                    if ((playerScores[2].GetComponent<PlayerManager>().currentscore > playerScores[3].GetComponent<PlayerManager>().currentscore) || (playerScores[2].GetComponent<PlayerManager>().currentscore == playerScores[3].GetComponent<PlayerManager>().currentscore))
                    {
                        if ((playerScores[3].GetComponent<PlayerManager>().currentscore < playerScores[2].GetComponent<PlayerManager>().currentscore) || (playerScores[3].GetComponent<PlayerManager>().currentscore == playerScores[2].GetComponent<PlayerManager>().currentscore))
                        {
                            if ((playerScores[2].GetComponent<PlayerManager>().currentscore < playerScores[1].GetComponent<PlayerManager>().currentscore) || (playerScores[2].GetComponent<PlayerManager>().currentscore == playerScores[1].GetComponent<PlayerManager>().currentscore))
                            {
                                if ((playerScores[1].GetComponent<PlayerManager>().currentscore < playerScores[0].GetComponent<PlayerManager>().currentscore) || (playerScores[0].GetComponent<PlayerManager>().currentscore == playerScores[1].GetComponent<PlayerManager>().currentscore))
                                {
                                    sorted = true;
                                }
                                else
                                {
                                    tmp = playerScores[0];
                                    playerScores[1] = playerScores[0];
                                    playerScores[0] = tmp;
                                    sorted = false;
                                }
                            }
                            else
                            {
                                tmp = playerScores[1];
                                playerScores[2] = playerScores[1];
                                playerScores[1] = tmp;
                                sorted = false;
                            }
                        }
                        else
                        {
                            tmp = playerScores[2];
                            playerScores[3] = playerScores[2];
                            playerScores[2] = tmp;
                            sorted = false;
                        }
                    }
                    else
                    {
                        tmp = playerScores[2];
                        playerScores[2] = playerScores[3];
                        playerScores[3] = tmp;
                        sorted = false;
                    }
                }
                else
                {
                    tmp = playerScores[1];
                    playerScores[1] = playerScores[2];
                    playerScores[2] = tmp;
                    sorted = false;
                }
            }
            else
            {
                tmp = playerScores[0];
                playerScores[0] = playerScores[1];
                playerScores[1] = tmp;
                sorted = false;
            }
        }


        firstPlace = playerScores[0];
        secondPlace = playerScores[1];
        thirdPlace = playerScores[2];
        fourthPlace = playerScores[3];

        if (lastFirst != firstPlace)
        {
            GetComponent<AudioSource>().clip = bossDing;
            GetComponent<AudioSource>().Play();
            lastFirst = firstPlace;
        }

        firstPlace.transform.GetChild(2).gameObject.SetActive(true);
        secondPlace.transform.GetChild(2).gameObject.SetActive(false);
        thirdPlace.transform.GetChild(2).gameObject.SetActive(false);
        fourthPlace.transform.GetChild(2).gameObject.SetActive(false);

        firstPlace.GetComponent<PlayerManager>().damageable = true;
        secondPlace.GetComponent<PlayerManager>().damageable = false;
        thirdPlace.GetComponent<PlayerManager>().damageable = false;
        fourthPlace.GetComponent<PlayerManager>().damageable = false;

        sortLock = false;
    }

    IEnumerator GameOver()
    {
        GameObject.Find("Canvas").transform.GetChild(2).gameObject.SetActive(false);
        GameObject.Find("Canvas").transform.GetChild(3).gameObject.SetActive(false);

        for (int i = 0; i < 4; i++)
        {
            SelectManager.selection = i;
            SelectManager.playerchoice = 420;
        }



        gameOver = true;
        Destroy(secondPlace);
        Destroy(thirdPlace);
        Destroy(fourthPlace);
        GetComponent<AudioSource>().clip = winMusic;
        GetComponent<AudioSource>().Play();

        firstPlace.transform.GetChild(0).transform.GetChild(2).GetComponent<Image>().enabled = true;

        Instantiate(winnerParticles, new Vector3(0, 7.01f, 0), Quaternion.identity);
        GameObject.Find("Canvas").GetComponent<redfade>().Fadeout();
        yield return new WaitForSeconds(10.0f);
        SceneManager.LoadScene(0);
    }

    IEnumerator GetFarming()
    {
        GameObject.Find("Canvas").transform.GetChild(4).GetComponent<fadeoutUI>().Fadein();
        yield return null;
    }

    IEnumerator GetKilling()
    {
        GameObject.Find("Canvas").transform.GetChild(5).GetComponent<fadeoutUI>().Fadein();
        yield return null;
    }

}
