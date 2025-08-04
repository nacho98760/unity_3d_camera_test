using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class InventoryUIScript : MonoBehaviour
{
    public InventorySlot[] inventorySlots;
    public GameObject inventoryItemPrefab;
    public bool ItemAlreadyPlaced = false;

    [SerializeField] public GameObject InventoryUI;
    public bool isOpen;

    void Start()
    {
        InventoryUI.SetActive(false);
        isOpen = false;
    }


    private void Update()
    {
        checkInvState();
    }


    private void checkInvState()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (isOpen)
            {
                InventoryUI.SetActive(false);
                isOpen = false;
            }
            else
            {
                InventoryUI.SetActive(true);
                isOpen = true;
            }
        }
    }


    public void AddItem(Item item)
    {
        // We check if there's already an instance of that item placed on any slot
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];

            if (slot.transform.childCount > 0)
            {
                GameObject itemInSlot = slot.transform.GetChild(0).gameObject;
                InventoryItem inventoryItem = itemInSlot.GetComponent<InventoryItem>();

                if (inventoryItem.itemName == item.itemName)
                {
                    inventoryItem.amount += 1;
                    inventoryItem.amountText.text = inventoryItem.amount.ToString();
                    ItemAlreadyPlaced = true;
                    return;
                }
            }
        }

        // If there's no instance of that item in any slot, we find an empty slot to place the item
        if (ItemAlreadyPlaced == false)
        {
            for (int i = 0; i < inventorySlots.Length; i++)
            {
                InventorySlot slot = inventorySlots[i];

                InventoryItem itemInSlot = slot.GetComponent<InventoryItem>();

                if (slot.transform.childCount == 0)
                {
                    SpawnNewItem(item, slot);
                    return;
                }
            }
        }
    }

    void SpawnNewItem(Item item, InventorySlot slot)
    {
        GameObject newItemGameObject = Instantiate(inventoryItemPrefab, slot.transform);
        InventoryItem inventoryItem = newItemGameObject.GetComponent<InventoryItem>();
        inventoryItem.InitializeItem(item);
    }
}
