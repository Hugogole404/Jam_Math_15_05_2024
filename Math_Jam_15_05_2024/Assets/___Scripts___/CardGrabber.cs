using System;
using System.Collections;
using System.Collections.Generic;
using System.Security;
using DG.Tweening;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class CardGrabber : MonoBehaviour
{
    public bool IsCardSelected;
    [SerializeField] bool _wantCursorVisibility;
    [SerializeField] float _heightToHoldCard;
    [SerializeField] LayerMask _layerMask;
    CardMoveManager _manager;
    Camera _camera;
    GameObject _selectedObject;
    float _speedSelectedObject;
    Vector3 _lastPos;

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
        //prendre la card 
        if (Input.GetMouseButtonDown(0))
        {
            if (_selectedObject == null)
            {
                IsCardSelected = true;
                RaycastHit hit = CastRay();

                if (hit.collider != null)
                {
                    // verif si obj est bien une card 
                    if (!hit.collider.gameObject.GetComponent<CardStacks>())
                    {
                        return;
                    }

                    // assigner la card 
                    _selectedObject = hit.collider.gameObject.transform.parent.gameObject;
                    _selectedObject.GetComponent<Rigidbody>().freezeRotation = false;
                    
                    //AudioManager.Instance.PlaySound("Attraper_une_carte");


                    if (_selectedObject.GetComponent<CardMathChara>())
                    {
                        _selectedObject.GetComponent<CardMathChara>().SliderMgr.HideStopSlider();
                    }

                    if (_wantCursorVisibility)
                        Cursor.visible = false;
                }
                else
                {
                    // si la card est nulle return
                    if (_selectedObject == null) return;

                    // bouger la card 

                    Vector3 position = new Vector3(Input.mousePosition.x, Input.mousePosition.y,
                        _camera.WorldToScreenPoint(_selectedObject.transform.position).z);
                    Vector3 worldPosition = _camera.ScreenToWorldPoint(position);
                    _selectedObject.transform.position = new Vector3(worldPosition.x, 0f, worldPosition.z);



                    _selectedObject = null;
                    if (_wantCursorVisibility) Cursor.visible = true;
                }
                _lastPos = _selectedObject.transform.position;
            }
        }
        // relacher la card 
        else if (Input.GetMouseButtonUp(0))
        {
            if (_selectedObject == null) return;

            _selectedObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            _selectedObject.transform.DORotate(new Vector3(0, 0, 0), 0);
            _selectedObject.GetComponent<Rigidbody>().freezeRotation = true;
            _selectedObject = null;
            if (_wantCursorVisibility) Cursor.visible = true;
            _lastPos = new Vector3(0, 0, 0);
            IsCardSelected = false;
        }

        // verif si la card n'est pas nulle 
        if (_selectedObject != null)
        {
            Vector3 position = new Vector3(Input.mousePosition.x, Input.mousePosition.y,
                _camera.WorldToScreenPoint(_selectedObject.transform.position).z);
            Vector3 worldPosition = _camera.ScreenToWorldPoint(position);
            _selectedObject.transform.position = new Vector3(worldPosition.x, _heightToHoldCard, worldPosition.z);
        }
    }
    private void VelocitySelectedObject()
    {
        if (_selectedObject != null)
        {
            _speedSelectedObject = _selectedObject.GetComponent<Rigidbody>().velocity.magnitude;
        }
    }

    private Vector3 Clampp(Vector3 vector, Vector3 min, Vector3 max)
    {
        float x = Mathf.Clamp(vector.x, min.x, max.x);
        float y = Mathf.Clamp(vector.y, min.y, max.y);
        float z = Mathf.Clamp(vector.z, min.z, max.z);

        return new Vector3(x, 0, z);
    }

    Vector3 rota;
    private void RotateCard()
    {
        if (_selectedObject != null)
        {
            var speed = _manager.Speed * Time.deltaTime;

            // gauche droite 
            if (_selectedObject.transform.position.x > _lastPos.x)
            {
                rota += new Vector3(0, 0, speed);
            }
            if (_selectedObject.transform.position.x < _lastPos.x)
            {
                rota -= new Vector3(0, 0, speed);
            }

            // haut bas 
            if (_selectedObject.transform.position.z > _lastPos.z)
            {
                rota -= new Vector3(speed, 0, 0);
            }
            if (_selectedObject.transform.position.z < _lastPos.z)
            {
                rota += new Vector3(speed, 0, 0);
            }

            rota = Clampp(rota, new Vector3(-_manager.TurnMaxAngles, 0,-_manager.TurnMaxAngles), new Vector3(_manager.TurnMaxAngles, 0, _manager.TurnMaxAngles));

            _selectedObject.transform.localEulerAngles = rota;

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
        VelocitySelectedObject();
        RotateCard();
    }
}