using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager Instance;

    [field:SerializeField] public int CurrentMoney { get; set; }
    [SerializeField] private TMP_Text _moneyText;
    
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        SetText();
    }

    public void UpdateMoney(int add)
    {
        CurrentMoney += add;
        SetText();
    }
    
    private void SetText()
    {
        _moneyText.text =$"{CurrentMoney}";
    }
}
