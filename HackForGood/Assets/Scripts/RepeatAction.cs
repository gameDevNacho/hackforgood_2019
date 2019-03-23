using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RepeatAction : Action
{
    [SerializeField]
    private List<MoveAction> moveActions;
    [SerializeField]
    private Text repeatingText;
    [SerializeField]
    private Image icon;

    private Image image;

    public int maxMoveActions;

    private int index;

    public int TimesRepeating
    {
        get;

        set;
    }

    private void Awake()
    {
        index = 0;
        image = GetComponent<Image>();
        assigned = false;
        moveActions = new List<MoveAction>();
        TimesRepeating = 1;
        maxMoveActions = 2;
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
        #if UNITY_STANDALONE_WIN
            transform.position = Input.mousePosition;
        #elif UNITY_ANDROID
            transform.position = Input.GetTouch(0).position;
        #endif
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        image.raycastTarget = true;

        Image[] images = GetComponentsInChildren<Image>();

        for (int i = 0; i < images.Length; i++)
        {
            images[i].raycastTarget = true;
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

    public void AddRepetition(bool add)
    {
        if(add)
        {
            TimesRepeating++;
        }

        else
        {
            if(TimesRepeating - 1 >= 1)
            {
                TimesRepeating--;
            }
        }

        repeatingText.text = TimesRepeating.ToString();
    }

    public void Error()
    {
        StopCoroutine(ErrorColor());
        StartCoroutine(ErrorColor());
    }

    private IEnumerator ErrorColor()
    {
        Color color = icon.color;
        icon.color = new Color(1, 0, 0, 1);
        yield return new WaitForSeconds(0.5f);
        icon.color = color;

    }
}
