using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RecipeIngredient
{
    public Item item;
    public int amount;
}

[System.Serializable]
public class Recipe
{
    public string recipeName;
    public List<RecipeIngredient> ingredients = new List<RecipeIngredient>();
    public Item resultItem;
}