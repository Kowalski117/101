using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private Transform _deck;
    [SerializeField] private Transform[] _decks;
    [SerializeField] private PlayedDeck _playedDeck;
    [SerializeField] private Menu _menu;
    [SerializeField] private ButtonNewGame _newGame;

    private void OnEnable()
    {
        _newGame.NewGame += NewGame;
        GameOverPanel.Continue += NewGame;
    }

    private void OnDisable()
    {
        _newGame.NewGame -= NewGame;
        GameOverPanel.Continue -= NewGame;
    }

    private void Start()
    {
        _menu.Open();
    }

    private void NewGame()
    {
        CollectCards();
        _playedDeck.StartGame();
    }

    private void CollectCards()
    {
        Transform card;
        for (int i = 0; i < _decks.Length; i++)
        {
            Transform[] cards = _decks[i].GetComponentsInChildren<Transform>();

            for (int j = 1; j < cards.Length; j++)
            {
                card = cards[j];
                card.SetParent(_deck);
                card.GetComponent<CardView>().SetSpriteShirt();
            }
        }
    }
}
