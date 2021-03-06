﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : Ball
{

    ParticleSystem ps;

    private void Awake()
    {
        ps = GetComponent<ParticleSystem>();
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }


    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (shot && ps.isStopped)
        {
            ps.Play();
        }

    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
    }

    protected override void ResetBall()
    {
        base.ResetBall();
        if (ps.isPlaying)
        {
            ps.Stop();
        }
    }


}
