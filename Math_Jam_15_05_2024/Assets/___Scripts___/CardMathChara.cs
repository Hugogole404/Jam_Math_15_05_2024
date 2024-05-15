using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardMathChara : Card
{
    [field:SerializeField] public SliderManager SliderMgr { get; set; }
    void Start()
    {
        InitAnim();
    }
}