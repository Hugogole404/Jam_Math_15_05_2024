using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class SliderManager : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _srSlider;
    [SerializeField] private float _timeSlider = 4f;
    
    private float _sliderValue = 1;
    private bool _canGoSlider;
    private Vector3 _startSize;
    private List<Card> _saveCards = new List<Card>();

    private void Start()
    {
        _startSize = transform.localScale;
        gameObject.transform.DOScale(0, 0);
        //StartCoroutine(test());
    }

    IEnumerator test()
    {
        yield return new WaitForSeconds(1f);
        GoSlider(null);
    }

    private void Update()
    {
        if(_canGoSlider)
            SliderAnim();
    }

    public void GoSlider(List<Card> cards)
    {
        _sliderValue = 1;
        gameObject.transform.DOScale(_startSize, .5f).SetEase(Ease.OutBounce);
        _canGoSlider = true;
        
        if(cards is { Count: > 1 })
            _saveCards = cards;
    }

    private void SliderAnim()
    {
        if(_sliderValue > 0)
            _sliderValue -= Time.deltaTime * (1/_timeSlider);
        else
        {
            HideStopSlider();

            if (_saveCards is { Count: > 1 })
            {
                OperationsManager.Instance.GoCalcul(_saveCards);
            }
        }
        
        _srSlider.size = new Vector2(_sliderValue, _srSlider.size.y);
    }

    public void HideStopSlider()
    {
        _canGoSlider = false;

        gameObject.transform.DOScale(0, .5f).SetEase(Ease.InBounce);
    }
}
