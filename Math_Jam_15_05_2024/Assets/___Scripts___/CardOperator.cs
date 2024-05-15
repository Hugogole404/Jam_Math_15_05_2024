using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardOperator : Card
{
    public Operators Operator { get; set; }
}

public enum Operators
{
    Sum = 0,
    Substract = 1,
    Multiply = 2,
    DivideEucli = 3
}