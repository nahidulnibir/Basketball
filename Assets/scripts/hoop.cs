using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hoop : MonoBehaviour
{

    SpriteRenderer sr;

    private void OnEnable()
    {
        Ball.onUpperCol += ChangeSortingLayerToFg;
        Ball.resetHoop += ResetSortingLayer;
    }

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ChangeSortingLayerToFg() {
        sr.sortingLayerID = SortingLayer.NameToID("fg");
    }

    void ResetSortingLayer()
    {
        sr.sortingLayerID = SortingLayer.NameToID("bg");
    }

}
