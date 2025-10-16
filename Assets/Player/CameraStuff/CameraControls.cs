using UnityEngine;

public class CameraControls : MonoBehaviour
{
    [SerializeField] private GameObject InventoryUI;

    float sensX = 100f;
    float sensY = 100f;

    public PlayerMovement player;

    float xRotation;
    float yRotation;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        CheckForCursorState();
    }


    public bool CheckIfAnyUIIsOpen()
    {
        GameObject[] allObjects = Resources.FindObjectsOfTypeAll<GameObject>();

        bool isAnyUIOpen = false;

        foreach (GameObject obj in allObjects)
        {
            if (obj.scene.IsValid())
            {
                if (obj.CompareTag("MainUI"))
                {
                    if (obj.activeSelf)
                    {
                        isAnyUIOpen = true;
                        break;
                    }
                }
            }
        }

        return isAnyUIOpen;
    }


    void CheckForCursorState()
    {
        if (player.isPlayerAlive == false || CheckIfAnyUIIsOpen())
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            float mouseX = Input.GetAxisRaw("Mouse X") * sensX;
            float mouseY = Input.GetAxisRaw("Mouse Y") * sensY;

            yRotation += mouseX * Time.deltaTime;
            xRotation -= mouseY * Time.deltaTime;
            xRotation = Mathf.Clamp(xRotation, -45f, 45f);

            transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0);
            player.gameObject.transform.rotation = Quaternion.Euler(0, yRotation, 0);
        }
    }
}
