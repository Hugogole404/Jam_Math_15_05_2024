using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardMove : MonoBehaviour
{
    Vector3 _mousePos;
    Camera _camera;
    Manager _manager;
    private Vector3 GetMousePos()
    {
        return  _manager.Camera.WorldToScreenPoint(transform.position);
    }
    private void OnMouseDown()
    {
        _mousePos = Input.mousePosition - GetMousePos();
    }
    private void OnMouseDrag()
    {
        transform.position = _manager.Camera.ScreenToWorldPoint(Input.mousePosition - _mousePos);
    }
    private void Start()
    {
        _manager = FindObjectOfType<Manager>();
    }
}