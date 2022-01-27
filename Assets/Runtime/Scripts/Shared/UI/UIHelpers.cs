using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public static class UIHelpers
{
    static public Vector3 NormalizePosition(RectTransform rectTransform)
    {
        Vector2 menuSize = rectTransform.sizeDelta;
        var mousePos = Mouse.current.position.ReadValue().AddZ(0);

        float offsetX = mousePos.x + menuSize.x > Screen.width ? -menuSize.x / 2 : menuSize.x / 2;
        float offsetY = mousePos.y + menuSize.y > Screen.height ? -menuSize.y / 2 : menuSize.y / 2;

        return mousePos + new Vector3(offsetX, offsetY, 0);

    }
}
