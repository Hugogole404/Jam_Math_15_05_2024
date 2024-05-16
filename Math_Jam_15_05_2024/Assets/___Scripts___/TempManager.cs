using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempManager : MonoBehaviour
{
    [SerializeField] private List<CardNumber> _cards = new List<CardNumber>();

    private void Start()
    {
        foreach (var card in _cards)
        {
            if(card != null)
                card.Init(0, true);
        }
    }
}
