using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class View : MonoBehaviour
{
    [SerializeField] private PlayedDeck _playedDeck;
    [SerializeField] private Button _buttonDeck;
    [SerializeField] private TMP_Text _text;
    [SerializeField] private Image _suit;
    [SerializeField] private Slider _sliderPlayer;
    [SerializeField] private Slider _sliderEnemy;

    private CanvasGroup _canvasGroupPlayer;
    private CanvasGroup _canvasGroupEnemy;

    private void OnEnable()
    {
        PlayedDeck.MoveChange += ChangeGameDeckButtonActivity;
        PlayedDeck.SuidChanged += ShowSuit;
        PlayedDeck.ChangedTurn += ChamgeTurn;
        GameDeck.ButtonPressed += ChangeGameDeckButtonActivity;
    }

    private void OnDisable()
    {
        PlayedDeck.MoveChange -= ChangeGameDeckButtonActivity;
        PlayedDeck.SuidChanged -= ShowSuit;
        PlayedDeck.ChangedTurn -= ChamgeTurn;
        GameDeck.ButtonPressed -= ChangeGameDeckButtonActivity;
    }

    private void Start()
    {
        _canvasGroupPlayer = _sliderPlayer.GetComponent<CanvasGroup>();
        _canvasGroupEnemy = _sliderEnemy.GetComponent<CanvasGroup>();
        _sliderPlayer.maxValue = _playedDeck.MaxTurnTime;
        _sliderEnemy.maxValue = _playedDeck.MaxTurnTime;
        _canvasGroupPlayer.alpha = 0;
        _canvasGroupEnemy.alpha = 0;
    }

    private void Update()
    {
        _text.text = _playedDeck.TurnTime.ToString();
        _sliderPlayer.value = _playedDeck.TurnTime;
        _sliderEnemy.value = _playedDeck.TurnTime;
    }

    private void ChangeGameDeckButtonActivity(bool activity)
    {
        _buttonDeck.enabled = activity;
    }

    private void ShowSuit(Suit suit)
    {
        _suit.sprite = Resources.Load<Sprite>("Sprite/Suit/" + suit.ToString());
    }

    private void ChamgeTurn()
    {
        if (_playedDeck.IsPlayerTurn)
        {
            _canvasGroupPlayer.alpha = 1;
            _canvasGroupEnemy.alpha = 0;
            _sliderPlayer.value = _playedDeck.TurnTime;
        }
        else
        {
            _canvasGroupPlayer.alpha = 0;
            _canvasGroupEnemy.alpha = 1;
            _sliderEnemy.value = _playedDeck.TurnTime;
        }
    }
}
