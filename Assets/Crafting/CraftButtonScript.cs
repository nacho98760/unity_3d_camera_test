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

        foreach (RecipeIngredient ingredient in itemScript.itemRecipe.ingredients)
        {
            bool doesPlayerHaveItemAndNecessaryAmount = CheckIfPlayerHasIngredient(ingredient);

            if (doesPlayerHaveItemAndNecessaryAmount)
            {
                inventoryUIScript.AddItem(itemScript.itemRecipe.resultItem, 1);

            }
        }
    }


    public bool CheckIfPlayerHasIngredient(RecipeIngredient ingredient)
    {
        bool hasIngredientAndNecessaryAmount = false;

        for (int i = 0; i < inventoryUIScript.inventorySlots.Length; i++)
        {
            InventorySlot slot = inventoryUIScript.inventorySlots[i];

            if (slot.HasAnInventoryItem())
            {
                InventoryItem item = slot.gameObject.transform.GetChild(0).GetComponent<InventoryItem>();

                if (item.item == ingredient.item)
                {
                    if (item.amount >= ingredient.amount)
                    {
                        hasIngredientAndNecessaryAmount = true;
                        break;
                    }
                }
            }
        }

        return hasIngredientAndNecessaryAmount;
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