using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public enum NameCard
{
    Six,
    Seven,
    Eigth,
    Nine,
    Ten,
    Jack,
    Queen,
    King,
    Ace,
}

public enum Suit
{
    Diamonds,
    Hearts,
    Clubs, 
    Spades,
}

public class Card
{
    private Sprite _sprite;

    public Card(NameCard name, Suit suit,string sprite)
    {
        Name = name;
        Suit = suit;
        _sprite = Resources.Load<Sprite>(sprite);
    }

    public NameCard Name { get; private set; }
    public Suit Suit { get; private set; }
}
