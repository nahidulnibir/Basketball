using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UiManager : MonoBehaviour
{    // Start is called before the first frame update

    public RectTransform ballOptionRect;
    bool ballOPtion = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    public static event Action resetBall;

    //revise later
    public void ResetBall()
    {
        resetBall();
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
}
