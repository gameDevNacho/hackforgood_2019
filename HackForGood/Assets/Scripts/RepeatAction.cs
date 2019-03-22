using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RepeatAction : Action
{
    [SerializeField]
    private List<MoveAction> moveActions;

    private Image image;

    private int index;

    private void Awake()
    {
        index = 0;
        image = GetComponent<Image>();
        assigned = false;
        moveActions = new List<MoveAction>();
    }

    public override void OnBeginDrag(PointerEventData eventData)
    {
        image.raycastTarget = false;
        Image[] images = GetComponentsInChildren<Image>();

        for (int i = 0; i < images.Length; i++)
        {
            images[i].raycastTarget = false;
        }

        Text[] texts = GetComponentsInChildren<Text>();

        for (int i = 0; i < texts.Length; i++)
        {
            texts[i].raycastTarget = false;
        }

        Button[] buttons = GetComponentsInChildren<Button>();

        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = false;
        }

        assigned = false;

        if (actionList)
        {
            actionList.RemoveFromList(this);
        }
    }

    public override void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        image.raycastTarget = true;

        Image[] images = GetComponentsInChildren<Image>();

        for (int i = 0; i < images.Length; i++)
        {
            images[i].raycastTarget = true;
        }

        Text[] texts = GetComponentsInChildren<Text>();

        for (int i = 0; i < texts.Length; i++)
        {
            texts[i].raycastTarget = true;
        }

        Button[] buttons = GetComponentsInChildren<Button>();

        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = true;
        }

        if (!assigned)
        {
            for (int i = 0; i < moveActions.Count; i++)
            {
                Destroy(moveActions[i].gameObject);
            }

            Destroy(gameObject);
        }
    }

    public List<MoveAction> GetMoveActions()
    {
        return moveActions;
    }

    public void RemoveFromList(MoveAction action)
    {
        moveActions.Remove(action);
        action.transform.SetParent(GameObject.Find("Canvas").transform);
    }
}
