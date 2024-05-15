using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutInHalfCard : MonoBehaviour
{
    [SerializeField] private GameObject _cardNumbrePrefab;
    private void OnTriggerEnter(Collider other)
    {
        var card = other.GetComponent<CardNumber>();
        if (card != null && card.CanBeCut)
        {
            print("detect card for cut");
            CutCard(other.GetComponent<CardNumber>());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var card = other.GetComponent<CardNumber>();
        if (card != null)
        {
            print("detect card for cut");
            card.CanBeCut = true;
        }
    }

    private void CutCard(CardNumber card)
    {
        int firstHalf = 0;
        int secondHalf = 0;

        firstHalf = card.Value / 2;
        secondHalf = firstHalf;

        if (card.Value - firstHalf * 2 != 0)
            secondHalf++;
        
        // print($"first : {firstHalf} -- second {secondHalf}");
        SpawnNewCard(firstHalf, card.gameObject.transform);
        SpawnNewCard(secondHalf, card.gameObject.transform);
        
        Destroy(card.gameObject);
    }

    private void SpawnNewCard(int value, Transform transform)
    {
        GameObject go = Instantiate(_cardNumbrePrefab, transform.position, transform.rotation);
        go.GetComponent<CardNumber>().Init(value, false);
    }
}
