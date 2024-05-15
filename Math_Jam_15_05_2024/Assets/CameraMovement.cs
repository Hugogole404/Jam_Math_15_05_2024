using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public static CameraMovement Instance;
    
    [SerializeField] private List<CamMovInfos> _camMovInfos = new List<CamMovInfos>();
    [SerializeField] private float _timeRota = 1;


    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        // ChangeDir((Directions)3);
    }

    public void ChangeDir(Directions newDir)
    {
        foreach (var info in _camMovInfos)
        {
            if (info.Directions == newDir)
            {
                MoveCam(info.Rotation);
                break;
            }
        }
    }

    private void MoveCam(Vector3 newRota)
    {
        gameObject.transform.DOKill();
        gameObject.transform.DORotate(newRota, _timeRota);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Keypad0))
            ChangeDir((Directions)0);
        if(Input.GetKeyDown(KeyCode.Keypad1))
            ChangeDir((Directions)1);
        if(Input.GetKeyDown(KeyCode.Keypad2))
            ChangeDir((Directions)2);
        if(Input.GetKeyDown(KeyCode.Keypad3))
            ChangeDir((Directions)3);
        if(Input.GetKeyDown(KeyCode.Keypad4))
            ChangeDir((Directions)4);
    }
}

[Serializable]
public class CamMovInfos
{
    public Directions Directions;
    public Vector3 Rotation;
}

public enum Directions
{
    Left = 0,
    Front = 1,
    Right = 2,
    Below = 3,
    Top = 4
}
