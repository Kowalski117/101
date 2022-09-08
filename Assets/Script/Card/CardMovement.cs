using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardMovement : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Transform DefaultParent;

    private Camera _camera;
    private Vector2 _offset;

    private void Awake()
    {
        _camera = Camera.allCameras[0];
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _offset = transform.position - _camera.ScreenToWorldPoint(eventData.position);
        DefaultParent = transform.parent;
        transform.SetParent(DefaultParent.parent);
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 newPosition = _camera.ScreenToWorldPoint(eventData.position);
        transform.position = newPosition + _offset;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(DefaultParent);
        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }
}