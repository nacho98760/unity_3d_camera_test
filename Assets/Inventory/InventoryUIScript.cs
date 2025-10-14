using System;
using System.Collections;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using UnityEngine.UI;


[System.Serializable]
public class InventoryUIScript : MonoBehaviour
{

    public SavingSystem savingSystem;

    private Color defaultColor = new Color(1f, 0.949664f, 0.8066038f);
    private Color slotWithEquippedItemColor = new Color(1f, 0.86f, 0.47f);

    public InventorySlot[] inventorySlots;
    public GameObject inventoryItemPrefab;
    public bool ItemAlreadyPlaced = false;

    public AxeAnimationHandler Axe;

    public GameObject Toolbar;
    public InventorySlot[] inventorySlotsOnToolbar;
    public InventorySlot currentlyEquippedSlot;

    public GameObject InventoryUI;
    public bool isOpen;

    void Start()
    {
        InventoryUI.SetActive(false);
        isOpen = false;

        inventorySlotsOnToolbar = new InventorySlot[Toolbar.transform.childCount];

        for (int i = 0; i < Toolbar.transform.childCount; i++)
        {
            inventorySlotsOnToolbar[i] = Toolbar.transform.GetChild(i).GetComponent<InventorySlot>();
        }
    }

    private void Update()
    {
        EquipSlotBasedOnKeyPressed();
        CheckInvState();
    }

    private void CheckInvState()
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


    public void AddItem(Item item, int itemAmount)
    {
        // We check if there's already an instance of that item placed on any slot
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];

            if (slot.transform.childCount > 0)
            {
                InventoryItem inventoryItem = slot.gameObject.transform.GetComponentInChildren<InventoryItem>();

                if (inventoryItem.itemName == item.itemName)
                {
                    inventoryItem.amount += itemAmount;
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

                if (slot.transform.childCount == 0)
                {
                    SpawnNewItem(item, slot, item.amountToAddOnInv);
                    return;
                }
            }
        }
    }

    public void SubstractItemAmount(Item item, int itemAmountToRemove)
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];

            if (!slot.HasAnInventoryItem())
                continue;

            InventoryItem itemInSlot = slot.gameObject.transform.GetComponentInChildren<InventoryItem>();

            if (itemInSlot.itemName != item.itemName)
                continue;
            
            itemInSlot.amount -= itemAmountToRemove;
            itemInSlot.amountText.text = (itemInSlot.amount).ToString();

            if (itemInSlot.amount <= 0)
            {
                Destroy(itemInSlot.gameObject);
            }
        }
    }


    public void SpawnNewItem(Item item, InventorySlot slot, int initialAmount)
    {
        GameObject newItemGameObject = Instantiate(inventoryItemPrefab, slot.transform);
        InventoryItem inventoryItem = newItemGameObject.GetComponent<InventoryItem>();
        inventoryItem.InitializeItem(item, initialAmount);
    }

    public void ChangeEquippedSlotColorAndResetThePrevious(int slotPosition)
    {
        if (currentlyEquippedSlot != null)
        {
            currentlyEquippedSlot.GetComponent<Image>().color = defaultColor;
        }

        currentlyEquippedSlot = inventorySlotsOnToolbar[slotPosition];
        currentlyEquippedSlot.GetComponent<Image>().color = slotWithEquippedItemColor;
        Axe.transform.gameObject.SetActive(CheckIfAxeIsEquipped());

        CheckAxeAnimationState(CheckIfAxeIsEquipped());
    }

    public void EquipSlotBasedOnKeyPressed()
    {
        KeyCode[] keysToEquipitems = new KeyCode[] { KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3, KeyCode.Alpha4, KeyCode.Alpha5, KeyCode.Alpha6, KeyCode.Alpha7 };

        for (int i = 0; i < keysToEquipitems.Length; i++)
        {
            if (Input.GetKeyDown(keysToEquipitems[i]))
            {
                ChangeEquippedSlotColorAndResetThePrevious(i);
            }
        }
    }


    public bool CheckIfAxeIsEquipped()
    {
        bool axeFoundOnSlot = false;

        foreach (InventorySlot slot in inventorySlots)
        {
            if (!slot.HasAnInventoryItem())
                continue;

            if (slot.transform.GetChild(0).gameObject.GetComponent<InventoryItem>().itemName == "Axe")
            {
                if (currentlyEquippedSlot == slot)
                {
                    axeFoundOnSlot = true;
                    break;
                }
            }
        }

        return axeFoundOnSlot;
    }

    public void CheckAxeAnimationState(bool axeWasFound)
    {
        if (axeWasFound)
        {
            Axe.ChangeAnimation("Idle");
        }
    }

}