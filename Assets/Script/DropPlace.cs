using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(PlayedDeck))]

public class DropPlace : MonoBehaviour, IDropHandler
{
    private PlayedDeck _gameDeck;

    private void Start()
    {
        _gameDeck = GetComponent<PlayedDeck>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        CardMovement card = eventData.pointerDrag.GetComponent<CardMovement>();
        CardView cards = eventData.pointerDrag.GetComponent<CardView>();

        if (card && _gameDeck.CanPut(cards))
            card.SetDefultParent(transform);
    }
}
