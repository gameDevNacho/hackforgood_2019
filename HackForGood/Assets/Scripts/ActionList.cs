using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ActionList : MonoBehaviour, IPointerEnterHandler, IDropHandler, IPointerExitHandler
{
    [SerializeField]
    private List<Action> actionList;

    private bool arranging;

    private GameObject emptyGameObject;

    [SerializeField]
    private Transform canvasTransform;

    private void Awake()
    {
        actionList = new List<Action>();
        arranging = false;
        emptyGameObject = null;
    }

    private void Update()
    {
        if(arranging && transform.childCount > 0)
        {
            if(transform.GetChild(0).transform.position.y < Input.mousePosition.y)
            {
                if (emptyGameObject)
                {
                    emptyGameObject.transform.SetSiblingIndex(0);
                }
            }

            else if(transform.GetChild(transform.childCount - 1).transform.position.y > Input.mousePosition.y)
            {
                if (emptyGameObject)
                {
                    emptyGameObject.transform.SetSiblingIndex(transform.childCount - 1);
                }
            }

            else
            {
                for (int i = 0; i < transform.childCount; i++)
                {
                    if (emptyGameObject && i + 1 < actionList.Count)
                    {
                        if (transform.GetChild(i).transform.position.y >= Input.mousePosition.y && transform.GetChild(i + 1).transform.position.y < Input.mousePosition.y)
                        {
                            emptyGameObject.transform.SetSiblingIndex(i + 1);
                        }
                    }
                }
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(eventData.dragging && eventData.pointerDrag.GetComponent<Action>() && !emptyGameObject)
        {
            arranging = true;
            emptyGameObject = new GameObject();
            emptyGameObject.transform.parent = transform;
            emptyGameObject.AddComponent<Image>();
            emptyGameObject.GetComponent<Image>().color = new Color(emptyGameObject.GetComponent<Image>().color.r, emptyGameObject.GetComponent<Image>().color.b, emptyGameObject.GetComponent<Image>().color.g, 0);
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        if(eventData.pointerDrag.GetComponent<Action>())
        {
            eventData.pointerDrag.transform.SetParent(transform);

            if (emptyGameObject)
            {
                eventData.pointerDrag.transform.SetSiblingIndex(emptyGameObject.transform.GetSiblingIndex());
                Destroy(emptyGameObject);
                actionList.Insert(eventData.pointerDrag.transform.GetSiblingIndex(), eventData.pointerDrag.GetComponent<Action>());
                eventData.pointerDrag.GetComponent<Action>().assigned = true;
                eventData.pointerDrag.GetComponent<Action>().actionList = this;
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(eventData.dragging)
        {
            arranging = false;
        }

        if(emptyGameObject)
        {
            Destroy(emptyGameObject);
        }
    }

    public void RemoveFromList(Action action)
    {
        actionList.Remove(action);
        action.transform.parent = canvasTransform;
    }

    public List<MoveAction> GetMoveActions()
    {
        List<MoveAction> moveActions = new List<MoveAction>();

        for (int i = 0; i < actionList.Count; i++)
        {
            if(actionList[i].GetComponent<MoveAction>())
            {
                moveActions.Add(actionList[i].GetComponent<MoveAction>());
            }

            else if(actionList[i].GetComponent<RepeatAction>())
            {
                RepeatAction repeatAction = actionList[i].GetComponent<RepeatAction>();

                for (int j = 0; j < repeatAction.GetMoveActions().Count; j++)
                {
                    moveActions.Add(repeatAction.GetMoveActions()[j]);
                }
            }
        }

        return moveActions;
    }
}
