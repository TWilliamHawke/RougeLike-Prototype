using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

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

    static public bool IsRaycastLocationValid(Vector2 sp, Image image)
    {
        var _sprite = image.sprite;
        var transform = image.rectTransform;

        var textureRect = _sprite.textureRect;
        var spriteRect = transform.rect;
        Vector2 pivot = transform.pivot;

        var textureWidth = textureRect.width + textureRect.x;
        var textureHeight = textureRect.height + textureRect.y;

        //from screenpos to rectpos
        float rectX = sp.x - transform.position.x + spriteRect.width * pivot.x;
        float rectY = sp.y - transform.position.y + spriteRect.height * pivot.y;

        //lerp from rectPos to texturePos
        int x = Mathf.FloorToInt(textureWidth * rectX / spriteRect.width);
        int y = Mathf.FloorToInt(textureHeight * rectY / spriteRect.height);

        //flip position if scale < 0
        y = Mathf.FloorToInt(textureHeight / 2 + Mathf.Sign(transform.localScale.y) * (y - textureHeight / 2));
        x = Mathf.FloorToInt(textureWidth / 2 + Mathf.Sign(transform.localScale.x) * (x - textureWidth / 2));

        try
        {
            return _sprite.texture.GetPixel(x, y).a > 0;
        }
        catch (UnityException err)
        {
            Debug.Log(err.Message);
            return false;
        }
    }
}
