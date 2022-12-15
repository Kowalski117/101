using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class GameDeck : MonoBehaviour
{
    [SerializeField] private PlayedDeck _playedDeck;
    [SerializeField] private Transform _deckContainer;
    [SerializeField] private Transform _playerContainer;
    [SerializeField] private Transform _playedContainer;
    [SerializeField] private AudioSource _audioSource;

    private Transform _card;

    public event UnityAction<bool> ButtonPressed;

    public void OnClick()
    {
        ButtonPressed?.Invoke(false);
        GiveCard(_playerContainer, 1);
        CardView lastCardPlayer = _playedDeck.GetLastCard(_playerContainer);

        if (_playedDeck.IsPossibleMoveCard(lastCardPlayer) == false)
        {
            _playedDeck.ChangeTurn();
        }
    }

    public void GiveCard(Transform container, int value)
    {
        int minValue = 1;

        for (int i = 0; i < value; i++)
        {
            Transform[] cards = _deckContainer.GetComponentsInChildren<Transform>();

            if (cards.Length <= 2)
            {
                StartCoroutine(ShuffleCards(_deckContainer));
            }

            if (cards.Length != 1)
            {
                int number = Random.Range(minValue, cards.Length);
                _card = cards[number];
                _card.SetParent(container);
                DefineDeck(container);
                _audioSource.Play();
            }
        }
    }

    private void DefineDeck(Transform container)
    {
        if (container == _playerContainer || container == _playedContainer)
        {
            _card.GetComponent<CardView>().SetSpriteCard();
        }
        else
            _card.GetComponent<CardView>().SetSpriteShirt();
    }

    IEnumerator ShuffleCards(Transform container)
    {
        Transform[] transforms = _playedContainer.GetComponentsInChildren<Transform>();

        for (int i = 1; i < transforms.Length - 1; i++)
        {
            _card = transforms[i];
            _card.SetParent(container);
            DefineDeck(container);
            _audioSource.Play();
            yield return new WaitForSeconds(0.1f);
        }
    }
}
