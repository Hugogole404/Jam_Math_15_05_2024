using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Card : MonoBehaviour
{
    [SerializeField] private TMP_Text _textDisplay;


    public void SetText(string text)
    {
        _textDisplay.text = text;
    }
}