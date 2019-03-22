using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ActionList : MonoBehaviour, IPointerEnterHandler, IDropHandler, IPointerExitHandler
{
    private List<Action> actionList;

    private bool arranging;

    private GameObject emptyGameObject;

    private void Awake()
    {
        actionList = new List<Action>();
        arranging = false;
    }

    private void Update()
    {
        if(arranging)
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

            for (int i = 0; i < transform.childCount; i++)
            {
                if(emptyGameObject && i + 1 < actionList.Count)
                {
                    if(transform.GetChild(i).transform.position.y >= Input.mousePosition.y && transform.GetChild(i + 1).transform.position.y < Input.mousePosition.y)
                    {
                        emptyGameObject.transform.SetSiblingIndex(i + 1);
                    }
                }
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(eventData.dragging)
        {
            arranging = true;
            emptyGameObject = Instantiate(new GameObject(), transform);
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
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(eventData.dragging)
        {
            arranging = false;
            Destroy(emptyGameObject);
        }
    }
}
