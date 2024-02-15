/*************************************************************
 *
 * Author: Natalie Soltis
 * Date: #DATE#
 * 
 * What: CardScale.cs
 * 
 * Function: ...
 *
 * TODO: ...
 *
 * Say thank you on the way out!
 * 
 ***********************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardScale : MonoBehaviour
{
    [SerializeField]
    private float cardRatio = 0.714f;

    
    private RectTransform rectTransform;
    
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        var sizeDelta = rectTransform.sizeDelta;
        sizeDelta = new Vector2(sizeDelta.y * cardRatio, sizeDelta.x * cardRatio);
        rectTransform.sizeDelta = sizeDelta;
    }
}
