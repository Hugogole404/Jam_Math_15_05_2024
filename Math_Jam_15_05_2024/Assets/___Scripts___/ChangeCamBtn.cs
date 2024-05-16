using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCamBtn : MonoBehaviour
{
    [SerializeField] private Directions _direction;

    private void OnMouseDown()
    {
        CameraMovement.Instance.ChangeDir(_direction);
        AudioManager.Instance.PlaySound("Transitions_Menus");
    }
}
