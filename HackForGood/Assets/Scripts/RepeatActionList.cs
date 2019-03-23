using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class RepeatActionList : MonoBehaviour, IPointerEnterHandler, IDropHandler, IPointerExitHandler
{
    [SerializeField]
    private RepeatAction repeatAction;

    private Image image;

    private bool arranging;

    private GameObject emptyGameObject;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(eventData.dragging && eventData.pointerDrag.GetComponent<MoveAction>() && !emptyGameObject && repeatAction.GetMoveActions().Count < repeatAction.maxMoveActions)
        {
            arranging = true;
            emptyGameObject = new GameObject();
            emptyGameObject.transform.parent = transform;
            emptyGameObject.AddComponent<Image>();
            emptyGameObject.GetComponent<Image>().color = new Color(emptyGameObject.GetComponent<Image>().color.r, emptyGameObject.GetComponent<Image>().color.b, emptyGameObject.GetComponent<Image>().color.g, 0);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (eventData.dragging)
        {
            arranging = false;
        }

        if (emptyGameObject)
        {
            Destroy(emptyGameObject);
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag.GetComponent<MoveAction>())
        {
            eventData.pointerDrag.transform.SetParent(transform);

            if (emptyGameObject)
            {

                if (repeatAction.GetMoveActions().Count < repeatAction.maxMoveActions)
                {
                    eventData.pointerDrag.transform.SetSiblingIndex(emptyGameObject.transform.GetSiblingIndex());
                    repeatAction.GetMoveActions().Insert(eventData.pointerDrag.transform.GetSiblingIndex(), eventData.pointerDrag.GetComponent<MoveAction>());
                    eventData.pointerDrag.GetComponent<MoveAction>().assigned = true;
                    eventData.pointerDrag.GetComponent<MoveAction>().repeatActionList = this;
                }

                else
                {
                    repeatAction.Error();
                }

                Destroy(emptyGameObject);
            }
        }
    }

    private void Awake()
    {
        image = GetComponent<Image>();
        arranging = false;
    }

    private void Update()
    {
        if(arranging && transform.childCount > 0)
        {
            if(transform.GetChild(0).transform.position.y < Input.mousePosition.y)
            {
                if(emptyGameObject)
                {
                    emptyGameObject.transform.SetSiblingIndex(0);
                }
            }

            else if(transform.GetChild(transform.childCount - 1).transform.position.y > Input.mousePosition.y)
            {
                if(emptyGameObject)
                {
                    emptyGameObject.transform.SetSiblingIndex(transform.childCount - 1);
                }
            }

            else
            {
                for (int i = 0; i < transform.childCount; i++)
                {
                    if (emptyGameObject && i + 1 < repeatAction.GetMoveActions().Count)
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

    public void RemoveFromList(MoveAction action)
    {
        repeatAction.RemoveFromList(action);
    }
}
