using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ActionInstantiator : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField]
    private Action actionToInstantiate;
    [SerializeField]
    private Transform canvasTransform;

    private Action actionInstantiated;

    private void Awake()
    {
        actionInstantiated = null;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        actionInstantiated = Instantiate(actionToInstantiate, canvasTransform);
        actionInstantiated.transform.position = Input.mousePosition;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        actionInstantiated = null;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if(actionInstantiated)
        {
            actionInstantiated.OnBeginDrag(eventData);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if(actionInstantiated)
        {
            actionInstantiated.OnDrag(eventData);
            eventData.pointerDrag = actionInstantiated.gameObject;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (actionInstantiated)
        {
            actionInstantiated.OnEndDrag(eventData);
        }

        actionInstantiated = null;
    }
}
