using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MoveAction : Action
{
    [SerializeField]
    private Directions direction;

    private Image image;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    public override void OnBeginDrag(PointerEventData eventData)
    {
        image.raycastTarget = false;
    }

    public override void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        image.raycastTarget = true;
    }

    public Directions GetDirection()
    {
        return direction;
    }
}
