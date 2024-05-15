using System.Collections;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CardOperator : Card
{
    [field:SerializeField] public Operators Operator { get; set; }
    
    private void Start()
    {
        Init(Operator);
        InitAnim();
    }

    public void Init(Operators newOpe)
    {
        switch (newOpe)
        {
            case Operators.Equal:
                SetText($"=");
                break;
            case Operators.Sum:
                SetText($"+");
                break;
            case Operators.Substract:
                SetText($"-");
                break;
            case Operators.Multiply:
                SetText($"*");
                break;
            // case Operators.DivideEucli:
            //     SetText($"/");
            //     break;
        }

        Operator = newOpe;
    }
}

public enum Operators
{
    Sum = 0,
    Substract = 1,
    Multiply = 2,
    Equal = 4
}