using System.Collections;
using UnityEngine;

public class TreeSpawningSystem : MonoBehaviour
{
    public GameObject treePrefab;
    private bool treeAlreadyInQueue = false;
    
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
        GameObject newTree = Instantiate(tree, new Vector3(Random.Range(-20f, 20f), 0.759f, Random.Range(-15f, 20f)), Quaternion.identity);
        newTree.transform.parent = transform;
        treeAlreadyInQueue = false;
    }
}
