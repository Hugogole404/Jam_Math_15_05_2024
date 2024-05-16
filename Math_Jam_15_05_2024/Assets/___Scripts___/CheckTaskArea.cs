using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckTaskArea : MonoBehaviour
{
    CardGrabber _grabber;
    TaskManager _taskManager;
    [HideInInspector] public int NumberEnter;
    public GameObject ActualCard;

    private void OnTriggerStay(Collider other)
    {
        if(_grabber.IsCardSelected == false && other.GetComponent<CardNumber>() != null)
        {
            ActualCard = other.gameObject;
            NumberEnter = other.GetComponent<CardNumber>().Value;
            _taskManager.CheckTasks();
        }
    }
    private void Awake()
    {
        _grabber = FindObjectOfType<CardGrabber>();
        _taskManager = FindObjectOfType<TaskManager>();
    }
}