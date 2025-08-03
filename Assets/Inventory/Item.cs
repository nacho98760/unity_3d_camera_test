using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Item", menuName = "Scriptable Objects/Item")]
public class Item : ScriptableObject
{
    public Texture2D image;
    public ItemType type;
    public ActionType actionType;

    public bool stackable = true;
}

public enum ItemType
{
    Tool,
    Resource,
}

public enum ActionType
{
    None,
    Chop,
}