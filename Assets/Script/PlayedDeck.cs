using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class PlayedDeck : MonoBehaviour
{
    [SerializeField] private Transform _playerContainer;
    [SerializeField] private Transform _enemyContainer;
    [SerializeField] private Transform _playedContainer;

    [SerializeField] private Enemy _enemy;
    [SerializeField] private Ability _ability;
    [SerializeField] private GameDeck _gameDeck;
    [SerializeField] private AudioSource _audioSource;

    static public UnityAction<Suit> SuidChanged;
    static public UnityAction<bool> MoveChange;
    static public UnityAction EndTheGame;
    static public UnityAction ChangedTurn;

    private int _turnTime;
    private int _turn;
    private bool _isPlayerTurn => _turn % 2 != 0;
    private Coroutine _coroutine;
    private Coroutine _wait;
    private Suit _usedSuid;

    public int MaxTurnTime => 15;
    public int TurnTime => _turnTime;
    public bool IsPlayerTurn => _isPlayerTurn;
    public Suit UsedSuit;

    public void StartGame()
    {
        _turn = 0;
        DealCards();
        var lastCardPlayed = GetLastCard(_playedContainer);
        ChangeSuit(lastCardPlayed.Suit);
        ChangeTurn();
    }

    public bool CanPut(CardView card)
    {
        if (!_isPlayerTurn)
            return false;

        var lastCardPlayed = GetLastCard(_playedContainer);

        if (IsPossibleMoveCard(card))
        {
            _ability.Use(card, lastCardPlayed, _enemyContainer);
            _audioSource.Play();
            return true;
        }
        else
        {
            return false;
        }
    }

    public void ChangeSuit(Suit suit)
    {
        _usedSuid = suit;
        SuidChanged?.Invoke(suit);
    }

    public void ChangeTurn()
    {
        StopCoroutine(_coroutine);
        StopCoroutine(_wait);
        _turn++;

        if (IsGameOver())
        {
            GameOver();
            return;
        }

        if (_isPlayerTurn == false)
        {
            float waitSecond = UnityEngine.Random.Range(1, 5);
            _wait = StartCoroutine(WaitBeforeGoing(waitSecond));
        }
        ChangedTurn?.Invoke();
        MoveChange?.Invoke(_isPlayerTurn);
        _coroutine = StartCoroutine(TurnFunc());
    }


    public CardView GetLastCard(Transform container)
    {
        return container.GetChild(container.childCount - 1).GetComponent<CardView>();
    }

    public bool IsPossibleMoveCard(CardView card)
    {
        var lastCardPlayed = GetLastCard(_playedContainer);

        if (card.Name == lastCardPlayed.Name || card.Suit == _usedSuid || card.Name == NameCard.Queen)
            return true;

        else
            return false;
    }

    private void DealCards()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);
        if (_wait != null)
            StopCoroutine(_wait);

        _gameDeck.GiveCard(_playedContainer, 1);
        _gameDeck.GiveCard(_playerContainer, 5);
        _gameDeck.GiveCard(_enemyContainer, 5);
        _coroutine = StartCoroutine(TurnFunc());
        _wait = StartCoroutine(WaitBeforeGoing(5));
    }

    private bool IsGameOver()
    {
        if (_playerContainer.childCount <= 0 || _enemyContainer.childCount <= 0)
            return true;

        else
            return false;
    }

    private void GameOver()
    {
        EndTheGame?.Invoke();
    }

    IEnumerator TurnFunc()
    {
        _turnTime = MaxTurnTime;

        if (_isPlayerTurn)
        {
            while (_turnTime-- > 0)
            {
                yield return new WaitForSeconds(1);
            }
            if (_turnTime < 0)
            {
                _gameDeck.GiveCard(_playerContainer, 1);
            }
        }
        else
        {
            while (_turnTime-- > 0)
            {
                yield return new WaitForSeconds(1);
            }
            if (_turnTime < 0)
            {
                _gameDeck.GiveCard(_enemyContainer, 1);
            }
        }
        ChangeTurn();
    }

    IEnumerator WaitBeforeGoing(float value)
    {
        yield return new WaitForSeconds(value);
        _enemy.Move(_isPlayerTurn, _usedSuid);
    }
}
