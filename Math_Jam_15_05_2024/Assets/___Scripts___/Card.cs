using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class Card : MonoBehaviour
{
    [SerializeField] private TMP_Text _textDisplay;

    public string TextValue { get; set; }

  

    public void SetText(string text)
    {
        TextValue = text;
        _textDisplay.text = text;
    }
    
    public void InitAnim()
    {
        //gameObject.transform.DOPunchScale(Vector3.one* .5f, .5f, 8);
    }
}