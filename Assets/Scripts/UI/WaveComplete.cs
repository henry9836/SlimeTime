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

    void Update()
    {
        if (!isNull)
        {
            waveCompleteText.text = "wave " + gameManager.currentWave.ToString() + " complete";
        }
    }

    public void Begin()
    {
        canvas.DOFade(1f, 0.25f);
        waveCompleteText.gameObject.transform.DOPunchScale(new Vector3(0.1f, 0.1f, 0.1f), 0.4f, 3, 0.5f);
        backPanel.transform.DOScaleX(1.0f, 1.0f).SetEase(Ease.InOutQuart);
    }

    public void End()
    {
        canvas.DOFade(0f, 0.25f);
        backPanel.transform.DOScaleX(0, 1.0f).SetEase(Ease.OutQuint);
    }
}
