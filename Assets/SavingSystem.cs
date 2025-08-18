#nullable enable

using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class SavingSystem : MonoBehaviour
{
    public InventoryUIScript inventoryUIScript;
    [SerializeField] private TreeSpawningSystem treeSpawningSystem;

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

        InventoryItem newInvItem = Instantiate(InvItemPrefab, invSlot.transform);

        newInvItem.itemName = itemName;
        SetItemTexture(newInvItem, itemName);
        newInvItem.amount = itemAmount;
        newInvItem.amountText.text = ChangeVisibilityBasedOnAmount(itemAmount);
    }

    public string ChangeVisibilityBasedOnAmount(int amount)
    {
        if (amount > 1)
        {
            return amount.ToString();
        }
        else
        {
            return "";
        }
    }

    public void SetItemTexture(InventoryItem item, string? itemName)
    {
        if (itemName == "Axe") 
        {
            item.image.texture = AxeTexture;
        }
        else if (itemName == "Log")
        {
            item.image.texture = LogTexture;
        }
    }


    public string? CheckItemName(InventorySlot invSlot) 
    {
       if (invSlot.HasAnInventoryItem())
       {
            print("True, " + invSlot.transform.GetChild(0).gameObject.GetComponent<InventoryItem>().itemName);
            return invSlot.transform.GetChild(0).gameObject.GetComponent<InventoryItem>().itemName;
       }
       else
       {
            print("False");
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

        model.toolBarInvItem1Name = CheckItemName(inventoryUIScript.inventorySlots[0]);
        model.toolBarInvItem2Name = CheckItemName(inventoryUIScript.inventorySlots[1]);
        model.toolBarInvItem3Name = CheckItemName(inventoryUIScript.inventorySlots[2]);
        model.toolBarInvItem4Name = CheckItemName(inventoryUIScript.inventorySlots[3]);
        model.toolBarInvItem5Name = CheckItemName(inventoryUIScript.inventorySlots[4]);
        model.toolBarInvItem6Name = CheckItemName(inventoryUIScript.inventorySlots[5]);
        model.toolBarInvItem7Name = CheckItemName(inventoryUIScript.inventorySlots[6]);
        
        model.InvItem1Name = CheckItemName(inventoryUIScript.inventorySlots[7]);
        model.InvItem2Name = CheckItemName(inventoryUIScript.inventorySlots[8]);
        model.InvItem3Name = CheckItemName(inventoryUIScript.inventorySlots[9]);
        model.InvItem4Name = CheckItemName(inventoryUIScript.inventorySlots[10]);
        model.InvItem5Name = CheckItemName(inventoryUIScript.inventorySlots[11]);
        model.InvItem6Name = CheckItemName(inventoryUIScript.inventorySlots[12]);
        model.InvItem7Name = CheckItemName(inventoryUIScript.inventorySlots[13]);
        model.InvItem8Name = CheckItemName(inventoryUIScript.inventorySlots[14]);
        model.InvItem9Name = CheckItemName(inventoryUIScript.inventorySlots[15]);
        model.InvItem10Name = CheckItemName(inventoryUIScript.inventorySlots[16]);
        model.InvItem11Name = CheckItemName(inventoryUIScript.inventorySlots[17]);
        model.InvItem12Name = CheckItemName(inventoryUIScript.inventorySlots[18]);
        model.InvItem13Name = CheckItemName(inventoryUIScript.inventorySlots[19]);
        model.InvItem14Name = CheckItemName(inventoryUIScript.inventorySlots[20]);
        model.InvItem15Name = CheckItemName(inventoryUIScript.inventorySlots[21]);
        model.InvItem16Name = CheckItemName(inventoryUIScript.inventorySlots[22]);
        model.InvItem17Name = CheckItemName(inventoryUIScript.inventorySlots[23]);
        model.InvItem18Name = CheckItemName(inventoryUIScript.inventorySlots[24]);
        model.InvItem19Name = CheckItemName(inventoryUIScript.inventorySlots[25]);
        model.InvItem20Name = CheckItemName(inventoryUIScript.inventorySlots[26]);
        model.InvItem21Name = CheckItemName(inventoryUIScript.inventorySlots[27]);


        model.toolBarInvItem1Amount = CheckItemAmount(inventoryUIScript.inventorySlots[0]);
        model.toolBarInvItem2Amount = CheckItemAmount(inventoryUIScript.inventorySlots[1]);
        model.toolBarInvItem3Amount = CheckItemAmount(inventoryUIScript.inventorySlots[2]);
        model.toolBarInvItem4Amount = CheckItemAmount(inventoryUIScript.inventorySlots[3]);
        model.toolBarInvItem5Amount = CheckItemAmount(inventoryUIScript.inventorySlots[4]);
        model.toolBarInvItem6Amount = CheckItemAmount(inventoryUIScript.inventorySlots[5]);
        model.toolBarInvItem7Amount = CheckItemAmount(inventoryUIScript.inventorySlots[6]);

        model.InvItem1Amount = CheckItemAmount(inventoryUIScript.inventorySlots[7]);
        model.InvItem2Amount = CheckItemAmount(inventoryUIScript.inventorySlots[8]);
        model.InvItem3Amount = CheckItemAmount(inventoryUIScript.inventorySlots[9]);
        model.InvItem4Amount = CheckItemAmount(inventoryUIScript.inventorySlots[10]);
        model.InvItem5Amount = CheckItemAmount(inventoryUIScript.inventorySlots[11]);
        model.InvItem6Amount = CheckItemAmount(inventoryUIScript.inventorySlots[12]);
        model.InvItem7Amount = CheckItemAmount(inventoryUIScript.inventorySlots[13]);
        model.InvItem8Amount = CheckItemAmount(inventoryUIScript.inventorySlots[14]);
        model.InvItem9Amount = CheckItemAmount(inventoryUIScript.inventorySlots[15]);
        model.InvItem10Amount = CheckItemAmount(inventoryUIScript.inventorySlots[16]);
        model.InvItem11Amount = CheckItemAmount(inventoryUIScript.inventorySlots[17]);
        model.InvItem12Amount = CheckItemAmount(inventoryUIScript.inventorySlots[18]);
        model.InvItem13Amount = CheckItemAmount(inventoryUIScript.inventorySlots[19]);
        model.InvItem14Amount = CheckItemAmount(inventoryUIScript.inventorySlots[20]);
        model.InvItem15Amount = CheckItemAmount(inventoryUIScript.inventorySlots[21]);
        model.InvItem16Amount = CheckItemAmount(inventoryUIScript.inventorySlots[22]);
        model.InvItem17Amount = CheckItemAmount(inventoryUIScript.inventorySlots[23]);
        model.InvItem18Amount = CheckItemAmount(inventoryUIScript.inventorySlots[24]);
        model.InvItem19Amount = CheckItemAmount(inventoryUIScript.inventorySlots[25]);
        model.InvItem20Amount = CheckItemAmount(inventoryUIScript.inventorySlots[26]);
        model.InvItem21Amount = CheckItemAmount(inventoryUIScript.inventorySlots[27]);


        string json = JsonUtility.ToJson(model);
        File.WriteAllText(Application.persistentDataPath + "/save.json", json);

        print("Data Saved");
    }


    public void LoadData()
    {
        SavingModel model = JsonUtility.FromJson<SavingModel>(File.ReadAllText(Application.persistentDataPath + "/save.json"));

        SetItemData(inventoryUIScript.inventorySlots[0], model.toolBarInvItem1Name, model.toolBarInvItem1Amount);
        SetItemData(inventoryUIScript.inventorySlots[1], model.toolBarInvItem2Name, model.toolBarInvItem2Amount);
        SetItemData(inventoryUIScript.inventorySlots[2], model.toolBarInvItem3Name, model.toolBarInvItem3Amount);
        SetItemData(inventoryUIScript.inventorySlots[3], model.toolBarInvItem4Name, model.toolBarInvItem4Amount);
        SetItemData(inventoryUIScript.inventorySlots[4], model.toolBarInvItem5Name, model.toolBarInvItem5Amount);
        SetItemData(inventoryUIScript.inventorySlots[5], model.toolBarInvItem6Name, model.toolBarInvItem6Amount);
        SetItemData(inventoryUIScript.inventorySlots[6], model.toolBarInvItem7Name, model.toolBarInvItem7Amount);

        SetItemData(inventoryUIScript.inventorySlots[7], model.InvItem1Name, model.InvItem1Amount);
        SetItemData(inventoryUIScript.inventorySlots[8], model.InvItem2Name, model.InvItem2Amount);
        SetItemData(inventoryUIScript.inventorySlots[9], model.InvItem3Name, model.InvItem3Amount);
        SetItemData(inventoryUIScript.inventorySlots[10], model.InvItem4Name, model.InvItem4Amount);
        SetItemData(inventoryUIScript.inventorySlots[11], model.InvItem5Name, model.InvItem5Amount);
        SetItemData(inventoryUIScript.inventorySlots[12], model.InvItem6Name, model.InvItem6Amount);
        SetItemData(inventoryUIScript.inventorySlots[13], model.InvItem7Name, model.InvItem7Amount);
        SetItemData(inventoryUIScript.inventorySlots[14], model.InvItem8Name, model.InvItem8Amount);
        SetItemData(inventoryUIScript.inventorySlots[15], model.InvItem9Name, model.InvItem9Amount);
        SetItemData(inventoryUIScript.inventorySlots[16], model.InvItem10Name, model.InvItem10Amount);
        SetItemData(inventoryUIScript.inventorySlots[17], model.InvItem11Name, model.InvItem11Amount);
        SetItemData(inventoryUIScript.inventorySlots[18], model.InvItem12Name, model.InvItem12Amount);
        SetItemData(inventoryUIScript.inventorySlots[19], model.InvItem13Name, model.InvItem13Amount);
        SetItemData(inventoryUIScript.inventorySlots[20], model.InvItem14Name, model.InvItem14Amount);
        SetItemData(inventoryUIScript.inventorySlots[21], model.InvItem15Name, model.InvItem15Amount);
        SetItemData(inventoryUIScript.inventorySlots[22], model.InvItem16Name, model.InvItem16Amount);
        SetItemData(inventoryUIScript.inventorySlots[23], model.InvItem17Name, model.InvItem17Amount);
        SetItemData(inventoryUIScript.inventorySlots[24], model.InvItem18Name, model.InvItem18Amount);
        SetItemData(inventoryUIScript.inventorySlots[25], model.InvItem19Name, model.InvItem19Amount);
        SetItemData(inventoryUIScript.inventorySlots[26], model.InvItem20Name, model.InvItem20Amount);
        SetItemData(inventoryUIScript.inventorySlots[27], model.InvItem21Name, model.InvItem21Amount);
    }
}


public class SavingModel
{
    public string? toolBarInvItem1Name;
    public string? toolBarInvItem2Name;
    public string? toolBarInvItem3Name;
    public string? toolBarInvItem4Name;
    public string? toolBarInvItem5Name;
    public string? toolBarInvItem6Name;
    public string? toolBarInvItem7Name;

    public string? InvItem1Name;
    public string? InvItem2Name;
    public string? InvItem3Name;
    public string? InvItem4Name;
    public string? InvItem5Name;
    public string? InvItem6Name;
    public string? InvItem7Name;
    public string? InvItem8Name;
    public string? InvItem9Name;
    public string? InvItem10Name;
    public string? InvItem11Name;
    public string? InvItem12Name;
    public string? InvItem13Name;
    public string? InvItem14Name;
    public string? InvItem15Name;
    public string? InvItem16Name;
    public string? InvItem17Name;
    public string? InvItem18Name;
    public string? InvItem19Name;
    public string? InvItem20Name;
    public string? InvItem21Name;

    public int toolBarInvItem1Amount;
    public int toolBarInvItem2Amount;
    public int toolBarInvItem3Amount;
    public int toolBarInvItem4Amount;
    public int toolBarInvItem5Amount;
    public int toolBarInvItem6Amount;
    public int toolBarInvItem7Amount;

    public int InvItem1Amount;
    public int InvItem2Amount;
    public int InvItem3Amount;
    public int InvItem4Amount;
    public int InvItem5Amount;
    public int InvItem6Amount;
    public int InvItem7Amount;
    public int InvItem8Amount;
    public int InvItem9Amount;
    public int InvItem10Amount;
    public int InvItem11Amount;
    public int InvItem12Amount;
    public int InvItem13Amount;
    public int InvItem14Amount;
    public int InvItem15Amount;
    public int InvItem16Amount;
    public int InvItem17Amount;
    public int InvItem18Amount;
    public int InvItem19Amount;
    public int InvItem20Amount;
    public int InvItem21Amount;
}
