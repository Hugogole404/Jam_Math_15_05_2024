using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardMathChara : Card
{
    [field:SerializeField] public SliderManager SliderMgr { get; set; }
    [SerializeField] private Renderer _renderer;
    [SerializeField] private Sprite _spriteMath;

    void Start()
    {
        InitAnim();
        
        Material material = _renderer.material;
        material.mainTexture = _spriteMath.texture;
        
        // SetText("");
    }
}