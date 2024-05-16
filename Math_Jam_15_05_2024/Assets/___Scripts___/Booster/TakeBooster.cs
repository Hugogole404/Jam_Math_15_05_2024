using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TakeBooster : MonoBehaviour
{
    [Header("--- Setup ---")]
    [SerializeField] private GameObject _prefabBooster;
    [SerializeField] private BoosterScriptable _boosterScriptable;
    
    [Header("--- values ---")]
    [SerializeField] private TMP_Text _boosterText;
    [SerializeField] private Vector3 _boosterJumpPos = new Vector3(0,1,5);
    [SerializeField] private float _timeBeforeTakeAnotherBooster = 1;

    int _boosterPrice = 5;
    private bool _canOpen;

    private void Start()
    {
        _canOpen = true;
        _boosterPrice = _boosterScriptable.Cost;
        SetText();
    }

    private void OnMouseDown()
    {
        // print("click on booster");
        
        if(CanBuyBooster())
            SpawnBooster();
    }

    private bool CanBuyBooster()
    {
        if (!_canOpen) return false;
        
        if (MoneyManager.Instance.CurrentMoney >= _boosterPrice)
        {
            MoneyManager.Instance.UpdateMoney(-_boosterPrice);
            return true;
        }

        return false;
    }

    private void SpawnBooster()
    {
        var pos = transform.position;
        GameObject go = Instantiate(_prefabBooster, new Vector3(pos.x, pos.y +1, pos.z), _prefabBooster.transform.rotation);
        go.GetComponent<Booster>().Init(_boosterScriptable, _boosterJumpPos);

        StartCoroutine(WaitTilPickUpAgainBooster());
    }

    private void SetText()
    {
        _boosterText.text = $"{_boosterPrice}";
    }

    IEnumerator WaitTilPickUpAgainBooster()
    {
        _canOpen = false;
        
        yield return new WaitForSeconds(_timeBeforeTakeAnotherBooster);
        
        _canOpen = true;
    }
}
