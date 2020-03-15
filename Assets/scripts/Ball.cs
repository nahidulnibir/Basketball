using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(CircleCollider2D))]
public class Ball : MonoBehaviour
{
    Vector2 startPos, endPos, dir, normDir;
    [SerializeField]
    Vector2 origin = new Vector2(0,-4);

    float tStart, tEnd, tInterval;

    public float tForce = 100;

    Rigidbody2D rb2d;

    public bool shot=false;


    float resetTime = 10;
    float currentTime = 10;


    public static event Action onUpperCol;
    public static event Action resetHoop;
    public static event Action resetBall;


    protected virtual void OnEnable()
    {
        UiManager.resetBall += ResetBall;
        GameManager.resetBall += ResetBall;
    }

    protected virtual void OnDisable()
    {
        UiManager.resetBall -= ResetBall;
        GameManager.resetBall -= ResetBall;

    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.isKinematic = true;
        rb2d.velocity = Vector2.zero;
        rb2d.gravityScale = 4;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if(Input.touchCount>0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            tStart = Time.time;
            startPos = Input.GetTouch(0).position;
        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            tEnd = Time.time;
            tInterval = tEnd - tStart;
            endPos = Input.GetTouch(0).position;
            dir = (endPos - startPos);
            normDir = (endPos - startPos).normalized;
            Debug.Log(dir);
            if (dir.y > 20)
            {
                rb2d.isKinematic = false;
                rb2d.AddForce(normDir * tForce, ForceMode2D.Impulse);
                shot = true;
            }
        }

        if (shot)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, transform.localScale * .4f, Time.deltaTime*2);
        }

        if( shot && currentTime > 0)
        {
            currentTime -= Time.deltaTime;
        }
        else
        {
            ResetBall();
        }



    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "uppercol")
        {
            onUpperCol();
        }
    }

    protected virtual void ResetBall()
    {
        transform.localPosition = origin;
        shot = false;
        transform.localScale = new Vector2(1, 1);
        resetHoop();
        rb2d.isKinematic = true;
        rb2d.velocity = Vector2.zero;
        rb2d.angularVelocity = 0;
        currentTime = 10;
    }

}
