using UnityEngine;
using UnityEngine.UI;

public class CardView : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private Sprite _sprite;
    [SerializeField] private Sprite _shirt;

    private NameCard _name;
    private Suit _suit;

    public NameCard Name => _name;
    public Suit Suit => _suit;

    public void Render(Card card)
    {
        ShowShirt();
        _sprite = card.Sprite;
        _name = card.Name;
        _suit = card.Suit;
    }

    public void ShowSprite()
    {
        _image.sprite = _sprite;
    }

    public void ShowShirt()
    {
        _image.sprite = _shirt;
    }
}
