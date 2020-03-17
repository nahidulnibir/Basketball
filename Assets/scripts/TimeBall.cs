using System;
using UnityEngine;

public class TimeBall : Ball
{
    public static event Action timeBallReset;
    public static event Action gameOver;
    public static event Action turnOffTimeBall;
    [SerializeField]
    ParticleSystem ps;
    SpriteRenderer sr;

    protected override void OnEnable()
    {
        base.OnEnable();
        UiManager.timerTimeUp += PlayBurst;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        turnOffTimeBall();
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
        if (base.timeUp)
        {
            base.sm.PlaySound(SoundManager.sounds.burst);
            gameOver();
            base.timeUp = false;
        }
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
        sr.enabled = false;
    }

}
