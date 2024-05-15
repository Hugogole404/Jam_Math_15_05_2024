using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class CardStacks : MonoBehaviour
{
    GameObject _topCard;
    GameObject _botCard;

    CardMoveManager _cardMoveManager;
    public bool IsPlaced;
    private void OnTriggerEnter(Collider other)
    {
        if (IsPlaced == false)
        {
            if (other.GetComponent<CardMove>() != null)
            {
                if (gameObject.transform.parent.transform.position.y >= other.gameObject.transform.position.y)
                {
                    _topCard = gameObject;
                    _botCard = other.gameObject;
                }
                else
                {
                    _topCard = other.gameObject;
                    _botCard = gameObject;
                }
                //_topCard.transform.position = new Vector3(_botCard.transform.position.x,
                //    _topCard.transform.position.y,
                //    _topCard.transform.position.z - _cardMoveManager.OffsetStackCards);
                _topCard.transform.DOMoveX(_botCard.transform.position.x, 0.5f);
                _topCard.transform.DOMoveZ(_botCard.transform.position.z - _cardMoveManager.OffsetStackCards, 0.5f);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        IsPlaced = false;
    }
    private void Awake()
    {
        _cardMoveManager = FindObjectOfType<CardMoveManager>();
    }
}