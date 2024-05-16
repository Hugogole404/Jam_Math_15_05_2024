using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class Card : MonoBehaviour
{
    [FormerlySerializedAs("_textDisplay")] [SerializeField]
    private TMP_Text _textDisplayBig;

    [SerializeField] private TMP_Text _textDisplaySmall;
    [SerializeField] private GameObject _fx_DropCardOnGround;

    public string TextValue { get; set; }
    private Vector3 _startSize;

    private void Awake()
    {
        _startSize = gameObject.transform.localScale;

        gameObject.transform.DOScale(0, 0);
    }

    public void SetText(string text)
    {
        TextValue = text;
        _textDisplayBig.text = text;
        _textDisplaySmall.text = text;
    }

    public void InitAnim()
    {
        gameObject.transform.DOScale(_startSize, 1f).SetEase(Ease.OutBounce);
        // gameObject.transform.DOPunchScale(Vector3.one * .15f, .5f);
    }

    public void DeathAnim()
    {
        Destroy(gameObject);

        // gameObject.transform.DOScale(0, .5f).SetEase(Ease.InBounce).OnComplete(() =>
        // {
        //     Destroy(gameObject);
        // });;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.GetComponent<CardTable>())
        {
            GameObject go = Instantiate(_fx_DropCardOnGround, transform.position, transform.rotation);
        }
    }
}