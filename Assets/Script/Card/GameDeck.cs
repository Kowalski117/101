using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDeck : MonoBehaviour
{
    private BuildingDeck _buildingDeck;
    private List<Card> _deck = new List<Card>();

    private void Start()
    {
        _deck = _buildingDeck.Deck;
    }
}
