using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CreateCards))]

public class BuildingDeck : MonoBehaviour
{
    private Transform _deckContainer;

    private List<Card> _deck = new List<Card>();
    private string _spritePath = "Sprite/Cards/";
    private CreateCards _createCards;

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
        _createCards = GetComponent<CreateCards>();
        _deckContainer = GetComponent<Transform>();
        _createCards.Render(Deck, _deckContainer);
    }
}