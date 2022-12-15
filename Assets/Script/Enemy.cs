using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Transform _enemyContainer;
    [SerializeField] private Transform _playedContainer;
    [SerializeField] private Transform _playerContainer;
    [SerializeField] private PlayedDeck _playedDeck;
    [SerializeField] private Ability _ability;
    [SerializeField] private GameDeck _gameDeck;
    [SerializeField] private AudioSource _audioSource;

    public void Move(bool isPlayerTurn, Suit usedSuid)
    {
        var lastCardPlayed = _playedDeck.GetLastCard(_playedContainer);

        for (int i = 0; i < _enemyContainer.childCount; i++)
        {
            CardView card = _enemyContainer.GetChild(i).GetComponent<CardView>();

            if (card.Name == lastCardPlayed.Name || card.Name == NameCard.Queen)
            {
                PutCard(card, lastCardPlayed, isPlayerTurn, usedSuid);
                return;
            }
        }

        for (int i = 0; i < _enemyContainer.childCount; i++)
        {
            CardView card = _enemyContainer.GetChild(i).GetComponent<CardView>();

            if (card.Suit == usedSuid)
            {
                PutCard(card, lastCardPlayed, isPlayerTurn, usedSuid);
                return;
            }
        }

        _gameDeck.GiveCard(_enemyContainer, 1);

        CardView lastCardEnemy = _playedDeck.GetLastCard(_enemyContainer);

        if (_playedDeck.IsPossibleMoveCard(lastCardEnemy))
        {
            PutCard(lastCardEnemy, lastCardPlayed, isPlayerTurn, usedSuid);
        }
        else
        {
            _playedDeck.ChangeTurn();
        }
    }

    private void PutCard(CardView card, CardView lastCardPlayed, bool isPlayerTurn, Suit usedSuid)
    {
        card.transform.SetParent(_playedContainer);
        card.SetSpriteCard();
        _ability.Use(card, lastCardPlayed, _playerContainer);
        _audioSource.Play();
    }
}
