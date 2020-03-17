using System;
using System.Collections;
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
    float timeToBurst = 10;
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


    [Header("life")]
    [SerializeField]
    Text lifeText;
    [SerializeField]
    Image lifeImage;
    [SerializeField]
    Image animationBall;
    Vector2 lifeBallPosition;
    int life = 3;



    [Header("GameOverPanel")]
    [SerializeField]
    RectTransform gameOverPanel;
    [SerializeField]
    Text gameoverScoreText;
    [SerializeField]
    Text highScoreText;

    [Header("startPanel")]
    RectTransform startPanel;


    bool startTimer = false;


    Vector2 screenDim;


    public static event Action resetBall;
    public static event Action timerTimeUp;
    public static event Action restart;


    SoundManager sm;

    void Start()
    {
        currentTime = timeToBurst;
        screenDim = new Vector2(Gamecanvas.rect.width, Gamecanvas.rect.height);
        scoreStarPosition = scoreStar.rectTransform.anchoredPosition;
        lifeBallPosition = lifeImage.rectTransform.anchoredPosition;
        sm = SoundManager.Instance;
    }

    private void OnEnable()
    {
        TimeBall.timeBallReset += Timer;
        TimeBall.turnOffTimeBall += turnOffTimeBall;
        Ball.turnOffTimer += turnOffTimeBall;
        ScoreManager.scoreAction += score;
        ScoreManager.resetScore += resetScore;
        ScoreManager.damageAction += miss;
        ScoreManager.gameOverAction += GameOver;
        PlatfromAction.ResetBall += ResetBall;


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


        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (Application.platform == RuntimePlatform.Android)
            {
                AndroidJavaObject activity = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");
                activity.Call<bool>("moveTaskToBack", true);
            }
            else
            {
                Application.Quit();
            }
        }
    }



    void score(int score)
    {
        ScoreText.text = score.ToString();
        sm.PlaySound(SoundManager.sounds.score);
        StartCoroutine(scoreAnim());
    }
    void miss()
    {
        life -= 1;
        lifeText.text = life.ToString();
        sm.PlaySound(SoundManager.sounds.miss);
        StartCoroutine(MissAnim());
    }
    void resetScore()
    {
        life = 3;
    }

    void GameOver(int score, int highScore)
    {
        gameoverScoreText.text = "SCORE: " + score.ToString();
        highScoreText.text = "HIGHSCORE: " + highScore.ToString();
        gameOverPanel.DOAnchorPos(new Vector2(0, 0), 1f);
        sm.PlaySound(SoundManager.sounds.gameover);
        sm.PlaySound(SoundManager.sounds.swish);
    }

    public void Restart()
    {
        gameOverPanel.DOAnchorPos(new Vector2(1000, 0), 1f);
        //currentTime = timeToBurst;
        turnOffTimeBall();
        sm.PlaySound(SoundManager.sounds.restart);
        sm.PlaySound(SoundManager.sounds.swish);
        restart();
    }

    IEnumerator scoreAnim()
    {
        animationStar.gameObject.SetActive(true);
        animationStar.rectTransform.position = new Vector2(0, -2000);
        Tween scoreTween = animationStar.rectTransform.DOAnchorPos(scoreStarPosition, 1);
        yield return scoreTween.WaitForCompletion();
        animationStar.gameObject.SetActive(false);
    }
    IEnumerator MissAnim()
    {
        Debug.Log("miss anim");
        animationBall.gameObject.SetActive(true);
        animationBall.rectTransform.anchoredPosition = lifeBallPosition;
        Tween scoreTween = animationBall.rectTransform.DOAnchorPos(new Vector2(0,-2000), 1);
        yield return scoreTween.WaitForCompletion();
        animationBall.gameObject.SetActive(false);
    }




    //revise later
    public void ResetBall()
    {
        resetBall();
        //restart();

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
        timerContainer.rectTransform.position = new Vector2(screenDim.x / 2, screenDim.y / 2);
        moveTimer();
        Debug.Log("timeballreset");
    }

    void turnOffTimeBall()
    {
        timerContainer.gameObject.SetActive(false);
    }
}
