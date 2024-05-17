using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    CheckTaskArea _taskArea;
    [SerializeField] public List<Task> _listTasksUncompleted;
    [SerializeField] private List<Task> _listTasksCompleted;
    private TaskCardColor _cardColor;
    private MoneyManager _moneyManager;
    private int _numberTaskCompletedOld;

    public void CheckTasks()
    {
        bool isTaskValid = false;
        foreach (var task in _listTasksUncompleted)
        {
            if (isTaskValid == false)
            {

                if (task.GetComponent<Task>().State <= 1)
                {
                    if (_taskArea.NumberEnter == task.GetComponent<Task>().Value)
                    {
                        task.GetComponent<Task>().IsValidate = true;
                        isTaskValid = true;
                    }
                }
                else if (task.GetComponent<Task>().State == 2)
                {
                    // Vert 
                    print(task.GetComponent<Task>().Value);
                    print(task.GetComponent<Task>().IsGreen);
                    print(task.GetComponent<Task>().IsGreen);

                    if (task.GetComponent<Task>().IsGreen)
                    {
                        if (_taskArea.NumberEnter <= 9)
                        {
                            task.GetComponent<Task>().IsValidate = true;
                            isTaskValid = true;
                            print("Vert");
                        }
                    }
                    // Bleu
                    else if (task.GetComponent<Task>().IsBlue && _taskArea.NumberEnter <= _cardColor.ColorBlueMaxVal && _taskArea.NumberEnter > _cardColor.ColorGreenMaxVal)
                    {
                        task.GetComponent<Task>().IsValidate = true;
                        isTaskValid = true;
                        print("Blue");
                    }
                    // Violet
                    else if (task.GetComponent<Task>().IsViolet && _taskArea.NumberEnter <= _cardColor.ColorVioletMaxVal && _taskArea.NumberEnter > _cardColor.ColorBlueMaxVal)
                    {
                        task.GetComponent<Task>().IsValidate = true;
                        isTaskValid = true;
                        print("Violet");
                    }
                    // Rose
                    else if (task.GetComponent<Task>().IsPink && _taskArea.NumberEnter <= _cardColor.ColorPinkMaxVal && _taskArea.NumberEnter > _cardColor.ColorVioletMaxVal)
                    {
                        task.GetComponent<Task>().IsValidate = true;
                        isTaskValid = true;
                        print("Pink");
                    }
                    // Rouge
                    else if (task.GetComponent<Task>().IsRed && _taskArea.NumberEnter <= _cardColor.ColorRedMaxVal && _taskArea.NumberEnter > _cardColor.ColorPinkMaxVal)
                    {
                        task.GetComponent<Task>().IsValidate = true;
                        isTaskValid = true;
                        print("Red");
                    }
                    // Orange
                    else if (task.GetComponent<Task>().IsOrange && _taskArea.NumberEnter <= _cardColor.ColorOrangeMaxVal && _taskArea.NumberEnter > _cardColor.ColorRedMaxVal)
                    {
                        task.GetComponent<Task>().IsValidate = true;
                        isTaskValid = true;
                        print("Orange");
                    }
                    // Yellow
                    else if (task.GetComponent<Task>().IsYellow && /*_taskArea.NumberEnter <= 199 && */_taskArea.NumberEnter > _cardColor.ColorOrangeMaxVal)
                    {
                        task.GetComponent<Task>().IsValidate = true;
                        isTaskValid = true;
                        print("Yellow");
                    }


                    ////Vert
                    ////if (_cardColor.ColorIntervalMax >= task.GetComponent<Task>().Value && _taskArea.NumberEnter <= _cardColor.ColorIntervalMax)
                    ////{
                    ////    task.GetComponent<Task>().IsValidate = true;
                    ////    isTaskValid = true;
                    ////    print("Vert");
                    ////}
                    //if (_taskArea.NumberEnter <= 9 && 9 >= task.GetComponent<Task>().Value)
                    //{
                    //    task.GetComponent<Task>().IsValidate = true;
                    //    isTaskValid = true;
                    //    print("Vert");
                    //}
                    ////Bleu
                    //else if (19 >= task.GetComponent<Task>().Value && _taskArea.NumberEnter <= 19 && 9 < task.GetComponent<Task>().Value)
                    //{
                    //    task.GetComponent<Task>().IsValidate = true;
                    //    isTaskValid = true;
                    //    print("Bleu");
                    //}
                    ////Violet
                    //else if (49 >= task.GetComponent<Task>().Value && _taskArea.NumberEnter <= 49 && 29 < task.GetComponent<Task>().Value)
                    //{
                    //    task.GetComponent<Task>().IsValidate = true;
                    //    isTaskValid = true;
                    //    print("Violet");
                    //}
                    ////Violet
                    ////else if (task.GetComponent<Task>().Value >= _cardColor.ColorIntervalMax2 && _taskArea.NumberEnter <= _cardColor.ColorIntervalMax2 && task.GetComponent<Task>().Value > _cardColor.ColorIntervalMax1)
                    ////{
                    ////    task.GetComponent<Task>().IsValidate = true;
                    ////    isTaskValid = true;
                    ////    print("Violet");
                    ////}
                    ////Rose
                    //else if (task.GetComponent<Task>().Value >= _cardColor.ColorIntervalMax3 && _taskArea.NumberEnter <= _cardColor.ColorIntervalMax3 && task.GetComponent<Task>().Value > _cardColor.ColorIntervalMax2)
                    //{
                    //    task.GetComponent<Task>().IsValidate = true;
                    //    isTaskValid = true;
                    //    print("Rose");
                    //}
                    ////Rouge
                    //else if (task.GetComponent<Task>().Value >= _cardColor.ColorIntervalMax4 && _taskArea.NumberEnter <= _cardColor.ColorIntervalMax4 && task.GetComponent<Task>().Value > _cardColor.ColorIntervalMax3)
                    //{
                    //    task.GetComponent<Task>().IsValidate = true;
                    //    isTaskValid = true;
                    //    print("Rouge");
                    //}
                    ////Orange
                    //else if (task.GetComponent<Task>().Value >= _cardColor.ColorIntervalMax5 && _taskArea.NumberEnter <= _cardColor.ColorIntervalMax5 && task.GetComponent<Task>().Value > _cardColor.ColorIntervalMax4)
                    //{
                    //    task.GetComponent<Task>().IsValidate = true;
                    //    isTaskValid = true;
                    //    print("Orange");
                    //}
                    ////Jaune
                    //else if (task.GetComponent<Task>().Value > _cardColor.ColorIntervalMax5 && _taskArea.NumberEnter > _cardColor.ColorIntervalMax5)
                    //{
                    //    task.GetComponent<Task>().IsValidate = true;
                    //    isTaskValid = true;
                    //    print("Jaune");
                    //}
                }
                else if (task.GetComponent<Task>().State == 3)
                {
                    if (_taskArea.NumberEnter < task.GetComponent<Task>().Value)
                    {
                        task.GetComponent<Task>().IsValidate = true;
                        isTaskValid = true;
                    }
                }
                else if (task.GetComponent<Task>().State >= 4)
                {
                    if (_taskArea.NumberEnter > task.GetComponent<Task>().Value)
                    {
                        task.GetComponent<Task>().IsValidate = true;
                        isTaskValid = true;
                    }
                }
            }
        }
        isTaskValid = false;
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
                Timeline.Instance.AddTime(15);
                AudioManager.Instance.PlaySound("Taches_terminee");
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