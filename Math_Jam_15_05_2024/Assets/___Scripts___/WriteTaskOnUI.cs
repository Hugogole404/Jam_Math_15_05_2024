using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WriteTaskOnUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _title;
    [SerializeField] TextMeshProUGUI _description;
    [SerializeField] TextMeshProUGUI _reward;

    TaskManager _taskManager;

    private void Update()
    {
        foreach (var task in _taskManager._listTasksUncompleted)
        {
            _title.text = $"Title : {task.GetComponent<Task>().Name}";
            _description.text = $"Description : {task.GetComponent<Task>().Description}";
            _reward.text = $"Reward : {task.GetComponent<Task>().Reward}";
        }
    }

    private void Awake()
    {
        _taskManager = FindObjectOfType<TaskManager>();
    }
}