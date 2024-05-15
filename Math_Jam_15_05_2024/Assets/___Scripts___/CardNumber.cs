using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CardNumber : Card
{
    [field:SerializeField] public int Value { get; set; }

    private void Start()
    {
        Init(Value);
    }

    public void Init(int newValue)
    {
        SetText($"{newValue}");
        Value = newValue;
    }
}