using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckTaskArea : MonoBehaviour
{
    [SerializeField] public int _wantedValue;
    [SerializeField] public int _wantedColor;
    CardGrabber _grabber;
    private void OnTriggerEnter(Collider other)
    {
        if(_grabber.IsCardSelected == false && other.GetComponent<CardNumber>() != null)
        {
            if(other.GetComponent<CardNumber>().Value == _wantedValue)
            {
                Cursor.visible = true;
                Destroy(other.gameObject);
            }
        }
    }
    private void Awake()
    {
        _grabber = FindObjectOfType<CardGrabber>();
    }
}