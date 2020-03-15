using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeBall : Ball
{
    public static event Action timeBallReset;
    [SerializeField]
    ParticleSystem ps;
    SpriteRenderer sr;

    protected override void OnEnable()
    {
        base.OnEnable();
        UiManager.timerTimeUp += PlayBurst;
    }

    protected override void Start()
    {
        base.Start();
        sr = GetComponent<SpriteRenderer>();


    }


    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
    }

    protected override void ResetBall()
    {
        base.ResetBall();
        timeBallReset();
        sr.enabled = true;
    }

    void PlayBurst()
    {
        ps.Play();
        //this.gameObject.SetActive(false);
        sr.enabled = false;
    }

}
