using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class CraftButtonScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] InventoryUIScript inventoryUIScript;
    [SerializeField] private GameObject craftingItem;

    public Texture2D pointingHandMouseIcon;

    public void OnButtonClicked()
    {
        GameObject craftingItem = transform.parent.gameObject;
        CraftingItemScript itemScript = craftingItem.GetComponent<CraftingItemScript>();

        bool doesPlayerHaveAllIngredients = false;

        foreach (RecipeIngredient ingredient in itemScript.itemRecipe.ingredients)
        {
            bool doesPlayerHaveItemAndNecessaryAmount = CheckIfPlayerHasIngredient(ingredient);

            print(doesPlayerHaveItemAndNecessaryAmount);
            if (doesPlayerHaveItemAndNecessaryAmount)
            {
                doesPlayerHaveAllIngredients = true;
                continue;
            }

            doesPlayerHaveAllIngredients = false;
        }

        if (doesPlayerHaveAllIngredients)
        {
            inventoryUIScript.AddItem(itemScript.itemRecipe.resultItem, 1);
            foreach (RecipeIngredient ingredient in itemScript.itemRecipe.ingredients)
            {
                inventoryUIScript.SubstractItemAmount(ingredient.item, ingredient.amount);
            }

            inventoryUIScript.ChangeEquippedSlotColorAndResetThePrevious(0);
        }
    }


    public bool CheckIfPlayerHasIngredient(RecipeIngredient ingredient)
    {
        foreach (InventorySlot slot in inventoryUIScript.inventorySlots)
        {
            if (slot.HasAnInventoryItem() == false)
                continue;

            InventoryItem item = slot.gameObject.transform.GetComponentInChildren<InventoryItem>();

            if (item.item != ingredient.item)
                continue;

            return (item.amount >= ingredient.amount);
        }

        return false;
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