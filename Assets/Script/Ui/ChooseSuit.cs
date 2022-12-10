using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class ChooseSuit : MonoBehaviour
{
    [SerializeField] private Button _diamonds;
    [SerializeField] private Button _clubs;
    [SerializeField] private Button _hearts;
    [SerializeField] private Button _spades;
    [SerializeField] private PlayedDeck _playedDeck;

    private CanvasGroup _canvasGroup;

    private void Start()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    private void OnEnable()
    {
        Ability.SuitChanged += ShowChangeSuitScreen;
    }

    private void OnDisable()
    {
        Ability.SuitChanged -= ShowChangeSuitScreen;
    }

    public void OnClickDiamonds()
    {
        _playedDeck.ChangeSuit(Suit.Diamonds);
        HideChangeSuitScreen();
    }

    public void OnClickClubs()
    {
        _playedDeck.ChangeSuit(Suit.Clubs);
        HideChangeSuitScreen();
    }

    public void OnClickHearts()
    {
        _playedDeck.ChangeSuit(Suit.Hearts);
        HideChangeSuitScreen();
    }

    public void OnClickSpades()
    {
        _playedDeck.ChangeSuit(Suit.Spades);
        HideChangeSuitScreen();
    }

    private void ShowChangeSuitScreen()
    {
        Time.timeScale = 0;
        _canvasGroup.alpha = 1;
        _canvasGroup.blocksRaycasts = true;
    }

    private void HideChangeSuitScreen()
    {
        _canvasGroup.alpha = 0;
        _canvasGroup.blocksRaycasts = false;
        Time.timeScale = 1;
    }
}
