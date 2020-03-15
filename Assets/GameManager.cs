using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    GameObject[] balls;
    GameObject activeBall;
    Vector2 restingPos = new Vector2(500, 500);
    Vector2 originPos = new Vector2(0, -4);


    void Start()
    {
        foreach (GameObject ball in balls)
        {
            ball.GetComponent<Transform>().localPosition = restingPos;
            ball.SetActive(false);
        }

        activeBall = balls[0];
        activeBall.SetActive(true);
        activeBall.GetComponent<Transform>().localPosition = originPos;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static event Action resetBall;

    public void SwitchBall(int ballsIndex)
    {
        activeBall.SetActive(false);
        activeBall = balls[ballsIndex];
        activeBall.SetActive(true);
        resetBall();
    }


}
