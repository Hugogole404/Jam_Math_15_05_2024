using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCamBtn : MonoBehaviour
{
    [SerializeField] private Directions _direction;
    [SerializeField] private bool _canInteractTimeline;
    [SerializeField] private bool _canGoTimeline;

    private void OnMouseDown()
    {
        CameraMovement.Instance.ChangeDir(_direction);
        AudioManager.Instance.PlaySound("Transitions_Menus");
        
        if(_canInteractTimeline)
            Timeline.Instance.UpdateTimelineState(_canGoTimeline);
    }
}
