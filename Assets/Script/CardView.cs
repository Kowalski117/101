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
        SetSpriteShirt();
        _sprite = card.Sprite;
        _name = card.Name;
        _suit = card.Suit;
    }

    public void SetSpriteCard()
    {
        _image.sprite = _sprite;
    }

    public void SetSpriteShirt()
    {
        _image.sprite = _shirt;
    }
}
