using System.Collections;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class CardOperator : Card
{
    [field: SerializeField] public Operators Operator { get; set; }
    [SerializeField] private Renderer _renderer;
    [SerializeField] private List<Sprite> _spritesOperator;

    private void Start()
    {
        Init(Operator);
        InitAnim();
    }

    public void Init(Operators newOpe)
    {
        Material material = _renderer.material;
        
        switch (newOpe)
        {
            case Operators.Sum:
                material.mainTexture = _spritesOperator[0].texture;
                SetText($"");
                break;
            case Operators.Substract:
                material.mainTexture = _spritesOperator[1].texture;
                SetText($"");
                break;
            case Operators.Multiply:
                material.mainTexture = _spritesOperator[2].texture;
                SetText($"");
                break;
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