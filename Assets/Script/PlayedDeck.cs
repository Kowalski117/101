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

    static public event UnityAction<Suit> SuitChanged;
    static public event UnityAction<bool> MoveChanged;
    static public event UnityAction EndTheGame;
    static public event UnityAction ChangedTurn;

    private float _turnTime;
    private float _turn;
    private bool _isPlayerTurn => _turn % 2 != 0;
    private bool _isGameProgress = false;
    private Coroutine _wait;
    private Suit _usedSuid;

    public int MaxTurnTime => 15;
    public float TurnTime => _turnTime;
    public bool IsPlayerTurn => _isPlayerTurn;
    public Suit UsedSuit;

    private void Update()
    {
        if (_isGameProgress)
        {
            _turnTime -= Time.deltaTime;

            if (_turnTime <= 0)
                EndTimeTurn();
        }
    }

    public void StartGame()
    {
        _turnTime = MaxTurnTime;
        _isGameProgress = true;
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
        SuitChanged?.Invoke(suit);
    }

    public void ChangeTurn()
    {
        _turnTime = MaxTurnTime;
        _turn++;

        if (_wait != null)
            StopCoroutine(_wait);

        if (_isPlayerTurn == false)
        {
            float waitSecond = Random.Range(1, 5);
            _wait = StartCoroutine(WaitBeforeGoing(waitSecond));
        }

        if (IsGameOver())
        {
            GameOver();
            return;
        }

        ChangedTurn?.Invoke();
        MoveChanged?.Invoke(_isPlayerTurn);
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
        _gameDeck.GiveCard(_playedContainer, 1);
        _gameDeck.GiveCard(_playerContainer, 5);
        _gameDeck.GiveCard(_enemyContainer, 5);
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
        _isGameProgress = false;
        EndTheGame?.Invoke();
    }

    private void EndTimeTurn()
    {
        if (_isPlayerTurn)
            _gameDeck.GiveCard(_playerContainer, 1);
        else
            _gameDeck.GiveCard(_enemyContainer, 1);
        ChangeTurn();
    }

    private IEnumerator TurnFunc()
    {
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

    private IEnumerator WaitBeforeGoing(float value)
    {
        yield return new WaitForSeconds(value);
        _enemy.Move(_isPlayerTurn, _usedSuid);
    }
}
