using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TreeSpawningSystem : MonoBehaviour
{

    [SerializeField] public SavingSystem savingSystem;

    public Text treeChoppedText;
    public GameObject treePrefab;
    public GameObject treeContainer;
    private bool treeAlreadyInQueue = false;
    public static int treesAlreadyChopped;


    private void Start()
    {
        treeChoppedText.text = treesAlreadyChopped.ToString() + "/10";
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


    public void DestroyAndChangeTextWhenChopped(GameObject tree)
    {
        Destroy(tree);
        treesAlreadyChopped++;
        print(treesAlreadyChopped);
        treeChoppedText.text = treesAlreadyChopped.ToString() + "/10";
        savingSystem.SaveData();
        print("Saved data from here!");
    }


    public int GetAmountOfTreesAlreadyChopped()
    {
        return treesAlreadyChopped;
    }

    public void SetAmountOfTreesAlreadyChopped(int amount)
    {
        treesAlreadyChopped = amount;
    }
}
