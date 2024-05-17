using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Timeline : MonoBehaviour
{
    public static Timeline Instance;
    
    [SerializeField] private float _duration = 20f; // Durée totale de la timeline en secondes
    [SerializeField] private Gradient _colorGradient; // Gradient de couleur pour représenter le temps restant
    [SerializeField] private AnimationCurve _widthCurve; // Courbe d'animation pour la largeur du sprite
    [SerializeField] private SpriteRenderer _spriteRenderer; // Référence au composant SpriteRenderer
    
    private float _startTime; // Temps de début de la timeline
    private float _startTimeSecond; // Temps de début de la timeline
    private bool _canGo;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        _startTimeSecond = _duration;
    }

    void Update()
    {
        if(_canGo)
            ReduceTime();
    }

    public void UpdateTimelineState(bool canGo)
    {
        _canGo = canGo;

        if(_canGo)
            _startTime = Time.time;
    }

    private void ReduceTime()
    {
        float elapsedTime = Time.time - _startTime;

        float progress = Mathf.Clamp01(elapsedTime / _duration);

        float width = _widthCurve.Evaluate(progress);
        transform.localScale = new Vector3(width, transform.localScale.y, transform.localScale.z);

        _spriteRenderer.color = _colorGradient.Evaluate(progress);

        if (elapsedTime >= _duration)
        {
            enabled = false;
        }
    }
    
    public void AddTime(float timeToAdd)
    {
        _duration += timeToAdd;
        
        if (_duration > _startTimeSecond)
            _duration = _startTimeSecond;
        
        _startTime = Time.time;
    }
}
