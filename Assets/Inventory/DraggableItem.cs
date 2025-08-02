using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public RawImage image; //Item texture
    public string ObjectType;
    public Transform parentAfterDrag;

    public void OnBeginDrag(PointerEventData eventData)
    {
        print("Begin Drag");
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        image.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        print("End Drag");
        transform.SetParent(parentAfterDrag);
        image.raycastTarget = true;
    }
}
