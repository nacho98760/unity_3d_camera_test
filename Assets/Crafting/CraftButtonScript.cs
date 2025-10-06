using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class CraftButtonScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Texture2D pointingHandMouseIcon;

    public void OnButtonClicked()
    {
        throw new NotImplementedException();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Cursor.SetCursor(pointingHandMouseIcon, Vector2.zero, CursorMode.Auto);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }
}