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

    public Item[] starterItems;

    void Start()
    {
        InventoryUI.SetActive(false);
        isOpen = false;

        inventorySlotsOnToolbar = new InventorySlot[Toolbar.transform.childCount];

        for (int i = 0; i < Toolbar.transform.childCount; i++)
        {
            inventorySlotsOnToolbar[i] = Toolbar.transform.GetChild(i).GetComponent<InventorySlot>();
        }

        ChangeEquippedSlotColorAndResetThePrevious(0); // First slot on toolbar
        StartCoroutine(AddStarterItems(starterItems));
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


    public void AddItem(Item item)
    {
        print(inventorySlots.Length);
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

    bool CheckIfItemIsAlreadyOnInventory(Item item)
    {
        bool itemFound = false;

        foreach (InventorySlot slot in inventorySlots)
        {
            if (slot.HasAnInventoryItem())
            {
                if (slot.transform.GetChild(0).gameObject.GetComponent<InventoryItem>().itemName == "Axe")
                {
                    itemFound = true;
                    break;
                }
            }
        }

        return itemFound;
    }

    IEnumerator AddStarterItems(Item[] items)
    {
        yield return null;

        foreach (Item starterItem in items)
        {
            if (CheckIfItemIsAlreadyOnInventory(starterItem) == false)
            {
                AddItem(starterItem);
            }
        }
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
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ChangeEquippedSlotColorAndResetThePrevious(0);
        }

        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ChangeEquippedSlotColorAndResetThePrevious(1);
        }

        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ChangeEquippedSlotColorAndResetThePrevious(2);
        }

        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            ChangeEquippedSlotColorAndResetThePrevious(3);
        }

        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            ChangeEquippedSlotColorAndResetThePrevious(4);
        }

        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            ChangeEquippedSlotColorAndResetThePrevious(5);
        }

        else if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            ChangeEquippedSlotColorAndResetThePrevious(6);
        }
    }


    public bool CheckIfAxeIsEquipped()
    {
        bool axeFoundOnSlot = false;

        foreach (InventorySlot slot in inventorySlots)
        {
            if (slot.HasAnInventoryItem())
            {
                if (slot.transform.GetChild(0).gameObject.GetComponent<InventoryItem>().itemName == "Axe")
                {
                    if (currentlyEquippedSlot == slot)
                    {
                        axeFoundOnSlot = true;
                    }
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


