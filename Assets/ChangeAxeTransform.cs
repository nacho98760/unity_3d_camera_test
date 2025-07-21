using UnityEngine;
using static UnityEngine.Rendering.DebugUI.Table;

public class ChangeAxeTransform : MonoBehaviour
{
    public Camera playerCamera;

    void Update()
    {
        transform.rotation = playerCamera.transform.rotation;

        float xRotation = transform.rotation.eulerAngles.x;
        float yRotation = transform.rotation.eulerAngles.y;

        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
    
    }
}
