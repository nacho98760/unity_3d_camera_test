using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TreeSpawningSystem : MonoBehaviour
{
    public Text treesChoppedText;
    public GameObject treePrefab;

    private bool treeAlreadyInQueue = false;
    private int treesAlreadyChopped = 0;

    private void Start()
    {
        treesChoppedText.text = treesAlreadyChopped.ToString() + "/10";
    }

    private void Update()
    {
        if (transform.childCount < 5 && treeAlreadyInQueue == false)
        {
            treeAlreadyInQueue = true;
            StartCoroutine(SpawnTree(treePrefab));
        }
    }

    private IEnumerator SpawnTree(GameObject tree)
    {
        yield return new WaitForSeconds(5f);

        float currentXPos = transform.position.x;
        float currentZPos = transform.position.z;

        GameObject newTree = Instantiate(tree, new Vector3(Random.Range(currentXPos - 20f, currentXPos + 20f), transform.position.y, Random.Range(currentZPos - 20f, currentZPos + 20f)), Quaternion.identity);
        newTree.transform.parent = transform;
        treeAlreadyInQueue = false;
    }

    public void DestroyAndChangeTextWhenChopped(GameObject tree)
    {
        Destroy(tree);
        treesAlreadyChopped += 1;
        treesChoppedText.text = treesAlreadyChopped.ToString() + "/10";
    }
}
