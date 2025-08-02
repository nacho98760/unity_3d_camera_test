using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class SavingSystem : MonoBehaviour
{

    [SerializeField] private TreeSpawningSystem treeSpawningSystem;
    
    private void Start()
    {
        LoadData();
    }


    public void SaveData()
    {
        SavingModel model = new SavingModel();

        string json = JsonUtility.ToJson(model);
        File.WriteAllText(Application.persistentDataPath + "/save.json", json);

        print("Data Saved");
    }


    public void LoadData()
    {
        SavingModel model = JsonUtility.FromJson<SavingModel>(File.ReadAllText(Application.persistentDataPath + "/save.json"));
        print("Model.treesChopped on Load: " + (model.amountOfLogs).ToString());
        print("Data loaded");
    }
}


public class SavingModel
{
    public int amountOfLogs;
}
