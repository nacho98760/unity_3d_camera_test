using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.IMGUI.Controls.PrimitiveBoundsHandle;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public LayerMask craftingTableLayer;

    [SerializeField] public GameObject InventoryUI;

    [SerializeField] private Button pickButton;
    private GameObject pickedItemObject;
    public Canvas PickItemCanvas;
    public Canvas CraftingCanvas;

    public bool isPlayerAlive;

    public bool didPlayerPickedUpStarterAxe;
    public Transform starterAxeSpawnPoint;
    public GameObject AxeModelPrefab;
    public Item AxeItem;
    public InventoryUIScript inventoryUIScript;

    float movementSpeed = 3.5f;
    float jumpForce = 4f;

    public float playerHeight;
    public LayerMask floorLayer;

    public float groundDrag;

    float horizontalInput;
    float verticalInput;

    Vector3 movementDirection;

    Rigidbody playerRigidBody;


    private void Start()
    {
        /*
        starterAxeLayer = LayerMask.NameToLayer("StarterAxeLayer");
        craftingTableLayer = LayerMask.NameToLayer("CraftingTableLayer");
        */

        pickButton.onClick.AddListener(OnPickedItemButtonPressed);
        isPlayerAlive = true;
        playerRigidBody = GetComponent<Rigidbody>();
        playerRigidBody.freezeRotation = true;

        if (didPlayerPickedUpStarterAxe == false)
        {
            AddStarterAxe();
        }

        PickItemCanvas.gameObject.SetActive(false);
    }

    
    private void Update()
    {
        bool isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, floorLayer);

        if (isGrounded)
        {
            playerRigidBody.linearDamping = groundDrag;
        }
        else
        {
            playerRigidBody.linearDamping = 0;
        }

        movePlayer(isGrounded);
        speedControl();
        // CheckForCraftingTableOpeningReq();
    }

    private void FixedUpdate()
    {
        if (InventoryUI.activeSelf || isPlayerAlive == false)
        {
            movementDirection = Vector3.zero;
        }
        else
        {
            movementDirection = transform.forward * verticalInput + transform.right * horizontalInput;
            playerRigidBody.AddForce(movementDirection.normalized * movementSpeed * 10f, ForceMode.Force);
        }
    }


    private void movePlayer(bool isGrounded)
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }
    }

    private void Jump()
    {
        playerRigidBody.linearVelocity = new Vector3(playerRigidBody.linearVelocity.x, 0f, playerRigidBody.linearVelocity.z);

        playerRigidBody.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void speedControl()
    {
        Vector3 flatVelocity = new Vector3(playerRigidBody.linearVelocity.x, 0f, playerRigidBody.linearVelocity.z);

        if (flatVelocity.magnitude > movementSpeed)
        {
            Vector3 limitedVelocity = flatVelocity.normalized * movementSpeed;
            playerRigidBody.linearVelocity = new Vector3(limitedVelocity.x, playerRigidBody.linearVelocity.y, limitedVelocity.z);
        }
    }


    private void AddStarterAxe()
    {
        GameObject starterAxe = Instantiate(AxeModelPrefab);
        starterAxe.transform.position = starterAxeSpawnPoint.position;
        starterAxe.transform.rotation = starterAxeSpawnPoint.rotation;
        starterAxe.transform.localScale = starterAxeSpawnPoint.localScale;
        starterAxe.layer = 10;

        BoxCollider axeCollider = starterAxe.AddComponent<BoxCollider>();
        axeCollider.size = new Vector3(axeCollider.size.x, axeCollider.size.y, axeCollider.size.z + 2.5f);
        axeCollider.isTrigger = true;
        axeCollider.includeLayers = LayerMask.GetMask("PlayerLayer");
        axeCollider.excludeLayers = 0;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 10)
        {
            pickedItemObject = other.gameObject;
            PickItemCanvas.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 10) 
        {
            if (pickedItemObject == other.gameObject)
            {
                pickedItemObject = null;
            }

            PickItemCanvas.gameObject.SetActive(false);
        }
    }

    public void OnPickedItemButtonPressed()
    {
        if (pickedItemObject == null)
            return;

        inventoryUIScript.AddItem(AxeItem, AxeItem.amountToAddOnInv);
        inventoryUIScript.Axe.transform.gameObject.SetActive(inventoryUIScript.CheckIfAxeIsEquipped());
        inventoryUIScript.CheckAxeAnimationState(inventoryUIScript.CheckIfAxeIsEquipped());
        Destroy(pickedItemObject);
        didPlayerPickedUpStarterAxe = true;
        PickItemCanvas.gameObject.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }


    /*
    private void CheckForCraftingTableOpeningReq()
    {
        Transform playerCameraPos = transform.Find("CameraPos");

        if (Input.GetMouseButtonDown(1))
        {
            if (Physics.Raycast(playerCameraPos.position, Vector3.forward, out RaycastHit hit, 50f, craftingTableLayer))
            {
                print("Yes");
                print(hit.collider.gameObject.name);
            }
            else
            {
                print("No");

            }

        }

    }
    */
}
