using UnityEngine;
using UnityEngine.UI;

public class CardView : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private Sprite _sprite;
    [SerializeField] private Sprite _shirt;
    public NameCard Name;
    public Suit Suit;

    public void Render(Card card)
    {
        ShowShirt();
        _sprite = card.Sprite;
        Name = card.Name;
        Suit = card.Suit;
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
