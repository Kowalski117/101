using System;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(PlayedDeck))]

public class Ability : MonoBehaviour
{
    [SerializeField] private GameDeck _gameDeck;

    public event UnityAction SuitChanged;

    private PlayedDeck _playedDeck;
    private int _takeCard = 0;
    private bool _isGiveCards;

    private void Start()
    {
        _playedDeck = GetComponent<PlayedDeck>();
    }

    public void Use(CardView card, CardView lastCardPlayed, Transform container)
    {
        switch (card.Name)
        {
            case NameCard.Six:

                if (lastCardPlayed.Name == card.Name && _isGiveCards == false)
                    _takeCard += 2;
                else
                {
                    _takeCard = 2;
                    _isGiveCards = false;
                }

                if (IsCardWithName(NameCard.Six, container) == false)
                {
                    _gameDeck.GiveCard(container, _takeCard);
                    _playedDeck.ChangeTurn();
                    _isGiveCards = true;
                }
                break;

            case NameCard.Seven:
                _gameDeck.GiveCard(container, 1);
                _playedDeck.ChangeTurn();
                break;

            case NameCard.Eigth:
                break;

            case NameCard.Nine:
                _playedDeck.ChangeTurn();
                break;

            case NameCard.Ten:
                break;

            case NameCard.Jack:
                break;

            case NameCard.Queen:

                if (_playedDeck.IsPlayerTurn == false)
                {
                    int indexSuit = UnityEngine.Random.Range(0, 3);
                    Suit suit = (Suit)Enum.GetValues(typeof(Suit)).GetValue(indexSuit);
                    _playedDeck.ChangeSuit(suit);
                    _playedDeck.ChangeTurn();
                    return;
                }
                else
                {
                    SuitChanged?.Invoke();
                }
                break;

            case NameCard.King:
                int value = 4;

                if (card.Suit == Suit.Spades)
                {
                    _gameDeck.GiveCard(container, value);
                    _playedDeck.ChangeTurn();
                }
                break;

            case NameCard.Ace:
                _playedDeck.ChangeTurn();
                break;
        }
        _playedDeck.ChangeTurn();
        _playedDeck.ChangeSuit(card.Suit);
    }

    private bool IsCardWithName(NameCard name, Transform container)
    {
        for (int i = 0; i < container.childCount; i++)
        {
            CardView card = container.GetChild(i).GetComponent<CardView>();

            if (card.Name == name)
                return true;
        }
        return false;
    }
}