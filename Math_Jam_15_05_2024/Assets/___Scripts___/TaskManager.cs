using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    CheckTaskArea _taskArea;
    [SerializeField] private List<Task> _listTasksUncompleted;
    [SerializeField] private List<Task> _listTasksCompleted;
    private TaskCardColor _cardColor;
    private MoneyManager _moneyManager;
    private int _numberTaskCompletedOld;

    public void CheckTasks()
    {
        foreach (var task in _listTasksUncompleted)
        {
            if (task.GetComponent<Task>().State <= 1)
            {
                if (_taskArea.NumberEnter == task.GetComponent<Task>().Value)
                {
                    task.GetComponent<Task>().IsValidate = true;
                }
            }
            else if (task.GetComponent<Task>().State == 2)
            {
                //Vert
                if (_cardColor.ColorIntervalMax <= task.GetComponent<Task>().Value && _taskArea.NumberEnter <= _cardColor.ColorIntervalMax)
                {
                    task.GetComponent<Task>().IsValidate = true;
                    print("Vert");
                    Debug.Log(_cardColor.ColorIntervalMax);
                    Debug.Log(_taskArea.NumberEnter);
                }
                //Bleu
                else if (task.GetComponent<Task>().Value <= _cardColor.ColorIntervalMax1 && _taskArea.NumberEnter <= _cardColor.ColorIntervalMax1 && task.GetComponent<Task>().Value >_cardColor.ColorIntervalMax)
                {
                    task.GetComponent<Task>().IsValidate = true;
                    print("Blleu");
                }
                //Violet
                else if (task.GetComponent<Task>().Value <= _cardColor.ColorIntervalMax2 && _taskArea.NumberEnter <= _cardColor.ColorIntervalMax2 && task.GetComponent<Task>().Value >_cardColor.ColorIntervalMax1)
                {
                    task.GetComponent<Task>().IsValidate = true;
                    print("Violet");
                }
                //Rose
                else if (task.GetComponent<Task>().Value <= _cardColor.ColorIntervalMax3 && _taskArea.NumberEnter <= _cardColor.ColorIntervalMax3 && task.GetComponent<Task>().Value >_cardColor.ColorIntervalMax2)
                {
                    task.GetComponent<Task>().IsValidate = true;
                    print("Rose");
                }
                //Rouge
                else if (task.GetComponent<Task>().Value <= _cardColor.ColorIntervalMax4 && _taskArea.NumberEnter <= _cardColor.ColorIntervalMax4 && task.GetComponent<Task>().Value >_cardColor.ColorIntervalMax3)
                {
                    task.GetComponent<Task>().IsValidate = true;
                    print("Rouge");
                }
                //Orange
                else if (task.GetComponent<Task>().Value <= _cardColor.ColorIntervalMax5 && _taskArea.NumberEnter <= _cardColor.ColorIntervalMax5 && task.GetComponent<Task>().Value >_cardColor.ColorIntervalMax4)
                {
                    task.GetComponent<Task>().IsValidate = true;
                    print("Orange");
                }
                //Jaune
                else if (task.GetComponent<Task>().Value > _cardColor.ColorIntervalMax5 && _taskArea.NumberEnter > _cardColor.ColorIntervalMax5)
                {
                    task.GetComponent<Task>().IsValidate = true;
                    print("Jaune");
                }
            }
            else if (task.GetComponent<Task>().State == 3)
            {
                if (_taskArea.NumberEnter < task.GetComponent<Task>().Value)
                {
                    task.GetComponent<Task>().IsValidate = true;
                }
            }
            else if (task.GetComponent<Task>().State >= 4)
            {
                if (_taskArea.NumberEnter > task.GetComponent<Task>().Value)
                {
                    task.GetComponent<Task>().IsValidate = true;
                }
            }
        }
        List<Task> tempTasks = new List<Task>();
        foreach (var item in _listTasksUncompleted)
        {
            if (item.GetComponent<Task>().IsValidate == false)
            {
                tempTasks.Add(item);
            }
            else
            {
                _moneyManager.UpdateMoney(item.GetComponent<Task>().Reward);
                _listTasksCompleted.Add(item);
            }
        }
        _listTasksUncompleted.Clear();
        foreach (var item in tempTasks)
        {
            _listTasksUncompleted.Add(item);
        }
        //_listTasksUncompleted = tempTasks;
        tempTasks.Clear();

        if (_listTasksCompleted.Count > _numberTaskCompletedOld)
        {
            Destroy(_taskArea.ActualCard);
        }

        _numberTaskCompletedOld = _listTasksCompleted.Count;
    }

    private void Awake()
    {
        _taskArea = FindObjectOfType<CheckTaskArea>();
        _cardColor = FindObjectOfType<TaskCardColor>();
        _moneyManager = FindObjectOfType<MoneyManager>();
    }
}