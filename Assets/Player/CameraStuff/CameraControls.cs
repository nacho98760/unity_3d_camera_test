using UnityEngine;

public class CameraControls : MonoBehaviour
{

    [SerializeField] private GameObject InventoryUI;

    float sensX = 500f;
    float sensY = 500f;

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
        checkForCursorState();
    }


    void checkForCursorState()
    {
        if (InventoryUI.activeSelf || player.isPlayerAlive == false || player.PickItemCanvas.gameObject.activeSelf)
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

            transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
            player.gameObject.transform.rotation = Quaternion.Euler(0, yRotation, 0);
        }
    }
}
