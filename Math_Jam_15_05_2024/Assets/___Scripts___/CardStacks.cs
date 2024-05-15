using System;
using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class CardStacks : MonoBehaviour
{
    [field: SerializeField] public GameObject _parent { get; set; }

    private GameObject _topCard;
    private GameObject _botCard;

    private CardMoveManager _cardMoveManager;

    private void Awake()
    {
        _cardMoveManager = FindObjectOfType<CardMoveManager>();
    }

    private void Update()
    {
        gameObject.transform.localPosition = Vector3.zero;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CardMove>() != null)
        {
            if (_parent.transform.position.y >= other.gameObject.transform.position.y)
            {
                _topCard = gameObject;

                _botCard = other.gameObject;
                _parent.GetComponent<CardMove>().CardNeighbor = other.gameObject;
            }
            else
            {
                _topCard = other.gameObject;
                _botCard = gameObject;
            }

            _topCard.transform.DOMoveX(_botCard.transform.position.x, 0.5f);
            _topCard.transform.DOMoveZ(_botCard.transform.position.z - _cardMoveManager.OffsetStackCards, 0.5f);


            if (_parent.GetComponent<CardMathChara>())
            {
                GetAllParent();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<CardMove>() != null)
        {
            // if (other.gameObject == _botCard || _topCard == null)
                _parent.GetComponent<CardMove>().CardNeighbor = null;
        }
    }

    private void GetAllParent()
    {
        List<GameObject> cardsObj = new List<GameObject>();

        GameObject cardToAdd = _parent.GetComponent<CardMove>().CardNeighbor;

        int i = 0;

        while (cardToAdd != null || i == 15)
        {
            cardsObj.Add(cardToAdd);

            var cardNeighbor = cardToAdd.GetComponent<CardMove>().CardNeighbor;
            if (cardNeighbor != null && !cardsObj.Contains(cardNeighbor))
            {
                cardToAdd = cardNeighbor;
            }
            else
            {
                cardToAdd = null;
            }
            i++;
        }

        // foreach (var card in cardsObj)
        // {
        //     print(card.name);
        // }

        // Convert to list of Card
        List<Card> cards = new List<Card>();
        foreach (var card in cardsObj)
        {
            cards.Add(card.GetComponent<Card>());
        }

        cards.Reverse();
            
        _parent.GetComponent<CardMathChara>().SliderMgr.GoSlider(cards);
    }
}