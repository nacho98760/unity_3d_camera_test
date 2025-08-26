using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SavingSystem : MonoBehaviour
{
    public InventoryUIScript inventoryUIScript;
    [SerializeField] private TreeSpawningSystem treeSpawningSystem;

    public Item[] itemList;
    public InventoryItem InvItemPrefab;
    public Texture2D AxeTexture;
    public Texture2D LogTexture;
    
    private void Start()
    {
        LoadData();
    }

    private void OnApplicationQuit()
    {
        SaveData();
    }

    public void SetItemData(InventorySlot invSlot, string? itemName, int itemAmount)
    {
        if (itemName == null || itemName == "")
        {
            return;
        }

        Item itemObject = GetItemObjectByName(itemName);
        inventoryUIScript.SpawnNewItem(itemObject, invSlot, itemAmount);
    }

    public Item? GetItemObjectByName(string itemName)
    {
        Item? itemFound = null;
        foreach (Item item in itemList)
        {
            if (item.name == itemName)
            {
                itemFound = item;
            }
        }

        return itemFound;
    }


    public string? CheckItemName(InventorySlot invSlot) 
    {
       if (invSlot.HasAnInventoryItem())
       {
            return invSlot.transform.GetChild(0).gameObject.GetComponent<InventoryItem>().itemName;
       }
       else
       {
            return null;
       }

    }

    public int CheckItemAmount(InventorySlot invSlot)
    {
        if (invSlot.HasAnInventoryItem())
        {
            return invSlot.transform.GetChild(0).gameObject.GetComponent<InventoryItem>().amount;
        }
        else
        {
            return 0;
        }
    }



    public void SaveData()
    {
        SavingModel model = new SavingModel();

        for (int i = 0; i < inventoryUIScript.inventorySlots.Length; i++)
        {
            model.InvItemNames[i] = CheckItemName(inventoryUIScript.inventorySlots[i]);
            model.InvItemAmounts[i] = CheckItemAmount(inventoryUIScript.inventorySlots[i]);
        }

        string json = JsonUtility.ToJson(model);
        File.WriteAllText(Application.persistentDataPath + "/save.json", json);

        print("Data Saved");
    }


    public void LoadData()
    {
        if (!File.Exists(Application.persistentDataPath + "/save.json"))
        {
            SavingModel emptyModel = CreateEmptyInventory();
            string json = JsonUtility.ToJson(emptyModel);
            File.WriteAllText(Application.persistentDataPath + "/save.json", json);
            return;
        }

        SavingModel model = JsonUtility.FromJson<SavingModel>(File.ReadAllText(Application.persistentDataPath + "/save.json"));


        for (int i = 0; i < inventoryUIScript.inventorySlots.Length; i++)
        {
            SetItemData(inventoryUIScript.inventorySlots[i], model.InvItemNames[i], model.InvItemAmounts[i]);
        }

        inventoryUIScript.ChangeEquippedSlotColorAndResetThePrevious(0);
    }

    public SavingModel CreateEmptyInventory()
    {
        SavingModel emptyModel = new SavingModel();

        for (int i = 0; i < inventoryUIScript.inventorySlots.Length; i++)
        {
            // Reset save.json if it exists
            emptyModel.InvItemNames[i] = "";
            emptyModel.InvItemAmounts[i] = 0;

            if (inventoryUIScript.inventorySlots[i].HasAnInventoryItem())
            {
                Destroy(inventoryUIScript.inventorySlots[i].transform.GetChild(0).gameObject);
            }
        }

        return emptyModel;
    }
}



public class SavingModel
{
    public string[] InvItemNames = new string[28];
    public int[] InvItemAmounts = new int[28];
}
