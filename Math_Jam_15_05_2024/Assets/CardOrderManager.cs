using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CardOrderManager : MonoBehaviour
{
    public List<Card> SortCard(List<Card> cards)
    {
        cards = cards.OrderByDescending(card => Mathf.Abs(card.transform.position.x - gameObject.transform.position.x)).ToList();

        return cards;
    }
}
