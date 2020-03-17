using System;
using UnityEngine;

public class PlatfromAction : MonoBehaviour
{
    EdgeCollider2D ec;
    bool scored = false;

    public static event Action miss;
    public static event Action ResetBall;


    private void OnEnable()
    {
        Ball.onUpperCol += PlatformOff;
        Ball.resetHoop += PlatformOn;
        hoop.score += FailDetect;
    }

    private void Awake()
    {
        ec = GetComponent<EdgeCollider2D>();
        ec.enabled = true;
    }

    private void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void PlatformOff()
    {
        ec.enabled = false;
    }
    void PlatformOn()
    {
        ec.enabled = true;
        scored = false ;
    }

    void FailDetect() {
        scored = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "ball" & !scored)
        {
            miss();
            ResetBall();
        }
        else
        {
            ResetBall();
        }

        //if (collision.tag == "ball")
        //{
        //    ResetBall();
        //}
    }

}
