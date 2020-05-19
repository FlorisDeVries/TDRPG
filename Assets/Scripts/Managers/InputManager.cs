using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class InputManager : UnitySingleton<InputManager>, IPointerClickHandler
{
    public Dictionary<KeyCode, Vector3Event> KeyEvents = new Dictionary<KeyCode, Vector3Event>();

    protected override void Awake()
    {
        base.Awake();
        foreach (KeyCode type in Enum.GetValues(typeof(KeyCode)))
        {
            if (!KeyEvents.ContainsKey(type))
                KeyEvents.Add(type, new Vector3Event());
        }
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        if (pointerEventData.button == PointerEventData.InputButton.Left)
            KeyEvents[KeyCode.Mouse0].Invoke(pointerEventData.pointerCurrentRaycast.worldPosition);
    }
}
