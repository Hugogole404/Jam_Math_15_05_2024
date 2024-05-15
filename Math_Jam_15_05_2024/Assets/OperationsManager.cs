using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class OperationsManager : MonoBehaviour
{
    public static OperationsManager Instance;

    [SerializeField] private CardOrderManager _cardOrderManager;
    
    // private List<Card> _cards = new List<Card>();
    
    
    // private void OnTriggerEnter(Collider other)
    // {
    //     if (other.gameObject.GetComponent<Card>())
    //     {
    //         print("Detect Card");
    //         Card newCard = other.gameObject.GetComponent<Card>();
    //         AddCard(newCard);
    //     }
    // }

    // public void AddCard(Card newCard)
    // {
    //     _cards.Add(newCard);
    //
    //     CardOperator ope = newCard.GetComponent<CardOperator>();
    //     
    //     if(ope != null && ope.Operator == Operators.Equal)
    //         GoCalcul();
    // }
    //
    // public void RemoveCard(Card card)
    // {
    //     if(_cards.Contains(card))
    //         _cards.Remove(card);
    // }

    private void Awake()
    {
        Instance = this;
    }

    public void GoCalcul(List<Card> cards)
    {
        print("go calcul");
        var SortedCards = _cardOrderManager.SortCard(cards);
        
        
        
        float result = Calculate(SortedCards);
        
        // Afficher le résultat ou un message d'erreur si le calcul est impossible
        if (float.IsNaN(result))
        {
            Debug.Log("Impossible de calculer le résultat.");
        }
        else
        {
            Debug.Log("Le résultat du calcul est : " + result);
        }
    }

    float Calculate(List<Card> cards)
    {
        // Initialiser le résultat avec la valeur du premier chiffre
        float result = 0f;
        if (cards.Count > 0 && cards[0] is CardNumber)
        {
            result = ((CardNumber)cards[0]).Value;
        }
        else
        {
            return float.NaN; // Renvoyer NaN si le premier élément n'est pas un chiffre
        }
    
        // Parcourir les cartes suivantes et appliquer les opérateurs
        for (int i = 1; i < cards.Count; i++)
        {
            if (cards[i] is CardOperator)
            {
                CardOperator op = (CardOperator)cards[i];
                switch (op.Operator)
                {
                    case Operators.Sum:
                        if (i + 1 < cards.Count && cards[i + 1] is CardNumber)
                        {
                            result += ((CardNumber)cards[i + 1]).Value;
                            i++; // Sauter le prochain nombre car il a déjà été utilisé
                        }
                        else
                        {
                            return float.NaN; // Renvoyer NaN si l'opération n'est pas possible
                        }
                        break;
                    case Operators.Substract:
                        if (i + 1 < cards.Count && cards[i + 1] is CardNumber)
                        {
                            result -= ((CardNumber)cards[i + 1]).Value;
                            i++;
                        }
                        else
                        {
                            return float.NaN;
                        }
                        break;
                    case Operators.Multiply:
                        if (i + 1 < cards.Count && cards[i + 1] is CardNumber)
                        {
                            result *= ((CardNumber)cards[i + 1]).Value;
                            i++;
                        }
                        else
                        {
                            return float.NaN;
                        }
                        break;
                   
                    case Operators.Equal:
                        // Ne rien faire pour l'opérateur égal, il a déjà été pris en compte dans le calcul précédent
                        break;
                }
            }
            else
            {
                return float.NaN; // Renvoyer NaN si une carte autre qu'un opérateur est trouvée
            }
        }
    
        return result;
    }
}