using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class SavingSystem : MonoBehaviour
{

    [SerializeField] private TreeSpawningSystem treeSpawningSystem;
    [SerializeField] Text treeChoppedText;
    
    private void Start()
    {
        LoadData();
    }

    
    private void Update()
    {
        
    }


    public void SaveData()
    {
        SavingModel model = new SavingModel();
        model.treesChopped = treeSpawningSystem.GetAmountOfTreesAlreadyChopped();
        print("Model.treesChopped on Save: " + (model.treesChopped).ToString());
        print("GetAmountOfTreesChopped: " + (treeSpawningSystem.GetAmountOfTreesAlreadyChopped()).ToString());

        string json = JsonUtility.ToJson(model);
        File.WriteAllText(Application.persistentDataPath + "/save.json", json);

        print("Data Saved");
    }


    public void LoadData()
    {
        SavingModel model = JsonUtility.FromJson<SavingModel>(File.ReadAllText(Application.persistentDataPath + "/save.json"));
        print("Model.treesChopped on Load: " + (model.treesChopped).ToString());
        treeChoppedText.text = (model.treesChopped).ToString() + "/10";
        treeSpawningSystem.SetAmountOfTreesAlreadyChopped(model.treesChopped);
        print(treeChoppedText.text);
        print("Data loaded");
    }
}


public class SavingModel
{
    public int treesChopped;
}
