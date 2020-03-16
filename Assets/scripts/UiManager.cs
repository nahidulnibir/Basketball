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
    [SerializeField]
    Image animationStar;
    Vector2 scoreStarPosition;

    [Header("GameOverPanel")]
    RectTransform gameOverPanel;
    Text gameoverScoreText;
    Text highScoreText;

    [Header("startPanel")]
    RectTransform startPanel;


    bool startTimer = false;


    Vector2 screenDim;
    public static event Action resetBall;
    public static event Action timerTimeUp;
    public static event Action restart;




    void Start()
    {
        currentTime = timeToBurst;
        screenDim = new Vector2(Gamecanvas.rect.width, Gamecanvas.rect.height);
        scoreStarPosition = scoreStar.rectTransform.anchoredPosition;
        Debug.Log(scoreStarPosition);

    }

    private void OnEnable()
    {
        TimeBall.timeBallReset += Timer;
        ScoreManager.scoreAction += score;
        ScoreManager.gameOverAction += GameOver;
    }

    // Update is called once per frame
    void Update()
    {
        if (startTimer)
        {
            if (currentTime > 0)
            {
                currentTime -= Time.deltaTime;
                timerContainer.fillAmount = currentTime / timeToBurst;
                timeText.text = currentTime.ToString("F1");
            }
            else
            {
                startTimer = false;
                timerContainer.gameObject.SetActive(false);
                timerTimeUp();
            }

        }
    }



    void score(int score)
    {
        ScoreText.text = score.ToString();
        StartCoroutine(scoreAnim());
    }

    void GameOver(int score, int highScore)
    {
        gameoverScoreText.text = "SCORE: " + score.ToString();
        highScoreText.text = "HIGHSCORE: " + highScore.ToString();
        gameOverPanel.DOAnchorPos(new Vector2(0, 0), 1f);
    }

    public void Restart()
    {
        gameOverPanel.DOAnchorPos(new Vector2(1000, 0), 1f);
        restart();
    }

    IEnumerator scoreAnim()
    {
        animationStar.gameObject.SetActive(true);
        animationStar.rectTransform.position = Vector2.zero;
        Tween scoreTween = animationStar.rectTransform.DOAnchorPos(scoreStarPosition, 1);
        yield return scoreTween.WaitForCompletion();
        animationStar.gameObject.SetActive(false);
    }


    //revise later
    public void ResetBall()
    {
        resetBall();
        restart();

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
        startTimer = true;
        timerContainer.gameObject.SetActive(true);
        timerContainer.rectTransform.position = new Vector2(screenDim.x/2,screenDim.y/2);
        moveTimer();
        Debug.Log("timeballreset");

    }
}
