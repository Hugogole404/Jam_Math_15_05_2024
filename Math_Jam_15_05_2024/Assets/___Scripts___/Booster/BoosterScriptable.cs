using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewBoosterScriptable", menuName = "Custom/BoosterScriptable")]
public class BoosterScriptable : ScriptableObject
{
    [Header("-- Sprite --")] 
    public Sprite SpriteBooster;
    
    [Header("-- Cost --")]
    public int Cost = 0;
    
    [Header("-- Pair --")]
    public int EvenNumbersCount = 0; 
    public List<int> EvenNumbers = new List<int>();
    
    [Header("-- Impair --")]
    public int OddNumbersCount = 0; 
    public List<int> OddNumbers = new List<int>(); 

    [Header("-- Operators --")]
    public int OperatorsCount = 0;
    public List<Operators> Operators = new List<Operators>(); 

    [Header("-- Math --")]
    public int MathematiciansCount = 0;
}
