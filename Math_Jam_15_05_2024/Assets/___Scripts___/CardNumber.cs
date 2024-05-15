using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CardNumber : Card
{
    [field:SerializeField] public int Value { get; set; }
    public bool CanBeCut { get; set; }

    private void Start()
    {
        InitAnim();
    }

    public void Init(int newValue, bool canBeCut)
    {
        CanBeCut = canBeCut;
        
        if(newValue != 0)
            Value = newValue;
        
        SetText($"{Value}");
    }

    
}