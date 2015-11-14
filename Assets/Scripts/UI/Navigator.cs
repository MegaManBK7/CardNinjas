using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Assets.Scripts.Util;

public static class Navigator
{
    public static void Navigate(CustomInput.UserInput direction, GameObject defaultGameObject)
    {
        GameObject next = EventSystem.current.currentSelectedGameObject;
        if (next == null)
        {
            if(defaultGameObject != null) EventSystem.current.SetSelectedGameObject(defaultGameObject);
            return;
        }

        bool nextIsValid = false;
        while (!nextIsValid)
        {
            switch (direction)
            {
                case CustomInput.UserInput.Up:
                    next = EventSystem.current.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnUp().gameObject;
                    break;
                case CustomInput.UserInput.Down:
                    next = EventSystem.current.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnDown().gameObject;
                    break;
                case CustomInput.UserInput.Left:
                    next = EventSystem.current.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnLeft().gameObject;
                    break;
                case CustomInput.UserInput.Right:
                    next = EventSystem.current.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnRight().gameObject;
                    break;
            }
            EventSystem.current.SetSelectedGameObject(next);
            nextIsValid = next.GetComponent<Selectable>().interactable;
        }
    }

    public static void CallSubmit()
    {
        var pointer = new PointerEventData(EventSystem.current);
        ExecuteEvents.Execute(EventSystem.current.currentSelectedGameObject, pointer, ExecuteEvents.submitHandler);
    }
}