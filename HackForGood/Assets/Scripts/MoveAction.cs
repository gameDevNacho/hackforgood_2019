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
        assigned = false;

        initialHeight = GetComponent<RectTransform>().rect.height;
        initialWidth = GetComponent<RectTransform>().rect.width;
    }

    public override void OnBeginDrag(PointerEventData eventData)
    {
        image.raycastTarget = false;
        assigned = false;

        if(actionList)
        {
            actionList.RemoveFromList(this);
        }

        if(repeatActionList)
        {
            repeatActionList.RemoveFromList(this);
        }
    }

    public override void OnDrag(PointerEventData eventData)
    {
        #if UNITY_STANDALONE_WIN
                transform.position = Input.mousePosition;
        #elif UNITY_ANDROID
                    transform.position = Input.GetTouch(0).position;
        #endif

        GetComponent<RectTransform>().sizeDelta = new Vector2(initialWidth, initialHeight);
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        image.raycastTarget = true;

        if(!assigned)
        {
            Destroy(gameObject);
        }
    }

    public Directions GetDirection()
    {
        return direction;
    }
}
