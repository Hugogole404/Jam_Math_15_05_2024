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
        gameObject.transform.DOPunchScale(Vector3.one * .15f, .5f);
    }

    public void DeathAnim()
    {
        Destroy(gameObject);

        // gameObject.transform.DOScale(0, .5f).SetEase(Ease.InBounce).OnComplete(() =>
        // {
        //     Destroy(gameObject);
        // });;
    }
}