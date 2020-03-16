using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    Ball[] balls;
    Ball activeBall;
    Vector2 restingPos = new Vector2(500, 500);
    Vector2 originPos = new Vector2(0, -4);


    void Start()
    {
        foreach (Ball ball in balls)
        {
            ball.GetComponent<Transform>().localPosition = restingPos;
            ball.gameObject.SetActive(false);
        }

        activeBall = balls[0];
        activeBall.gameObject.SetActive(true);
        activeBall.GetComponent<Transform>().localPosition = originPos;
        
    }

    // Update is called once per frame

    public static event Action resetBall;

    public void SwitchBall(int ballsIndex)
    {
        activeBall.gameObject.SetActive(false);
        activeBall = balls[ballsIndex];
        activeBall.gameObject.SetActive(true);
        resetBall();
    }


}
