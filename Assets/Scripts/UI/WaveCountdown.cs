using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class WaveCountdown : MonoBehaviour
{
    private GameObject gameManagerObject;
    private GameManager gameManager;
    private bool isNull = true;
    private float startTimerValue = 7.0f;
    private bool isActive;

    private CanvasGroup canvas;
    private Text countdownTimer;
    private Image countdownFill;

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
        countdownTimer = transform.Find("graceTime").GetComponent<Text>();
        countdownFill = transform.Find("timerImage").GetComponent<Image>();

        transform.DOScale(new Vector3(0, 0, 0), 0f);
    }

    void Update()
    {
        if (isActive)
        {
            countdownTimer.text = Mathf.Ceil(gameManager.gracetimer).ToString();
            countdownFill.fillAmount = gameManager.gracetimer / startTimerValue;
        }
        else
        {
            countdownTimer.text = "0";
            countdownFill.fillAmount = 0;
        }
    }

    //private void OnEnable()
    //{
    //    transform.DOScale(new Vector3(1f, 1f, 1f), 1f).SetEase(Ease.OutQuint);
    //}

    //private void OnDisable()
    //{
    //    transform.DOScale(new Vector3(0, 0, 0), 0f);
    //}

    public void Begin()
    {
        isActive = true;
        canvas.DOFade(1f, 0.25f);
        transform.DOScale(new Vector3(1f, 1f, 1f), 0.5f).SetEase(Ease.OutQuint);
    }

    public void End()
    {
        isActive = false;
        canvas.DOFade(0f, 0.25f);
        transform.DOScale(new Vector3(0, 0, 0), 0.4f);
    }
}
