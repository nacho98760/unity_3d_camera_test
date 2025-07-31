using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TreeSpawningSystem : MonoBehaviour
{

    public SavingSystem savingSystem;

    public Text amountOfLogsText;
    public GameObject treePrefab;
    public GameObject treeContainer;
    private bool treeAlreadyInQueue = false;
    public static int amountOfLogs;


    private void Start()
    {
        amountOfLogsText.text = amountOfLogs.ToString();
    }


    private void Update()
    {
        if (treeContainer.transform.childCount < 5 && treeAlreadyInQueue == false)
        {
            treeAlreadyInQueue = true;
            StartCoroutine(SpawnTree(treePrefab));
        }
    }


    private IEnumerator SpawnTree(GameObject tree)
    {
        yield return new WaitForSeconds(5f);

        float currentXPos = treeContainer.transform.position.x;
        float currentZPos = treeContainer.transform.position.z;

        GameObject newTree = Instantiate(tree, new Vector3(Random.Range(currentXPos - 20f, currentXPos + 20f), treeContainer.transform.position.y, Random.Range(currentZPos - 20f, currentZPos + 20f)), Quaternion.identity);
        newTree.transform.parent = treeContainer.transform;
        treeAlreadyInQueue = false;
    }


    public void UpdateTextWhenChopped(GameObject tree)
    {
        amountOfLogs += 3;
        amountOfLogsText.text = amountOfLogs.ToString();
        savingSystem.SaveData();
    }


    public int GetAmountOfTreesAlreadyChopped()
    {
        return amountOfLogs;
    }

    public void SetAmountOfTreesAlreadyChopped(int amount)
    {
        amountOfLogs = amount;
    }
}
