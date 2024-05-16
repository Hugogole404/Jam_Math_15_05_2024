using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    CheckTaskArea _taskArea;

    private void Awake()
    {
        _taskArea = FindObjectOfType<CheckTaskArea>();
    }
}