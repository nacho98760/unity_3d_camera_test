using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        // If there are no items in the current slot, the item that's being dragged takes that place.
        if (transform.childCount == 0)
        {
            DraggableItem currentItemBeingDragged = eventData.pointerDrag.GetComponent<DraggableItem>();

            currentItemBeingDragged.parentAfterDrag = transform;
        }

        // If there is an item in the current slot, it switches positions with the item that's being dragged
        else
        {
            DraggableItem currentItemBeingDragged = eventData.pointerDrag.GetComponent<DraggableItem>();

            DraggableItem currentItemInSlot = transform.GetChild(0).gameObject.GetComponent<DraggableItem>();

            currentItemInSlot.transform.SetParent(currentItemBeingDragged.parentAfterDrag);
            currentItemBeingDragged.parentAfterDrag = transform;
        }
    }
}
