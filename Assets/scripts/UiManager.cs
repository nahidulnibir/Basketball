using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{    // Start is called before the first frame update
    [SerializeField]
    RectTransform ballOptionRect;
    bool ballOPtion = false;
    [SerializeField]
    RectTransform Gamecanvas;

    [Header("timer")]
    [SerializeField]
    float timeToBurst;
    float currentTime;
    [SerializeField]
    Image timerContainer;
    [SerializeField]
    Text timeText;

    [Header("score")]
    [SerializeField]
    Text ScoreText;
    [SerializeField]
    Image scoreStar;

    bool starTimer = false;


    Vector2 screenDim;
    public static event Action resetBall;
    public static event Action timerTimeUp;


    void Start()
    {
        currentTime = timeToBurst;
        screenDim = new Vector2(Gamecanvas.rect.width, Gamecanvas.rect.height);
        Debug.Log(screenDim);
    }

    private void OnEnable()
    {
        TimeBall.timeBallReset += Timer;
    }

    // Update is called once per frame
    void Update()
    {
        if (starTimer)
        {
            if (currentTime > 0)
            {
                currentTime -= Time.deltaTime;
                timerContainer.fillAmount = currentTime / timeToBurst;
                timeText.text = currentTime.ToString("F1");
            }
            else
            {
                starTimer = false;
                timerContainer.gameObject.SetActive(false);
                timerTimeUp();
            }

        }
    }


    //revise later
    public void ResetBall()
    {
        resetBall();

    }

    void moveTimer()
    {
        timerContainer.rectTransform.DOAnchorPos(new Vector2(0, screenDim.y/2-100),1f);
    }

    public void RevealBallOption()
    {
        if (!ballOPtion)
        {
            ballOptionRect.DOAnchorPos(Vector2.zero, 0.25f);
            ballOPtion = true;
        }
        else
        {
            ballOptionRect.DOAnchorPos(new Vector2(0,-200), 0.25f);
            ballOPtion = false;

        }
    }

    void Timer() {
        currentTime = timeToBurst;
        starTimer = true;
        timerContainer.gameObject.SetActive(true);
        timerContainer.rectTransform.position = new Vector2(screenDim.x/2,screenDim.y/2);
        moveTimer();
        Debug.Log("timeballreset");

    }
}
