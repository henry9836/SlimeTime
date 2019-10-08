using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SlimeCounter : MonoBehaviour
{

    public GameObject mainPanel;
    public Text countText;
    public GameObject gameManagerObject;
    private GameManager gameManager;

    private bool isNull = true;

    void Start()
    {
        if (gameManagerObject != null)
        {
            isNull = false;

            gameManager = gameManagerObject.GetComponent<GameManager>();
        }
    }

    void Update()
    {
        if (!isNull)
        {
            countText.text = gameManager.remainingSpawn.ToString();
        }
        else
        {
            Debug.Log("Game Manager not found. Cannot display slime count.");
            countText.text = "00";
        }
    }
}
