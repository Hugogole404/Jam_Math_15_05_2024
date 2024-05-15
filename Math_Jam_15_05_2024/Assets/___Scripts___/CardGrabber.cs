using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CardGrabber : MonoBehaviour
{
    [SerializeField] bool _wantCursorVisibility;
    [SerializeField] float _heightToHoldCard;
    [SerializeField] LayerMask _layerMask;
    CardMoveManager _manager;
    Camera _camera;
    GameObject _selectedObject;

    private RaycastHit CastRay()
    {
        Vector3 screenMousePosFar = new Vector3(Input.mousePosition.x, Input.mousePosition.y, _camera.farClipPlane);
        Vector3 screenMousePosNear = new Vector3(Input.mousePosition.x, Input.mousePosition.y, _camera.nearClipPlane);

        Vector3 wolrdPosFar = _camera.ScreenToWorldPoint(screenMousePosFar);
        Vector3 wolrdPosNear = _camera.ScreenToWorldPoint(screenMousePosNear);
        RaycastHit hit;
        Physics.Raycast(wolrdPosNear, wolrdPosFar - wolrdPosNear, out hit, Mathf.Infinity, _layerMask);
        return hit;
    }

    private void CheckPressMouse()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (_selectedObject == null)
            {
                RaycastHit hit = CastRay();

                // if (hit == null) return;

                if (hit.collider != null)
                {
                    if (!hit.collider.gameObject.GetComponent<CardStacks>())
                    {
                        return;
                    }

                    _selectedObject = hit.collider.gameObject.transform.parent.gameObject;
                    _selectedObject.GetComponent<Rigidbody>().freezeRotation = false;
                    if (_wantCursorVisibility) Cursor.visible = false;
                }
                else
                {
                    if (_selectedObject == null) return;

                    //_selectedObject.GetComponent<Rigidbody>().freezeRotation = false;
                    Vector3 position = new Vector3(Input.mousePosition.x, Input.mousePosition.y,
                        _camera.WorldToScreenPoint(_selectedObject.transform.position).z);
                    Vector3 worldPosition = _camera.ScreenToWorldPoint(position);
                    _selectedObject.transform.position = new Vector3(worldPosition.x, 0f, worldPosition.z);

                    _selectedObject = null;
                    if (_wantCursorVisibility) Cursor.visible = true;
                }
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (_selectedObject == null) return;

            _selectedObject.transform.DORotate(new Vector3(0, 180, 0), 0);
            _selectedObject.GetComponent<Rigidbody>().freezeRotation = true;
            _selectedObject = null;
            if (_wantCursorVisibility) Cursor.visible = true;
        }

        if (_selectedObject != null)
        {
            Vector3 position = new Vector3(Input.mousePosition.x, Input.mousePosition.y,
                _camera.WorldToScreenPoint(_selectedObject.transform.position).z);
            Vector3 worldPosition = _camera.ScreenToWorldPoint(position);
            _selectedObject.transform.position = new Vector3(worldPosition.x, _heightToHoldCard, worldPosition.z);
        }
    }

    private void Start()
    {
        _manager = FindFirstObjectByType<CardMoveManager>();
        _camera = _manager.Camera;
    }

    private void Update()
    {
        CheckPressMouse();
    }
}