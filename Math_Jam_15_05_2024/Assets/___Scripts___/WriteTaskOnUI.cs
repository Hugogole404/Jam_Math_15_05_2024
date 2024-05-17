using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WriteTaskOnUI : MonoBehaviour
{
    public TextMeshProUGUI _title;
    public TextMeshProUGUI _description;
    public TextMeshProUGUI _reward;
    List<Task> _tasksList = new List<Task>();

    TaskManager _taskManager;

    private void Update()
    {
        //foreach (var task in _taskManager._listTasksUncompleted)
        //{
        //    _title.text = $"Title : {task.GetComponent<Task>().Name}";
        //    _description.text = $"Description : {task.GetComponent<Task>().Description}";
        //    _reward.text = $"Reward : {task.GetComponent<Task>().Reward}";
        //}
    }

    private void Awake()
    {
        _taskManager = FindObjectOfType<TaskManager>();
    }
}