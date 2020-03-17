using System;
using UnityEngine;

public class hoop : MonoBehaviour
{

    SpriteRenderer sr;
    CircleCollider2D scoreCol;


    public static event Action score;


    private void OnEnable()
    {
        Ball.onUpperCol += ChangeSortingLayerToFg;
        Ball.resetHoop += ResetSortingLayer;
    }

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        scoreCol = GetComponent<CircleCollider2D>();
    }

    private void Start()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "ball")
        {
            //score
            score();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ChangeSortingLayerToFg() {
        sr.sortingLayerID = SortingLayer.NameToID("fg");
        scoreCol.enabled = true;
    }

    void ResetSortingLayer()
    {
        sr.sortingLayerID = SortingLayer.NameToID("bg");
        scoreCol.enabled = false;
    }

}
