using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class WriteTaskManager : MonoBehaviour
{
    [SerializeField] GameObject _prefabQuest;
    [SerializeField] GameObject _parentQuest;
    TaskManager _taskManager;
    GameObject _actualQuest;
    GameObject _lastQuest;

    private void Start()
    {
        foreach (var task in _taskManager._listTasksUncompleted)
        {
            _actualQuest = Instantiate(_prefabQuest);
            _actualQuest.GetComponent<WriteTaskOnUI>()._title.text = $"{task.GetComponent<Task>().Name}";
            _actualQuest.GetComponent<WriteTaskOnUI>()._description.text = $"{task.GetComponent<Task>().Description}";
            _actualQuest.GetComponent<WriteTaskOnUI>()._reward.text = $"{task.GetComponent<Task>().Reward}";

            _actualQuest.transform.parent = _parentQuest.transform;
            _actualQuest.transform.localPosition = new Vector3(0, 0, 0);
            if (_lastQuest != null)
            {
                _actualQuest.transform.localPosition = new Vector3(0,_lastQuest.transform.localPosition.x - 1000,0);

            }

            _lastQuest = _actualQuest;

            //_title.text = $"Title : {task.GetComponent<Task>().Name}";
            //_description.text = $"Description : {task.GetComponent<Task>().Description}";
            //_reward.text = $"Reward : {task.GetComponent<Task>().Reward}";
        }
    }

    private void Awake()
    {
        _taskManager = FindObjectOfType<TaskManager>();
    }
}