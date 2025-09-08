using NUnit.Framework;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SavingSystem : MonoBehaviour
{
    public PlayerMovement playerScript;
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

    public Item GetItemObjectByName(string itemName)
    {
        Item itemFound = null;

        foreach (Item item in itemList)
        {
            if (item.name == itemName)
            {
                itemFound = item;
            }
        }

        return itemFound;
    }


    public string CheckItemName(InventorySlot invSlot) 
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

        print(model.didPlayerPickedUpStarterAxe);
        model.didPlayerPickedUpStarterAxe = playerScript.didPlayerPickedUpStarterAxe;

        string json = JsonUtility.ToJson(model);
        File.WriteAllText(Application.persistentDataPath + "/save.json", json);

        print("Data Saved");
    }


    public void LoadData()
    {
        if (!File.Exists(Application.persistentDataPath + "/save.json"))
        {
            CreateEmptyData();
            return;
        }

        SavingModel model = JsonUtility.FromJson<SavingModel>(File.ReadAllText(Application.persistentDataPath + "/save.json"));


        for (int i = 0; i < inventoryUIScript.inventorySlots.Length; i++)
        {
            SetItemData(inventoryUIScript.inventorySlots[i], model.InvItemNames[i], model.InvItemAmounts[i]);
        }


        playerScript.didPlayerPickedUpStarterAxe = model.didPlayerPickedUpStarterAxe;

        inventoryUIScript.ChangeEquippedSlotColorAndResetThePrevious(0);
    }


    public void CreateEmptyData()
    {
        SavingModel emptyModel = new SavingModel();

        CreateEmptyInventory(emptyModel);
        emptyModel.didPlayerPickedUpStarterAxe = false;
        string json = JsonUtility.ToJson(emptyModel);
        File.WriteAllText(Application.persistentDataPath + "/save.json", json);
    }

    public void CreateEmptyInventory(SavingModel emptyModel)
    {
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

        inventoryUIScript.ChangeEquippedSlotColorAndResetThePrevious(0);
    }
}



public class SavingModel
{
    public string[] InvItemNames = new string[28];
    public int[] InvItemAmounts = new int[28];

    public bool didPlayerPickedUpStarterAxe;
}
