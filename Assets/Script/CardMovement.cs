using UnityEngine;
using UnityEngine.EventSystems;

public class CardMovement : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Transform DefaultParent;
    private Camera _camera;
    private Vector2 _offset;
    private bool _isDragging;

    private void Awake()
    {
        _camera = Camera.allCameras[0];
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _offset = transform.position - _camera.ScreenToWorldPoint(eventData.position);
        DefaultParent = transform.parent;

        _isDragging = DefaultParent.TryGetComponent<Player>(out Player handPlayer1);

        if (!_isDragging)
            return;

        transform.SetParent(DefaultParent.parent);
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!_isDragging)
            return;

        Vector2 newPosition = _camera.ScreenToWorldPoint(eventData.position);
        transform.position = newPosition + _offset;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!_isDragging)
            return;

        transform.SetParent(DefaultParent);
        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }
}