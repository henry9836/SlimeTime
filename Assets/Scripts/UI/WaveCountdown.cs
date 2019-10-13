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
    private GameObject countdownTimer;
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

        countdownTimer = transform.Find("graceTime").gameObject;
        countdownFill = transform.Find("timerImage").GetComponent<Image>();

        transform.DOScale(new Vector3(0, 0, 0), 0f);
    }

    void Update()
    {
        countdownTimer.GetComponent<Text>().text = Mathf.Ceil(gameManager.gracetimer).ToString();
        countdownFill.fillAmount = gameManager.gracetimer / startTimerValue;
    }

    private void OnEnable()
    {
        transform.DOScale(new Vector3(1f, 1f, 1f), 1f).SetEase(Ease.OutQuint);
    }

    private void OnDisable()
    {
        transform.DOScale(new Vector3(0, 0, 0), 0f);
    }
}
