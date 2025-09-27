using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CloseMenuScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject craftingCanvas;
    public GameObject transparencyBackground;
    public Texture2D pointingHandMouseIcon;

    public void OnButtonClicked()
    {
        craftingCanvas.SetActive(false);
        transparencyBackground.GetComponent<Image>().color = new Color(0f, 0f, 0f, 0f);

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
