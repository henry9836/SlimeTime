using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class WaveComplete : MonoBehaviour
{

    private GameObject gameManagerObject;
    private GameManager gameManager;
    private bool isNull = true;

    private CanvasGroup canvas;
    private Text waveCompleteText;
    private GameObject backPanel;

    // Start is called before the first frame update
    void Awake()
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

        canvas = GetComponent<CanvasGroup>();
        waveCompleteText = transform.Find("endOfWave").GetComponent<Text>();
        backPanel = transform.Find("backPanel").gameObject;

        backPanel.transform.DOScaleX(0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isNull)
        {
            waveCompleteText.text = "wave " + gameManager.currentWave.ToString() + " complete";
        }
        else
        {
            Debug.Log("Game Manager not found. Cannot display slime count.");
        }
    }

    public void Begin()
    {
        canvas.DOFade(1f, 0.25f);
        backPanel.transform.DOScaleX(1.0f, 1.0f).SetEase(Ease.OutQuint);
    }

    public void End()
    {
        canvas.DOFade(0f, 0.25f);
        backPanel.transform.DOScaleX(0, 1.0f).SetEase(Ease.OutQuint);
    }
}
