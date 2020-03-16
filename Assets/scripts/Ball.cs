using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(CircleCollider2D))]


//main swipe mechanism works here
//detects the touch start and end and calculate the normalized direction then add a impulse force in that normalize direction 
//uses a flag to check if already shot if so then wont take any more shot
//there ia a collider above the hoop which triggers the sorting layer of the hoop which in general stays under the balls layer 
// but when triggered goes over the balls layer which gives an impression of ball going through the basket 
//another coolider in the hoop triggers the score 
//and if without triggering the score the ball touches the platform trigger then miss is counted 
//triggering the platform witll reset the ball 
//time ball will blast if not flicked in 10 seconds 
// to communicate with different scripts i mostly used delegates and events but in case of sounds manager i used singletone 




public class Ball : MonoBehaviour
{
    Vector2 startPos, endPos, dir, normDir;
    [SerializeField]
    Vector2 origin = new Vector2(0,-4);

    float tStart, tEnd, tInterval;

    public float tForce = 100;

    Rigidbody2D rb2d;

    protected bool shot=false;


    float resetTime = 10;
    float currentTime = 10;

    protected bool timeUp = false;

    public static event Action onUpperCol;
    public static event Action resetHoop;
    public static event Action turnOffTimer;
    //public static event Action resetBall;

    protected SoundManager sm;


    protected virtual void OnEnable()
    {
        UiManager.resetBall += ResetBall;
        GameManager.resetBall += ResetBall;
        UiManager.restart += ResetBall;

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
        sm = SoundManager.Instance;
    }

    // Update is called once per frame
    protected virtual void Update()
    {

#if UNITY_EDITOR

        if (Input.GetKeyDown("space") & !shot)
        {
            rb2d.isKinematic = false;
            rb2d.AddForce(new Vector2(0.1f,1) * tForce, ForceMode2D.Impulse);
            shot = true;
        }
        else if(Input.GetKeyDown("up")& !shot)
        {
            rb2d.isKinematic = false;
            rb2d.AddForce(Vector2.up * tForce, ForceMode2D.Impulse);
            shot = true;

        }

#endif

        if (Input.touchCount>0 && Input.GetTouch(0).phase == TouchPhase.Began)
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
            if (dir.y > 20&!shot)
            {
                rb2d.isKinematic = false;
                rb2d.AddForce(normDir * tForce, ForceMode2D.Impulse);
                shot = true;
                sm.PlaySound(SoundManager.sounds.ballThrow);

            }
        }

        if (shot)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, transform.localScale * .7f, Time.deltaTime*1.7f);
        }

        if (!shot && currentTime > 0)
        {
            currentTime -= Time.deltaTime;
        }
        else if (shot)
        {
            timeUp = false;
            turnOffTimer();
        }
        else
        {
            timeUp = true;
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
        timeUp = false;
    }

}
