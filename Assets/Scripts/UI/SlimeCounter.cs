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
    private string textBefore = "00";

    void Start()
    {

        if (gameManagerObject == null)
        {
            gameManagerObject = GameObject.Find("GameManager");
        }

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
            countText.text = gameManager.remainingSpawn.ToString("000");
        }
        else
        {
            Debug.Log("Game Manager not found. Cannot display slime count.");
            countText.text = "000";
        }

        if (Input.GetKeyDown(KeyCode.T) || textBefore != countText.text)
        {
            Shake();
        }


        textBefore = countText.text;
    }

    public void Shake()
    {
        mainPanel.transform.DOKill(true);
        mainPanel.transform.DOPunchPosition(new Vector3(0f, -16f, 0f), 0.25f, 3, 0.5f, false);
    }
}
