using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameOverPanel : MonoBehaviour
{
    [SerializeField] private PlayedDeck _playedDeck;
    [SerializeField] private GameObject _panel;
    [SerializeField] private Transform _handPlayer;
    [SerializeField] private Transform _handEnemy;
    [SerializeField] private Transform _containerPlayer;
    [SerializeField] private Transform _containerEnemy;

    [SerializeField] private TMP_Text _playerCardScore;
    [SerializeField] private TMP_Text _enemyCardScore;
    [SerializeField] private TMP_Text _playerTotalScore;
    [SerializeField] private TMP_Text _enemyTotalScore;
    [SerializeField] private TMP_Text _winner;

    [SerializeField] private Button _continue;
    [SerializeField] private Button _newGame;
    [SerializeField] private AudioSource _audioSource;

    static public UnityAction Continue;
    private Coroutine _coroutine;
    private bool _isPlayerWinnerParty;
    private bool _isPlayerWinner;
    private int _scorePlayer = 0;
    private int _scoreEnemy = 0;
    private int _maxScore = 101;

    private void OnEnable()
    {
        _playedDeck.EndTheGame += Open;
        _continue.onClick.AddListener(OnClickButtonContinue);
        _newGame.onClick.AddListener(OnClickButtonNewGame);
    }

    private void OnDisable()
    {
        _playedDeck.EndTheGame -= Open;
        _continue.onClick.RemoveListener(OnClickButtonContinue);
        _newGame.onClick.RemoveListener(OnClickButtonNewGame);
    }

    private void Open()
    {
        Time.timeScale = 1;
        _panel.SetActive(true);
        ChangeInteractableButtons(false);
        TransferCards();
        ViewWinnerParty();
    }

    private void Close()
    {
        _panel.SetActive(false);
    }

    private bool IsThereWinnerGame()
    {
        if (_scoreEnemy > _maxScore || _scorePlayer > _maxScore)
        {
            _isPlayerWinner = true;

            if (_scorePlayer > _maxScore)
                _isPlayerWinner = false;

            return true;
        }
        else
            return false;
    }

    private void TransferCards()
    {
        Transform[] cardsPlayer = _handPlayer.GetComponentsInChildren<Transform>();
        Transform[] cardsEnemy = _handEnemy.GetComponentsInChildren<Transform>();

        if (cardsEnemy.Length > cardsPlayer.Length)
        {
            _isPlayerWinnerParty = true;
            ShuffleCards(cardsEnemy, _containerEnemy);
        }
        else
        {
            _isPlayerWinnerParty = false;
            ShuffleCards(cardsPlayer, _containerPlayer);
        }
    }

    private void CountTheScore()
    {
        if (_isPlayerWinnerParty)
            _scoreEnemy += GetCardPoint(GetLastCard(_containerEnemy));
        else
            _scorePlayer += GetCardPoint(GetLastCard(_containerPlayer));
        ViewScore();
    }

    private void ViewScore()
    {
        if (_isPlayerWinnerParty)
            _enemyCardScore.text = GetCardPoint(GetLastCard(_containerEnemy)).ToString();

        else
            _playerCardScore.text = GetCardPoint(GetLastCard(_containerPlayer)).ToString();

        _enemyTotalScore.text = _scoreEnemy.ToString();
        _playerTotalScore.text = _scorePlayer.ToString();
    }

    private CardView GetLastCard(Transform container)
    {
        if (container.childCount >= 1)
            return container.GetChild(container.childCount - 1).GetComponent<CardView>();
        else
            return null;
    }

    private void ViewWinnerParty()
    {
        string text = "Победитель партии";
        _winner.text = text;

        if (_isPlayerWinnerParty)
        {
            _winner.transform.position = _containerPlayer.transform.position;
        }
        else
        {
            _winner.transform.position = _containerEnemy.transform.position;
        }
    }

    private void ViewWinnerGame()
    {
        string text = "Победитель игры";
        _winner.text = text;

        if (_isPlayerWinner)
        {
            _winner.transform.position = _containerPlayer.transform.position;
        }
        else
        {
            _winner.transform.position = _containerEnemy.transform.position;
        }
    }

    private void OnClickButtonContinue()
    {
        Continue?.Invoke();
        ResetText();
        Close();
    }

    private void OnClickButtonNewGame()
    {
        _scoreEnemy = 0;
        _scorePlayer = 0;
        ResetText();
        Close();
    }

    private void ResetText()
    {
        _playerCardScore.text = null;
        _enemyCardScore.text = null;
        _playerTotalScore.text = null;
        _enemyTotalScore.text = null;
    }

    private void ChangeInteractableButtons(bool isInteractable)
    {
        _newGame.interactable = isInteractable;

        if (IsThereWinnerGame() == false)
            _continue.interactable = isInteractable;
    }


    private void ShuffleCards(Transform[] transforms, Transform container)
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        if (transforms.Length > 1)
        {
            _coroutine = StartCoroutine(WaitTime(transforms, container));
        }
        else
        {
            if (IsThereWinnerGame())
                ViewWinnerGame();

            ChangeInteractableButtons(true);
        }
    }

    private void MoveCard(Transform[] transforms, Transform container)
    {
        Transform card;

        _audioSource.Play();
        card = transforms[transforms.Length - 1];
        card.SetParent(container);
        card.GetComponent<CardView>().SetSpriteCard();
        CountTheScore();

        if (_isPlayerWinnerParty)
        {
            transforms = _handEnemy.GetComponentsInChildren<Transform>();
        }
        else
        {
            transforms = _handPlayer.GetComponentsInChildren<Transform>();
        }

        ShuffleCards(transforms,container);
    }

    private IEnumerator WaitTime(Transform[] transforms, Transform container)
    {
        float expectation = 1.0f;
        yield return new WaitForSeconds(expectation);
        MoveCard(transforms, container);
    }

    private int GetCardPoint(CardView card)
    {
        switch (card.Name)
        {
            case NameCard.Six:
                return 6;

            case NameCard.Seven:
                return 7;

            case NameCard.Eigth:
                return 8;

            case NameCard.Nine:
                return 0;

            case NameCard.Ten:
                return 10;

            case NameCard.Jack:
                return 2;

            case NameCard.Queen:
                return 3;

            case NameCard.King:
                return 4;

            case NameCard.Ace:
                return 11;
        }
        return 0;
    }
}
