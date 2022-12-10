using System.Collections.Generic;
using UnityEngine;

public class CardCreator : MonoBehaviour
{
    [SerializeField] private CardView _cardView;

    public void Render(List<Card> cards, Transform container)
    {
        foreach (Card card in cards)
        {
            var view = Instantiate(_cardView, container);
            view.Render(card);
        }
    }
}
