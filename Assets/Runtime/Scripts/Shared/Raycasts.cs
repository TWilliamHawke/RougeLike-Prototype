using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public static class Raycasts
{
    //raycast for ui elements
    static public List<RaycastResult> UI()
    {
        return UI(Mouse.current.position.ReadValue());
    }

    static public List<RaycastResult> UI(Vector2 position)
    {
        var hits = new List<RaycastResult>();
        var eventData = new PointerEventData(EventSystem.current);
        eventData.position = position;
        EventSystem.current.RaycastAll(eventData, hits);
        return hits;
    }
}
