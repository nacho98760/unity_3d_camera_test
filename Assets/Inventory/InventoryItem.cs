using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


[System.Serializable]
public class InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{

    public int amount;
    public Text amountText;
    public string itemName;
    public RawImage image; //Item texture
    public string itemType;

    [HideInInspector] public Transform parentAfterDrag;
    [HideInInspector] public Item item;


    public void InitializeItem(Item newItem, int initialAmount)
    {
        item = newItem;
        itemName = newItem.itemName;
        image.texture = newItem.image;
        amount = initialAmount;
        amountText.text = amount.ToString();

        if (item.stackable == false)
        {
            amountText.text = "";
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
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
        transform.SetParent(parentAfterDrag);
        image.raycastTarget = true;
    }
}
