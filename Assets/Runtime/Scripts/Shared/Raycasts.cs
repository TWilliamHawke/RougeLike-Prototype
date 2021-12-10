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
        var hits = new List<RaycastResult>();
        var eventData = new PointerEventData(EventSystem.current);
        eventData.position = Mouse.current.position.ReadValue();
        EventSystem.current.RaycastAll(eventData, hits);
        return hits;
    }
}
