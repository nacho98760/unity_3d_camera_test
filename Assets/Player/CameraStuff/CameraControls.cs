using UnityEngine;

public class CameraControls : MonoBehaviour
{

    [SerializeField] private GameObject InventoryUI;

    float sensX = 400;
    float sensY = 400;

    public GameObject playerCharacter;

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
        if (InventoryUI.activeSelf)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            float mouseX = Input.GetAxisRaw("Mouse X") * sensX * Time.deltaTime;
            float mouseY = Input.GetAxisRaw("Mouse Y") * sensY * Time.deltaTime;

            yRotation += mouseX;
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -45f, 45f);

            transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
            playerCharacter.transform.rotation = Quaternion.Euler(0, yRotation, 0);
        }
    }
}
