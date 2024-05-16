using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.IO;

[Serializable]
public class Task : MonoBehaviour
{
    public string Name;
    [TextArea(5, 5)] public string Description;
    public int State;
    public int Value;
    public int Reward;
    public bool IsValidate;
}