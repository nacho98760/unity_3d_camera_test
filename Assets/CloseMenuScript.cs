using UnityEngine;
using UnityEngine.EventSystems;

public class CloseMenuScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Texture2D pointingHandMouseIcon;

    public void OnButtonClicked()
    {
        GameObject craftingCanvasObject = gameObject.transform.root.gameObject;

        craftingCanvasObject.SetActive(false);
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
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
