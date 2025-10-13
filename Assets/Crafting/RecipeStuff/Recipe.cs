using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Recipe", menuName = "Crafting/Recipe")]
public class Recipe : ScriptableObject
{
    public string recipeName;
    public List<RecipeIngredient> ingredients = new List<RecipeIngredient>();
    public Item resultItem;
}