using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewBoosterScriptable", menuName = "Custom/BoosterScriptable")]
public class BoosterScriptable : ScriptableObject
{
    [Header("-- Cost --")]
    public int Cost = 0;
    
    [Header("-- Pair --")]
    public int EvenNumbersCount = 0; // Nombre de nombres pairs
    public List<int> EvenNumbers = new List<int>(); // Liste de nombres pairs
    
    [Header("-- Impair --")]
    public int OddNumbersCount = 0; // Nombre de nombres impairs
    public List<int> OddNumbers = new List<int>(); // Liste de nombres impairs

    [Header("-- Operators --")]
    public int OperatorsCount = 0;
    public List<Operators> Operators = new List<Operators>(); // Liste des opérateurs

    [Header("-- Math --")]
    public int MathematiciansCount = 0; // Nombre de mathématiciens
}
