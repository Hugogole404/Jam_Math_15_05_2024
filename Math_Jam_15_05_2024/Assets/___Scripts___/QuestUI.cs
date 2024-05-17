using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestUI : MonoBehaviour
{
    [SerializeField] GameObject UI;
    private void OnMouseDown()
    {
        UI.SetActive(true);
        Timeline.Instance.UpdateTimelineState(false);
    }
    public void ClickCloseQuest()
    {
        UI.SetActive(false);
        Timeline.Instance.UpdateTimelineState(true);
    }
}