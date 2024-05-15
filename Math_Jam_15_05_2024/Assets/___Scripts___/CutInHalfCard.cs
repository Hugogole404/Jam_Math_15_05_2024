using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutInHalfCard : MonoBehaviour
{
    [SerializeField] private GameObject _cardNumbrePrefab;
    [SerializeField] private float _timeBeforeCanCutAgain = 2;

    private bool _canCut;

    private void Start()
    {
        _canCut = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!_canCut) return;
        
        var card = other.GetComponent<CardNumber>();
        if (card != null && card.CanBeCut && card.Value > 1)
        {
            // print("detect card for cut");
            CutCard(other.GetComponent<CardNumber>());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var card = other.GetComponent<CardNumber>();
        if (card != null)
        {
            // print("ciao for cut");
            card.CanBeCut = true;
        }
    }

    private void CutCard(CardNumber card)
    {
        // print("go cut");
        int firstHalf = 0;
        int secondHalf = 0;

        firstHalf = card.Value / 2;
        secondHalf = firstHalf;

        if (card.Value - firstHalf * 2 != 0)
            secondHalf++;
        
        // print($"first : {firstHalf} -- second {secondHalf}");
        SpawnNewCard(firstHalf, card.gameObject.transform);
        SpawnNewCard(secondHalf, card.gameObject.transform);

        StartCoroutine(TimeCanCanCutAgain());
        
        Destroy(card.gameObject);
    }

    private void SpawnNewCard(int value, Transform transform)
    {
        GameObject go = Instantiate(_cardNumbrePrefab, transform.position, transform.rotation);
        go.GetComponent<CardNumber>().Init(value, false);
    }

    IEnumerator TimeCanCanCutAgain()
    {
        _canCut = false;
        yield return new WaitForSeconds(_timeBeforeCanCutAgain);
        _canCut = true;
    }
}
