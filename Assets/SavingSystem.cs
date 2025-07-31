using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class SavingSystem : MonoBehaviour
{

    [SerializeField] private TreeSpawningSystem treeSpawningSystem;
    [SerializeField] Text amountOfLogsText;
    
    private void Start()
    {
        LoadData();
    }


    public void SaveData()
    {
        SavingModel model = new SavingModel();
        model.amountOfLogs = treeSpawningSystem.GetAmountOfTreesAlreadyChopped();

        string json = JsonUtility.ToJson(model);
        File.WriteAllText(Application.persistentDataPath + "/save.json", json);

        print("Data Saved");
    }


    public void LoadData()
    {
        SavingModel model = JsonUtility.FromJson<SavingModel>(File.ReadAllText(Application.persistentDataPath + "/save.json"));
        print("Model.treesChopped on Load: " + (model.amountOfLogs).ToString());
        amountOfLogsText.text = (model.amountOfLogs).ToString() + "/10";
        treeSpawningSystem.SetAmountOfTreesAlreadyChopped(model.amountOfLogs);
        print(amountOfLogsText.text);
        print("Data loaded");
    }
}


public class SavingModel
{
    public int amountOfLogs;
}
