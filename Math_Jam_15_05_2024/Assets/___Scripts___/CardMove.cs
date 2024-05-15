using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using TMPro;
using UnityEngine;

public class CardMove : MonoBehaviour
{
    Vector3 _mousePos;
    Camera _camera;
    CardMoveManager _manager;

    public GameObject CardNeighbor { get; set; }

    private void Start()
    {
        _manager = FindObjectOfType<CardMoveManager>();
        _camera = _manager.Camera;
    }
}