using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CardCreator))]

public class BuildingDeck : MonoBehaviour
{
    private List<Card> _deck = new List<Card>();
    private string _spritePath = "Sprite/Cards/";
    private CardCreator _cardCreator;

    public List<Card> Deck => _deck;

    private void Awake()
    {
        for (int i = 0; i < Enum.GetNames(typeof(Suit)).Length; i++)
        {
            for (int j = 0; j < Enum.GetNames(typeof(NameCard)).Length; j++)
            {
                _deck.Add(new Card((NameCard)Enum.GetValues(typeof(NameCard)).GetValue(j), (Suit)Enum.GetValues(typeof(Suit)).GetValue(i), _spritePath + (NameCard)Enum.GetValues(typeof(NameCard)).GetValue(j) + (Suit)Enum.GetValues(typeof(Suit)).GetValue(i)));
            }
        }
        _cardCreator = GetComponent<CardCreator>();
        _cardCreator.Render(Deck, this.transform);
    }
}