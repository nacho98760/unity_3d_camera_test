using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IDropHandler
{

    public void OnDrop(PointerEventData eventData)
    {
        // If there are no items in the current slot, the item that's being dragged takes that place.
        if (transform.childCount == 0)
        {
            InventoryItem currentItemBeingDragged = eventData.pointerDrag.GetComponent<InventoryItem>();

            currentItemBeingDragged.parentAfterDrag = transform;
        }

        // If there is an item in the current slot, it switches positions with the item that's being dragged
        else
        {
            InventoryItem currentItemBeingDragged = eventData.pointerDrag.GetComponent<InventoryItem>();

            InventoryItem currentItemInSlot = transform.GetChild(0).gameObject.GetComponent<InventoryItem>();

            currentItemInSlot.transform.SetParent(currentItemBeingDragged.parentAfterDrag);
            currentItemBeingDragged.parentAfterDrag = transform;
        }
    }
}
