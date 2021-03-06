﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class Action : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public bool assigned;

    public ActionList actionList;

    public RepeatActionList repeatActionList;

    public float initialWidth;
    public float initialHeight;

    public abstract void OnBeginDrag(PointerEventData eventData);

    public abstract void OnDrag(PointerEventData eventData);

    public abstract void OnEndDrag(PointerEventData eventData);
}
