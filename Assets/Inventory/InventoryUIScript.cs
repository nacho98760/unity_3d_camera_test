using UnityEngine;

public class InventoryUIScript : MonoBehaviour
{
    [SerializeField] public GameObject InventoryUI;
    [SerializeField] public GameObject GridWithInvSlots;
    public bool isOpen;

    void Start()
    {
        InventoryUI.SetActive(false);
        isOpen = false;
    }


    private void Update()
    {
        checkInvState();
    }


    private void checkInvState()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            UpdateInv();
            if (isOpen)
            {
                InventoryUI.SetActive(false);
                isOpen = false;
            }
            else
            {
                InventoryUI.SetActive(true);
                isOpen = true;
            }
        }
    }


    public void UpdateInv()
    {
        // ...
    }
}
