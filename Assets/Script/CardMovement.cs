using UnityEngine;
using UnityEngine.EventSystems;

public class CardMovement : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Transform _defaultParent;
    private Camera _camera;
    private Vector2 _offset;
    private bool _isDragging;

    private void Awake()
    {
        _camera = Camera.allCameras[0];
    }

    public void SetDefultParent(Transform transform)
    {
        _defaultParent = transform;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _offset = transform.position - _camera.ScreenToWorldPoint(eventData.position);
        _defaultParent = transform.parent;

        _isDragging = _defaultParent.TryGetComponent<Player>(out Player Player);

        if (!_isDragging)
            return;

        transform.SetParent(_defaultParent.parent);
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

        transform.SetParent(_defaultParent);
        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }
}