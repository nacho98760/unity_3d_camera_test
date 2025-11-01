using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.IMGUI.Controls.PrimitiveBoundsHandle;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public CameraControls playerCameraControls;

    public LayerMask craftingTableLayer;
    LayerMask PickableObjects;

    public GameObject InventoryUI;

    public Canvas PickItemCanvas;
    private GameObject pickedItemObject;
    private bool canPlayerPickItem = false;

    public Canvas CraftingCanvas;

    public bool isPlayerAlive;

    public bool didPlayerPickedUpStarterAxe;
    public Transform starterAxeSpawnPoint;
    public GameObject AxeModelPrefab;
    public Item[] pickableItems;
    public InventoryUIScript inventoryUIScript;

    float movementSpeed = 2f;
    float jumpForce = 4f;

    public float playerHeight;
    public LayerMask floorLayer;

    public float groundDrag;

    float horizontalInput;
    float verticalInput;

    Vector3 movementDirection;

    Rigidbody playerRigidBody;

    private void Awake()
    {
        Application.targetFrameRate = 180;
        PickableObjects = LayerMask.NameToLayer("PickableObjects");
    }

    private void Start()
    {
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

        MovePlayer(isGrounded);
        speedControl();
        CheckForCraftingTableOpeningReq();

        CheckIfPlayerPickedUpItem();
    }

    private void FixedUpdate()
    {
        if (isPlayerAlive == false || playerCameraControls.CheckIfAnyUIIsOpen())
        {
            movementDirection = Vector3.zero;
        }
        else
        {
            movementDirection = transform.forward * verticalInput + transform.right * horizontalInput;
            playerRigidBody.AddForce(movementDirection.normalized * movementSpeed, ForceMode.VelocityChange);
        }
    }


    // ----------------------------- Player Movement - START -----------------------------
    private void MovePlayer(bool isGrounded)
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
    // ----------------------------- Player Movement - END -----------------------------


    private void AddStarterAxe()
    {
        GameObject starterAxe = Instantiate(AxeModelPrefab);
        starterAxe.transform.position = starterAxeSpawnPoint.position;
        starterAxe.transform.rotation = starterAxeSpawnPoint.rotation;
        starterAxe.transform.localScale = starterAxeSpawnPoint.localScale;
        starterAxe.layer = PickableObjects;

        BoxCollider axeCollider = starterAxe.AddComponent<BoxCollider>();
        axeCollider.size = new Vector3(axeCollider.size.x, axeCollider.size.y, axeCollider.size.z + 2.5f);
        axeCollider.isTrigger = true;
        axeCollider.includeLayers = LayerMask.GetMask("PlayerLayer");
        axeCollider.excludeLayers = 0;
    }


    // ----------------------------- Pickable Objects - START -----------------------------
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == PickableObjects)
        {
            canPlayerPickItem = true;
            pickedItemObject = other.gameObject;
            PickItemCanvas.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        canPlayerPickItem = false;

        if (other.gameObject.layer == PickableObjects) 
        {
            if (pickedItemObject == other.gameObject)
            {
                pickedItemObject = null;
            }

            PickItemCanvas.gameObject.SetActive(false);
        }
    }

    private void CheckIfPlayerPickedUpItem()
    {
        if (Input.GetKeyDown(KeyCode.F) && canPlayerPickItem)
        {
            OnPickItemKeyPressed();
        }
    }

    private void OnPickItemKeyPressed()
    {
        if (pickedItemObject == null)
            return;

        Item itemToAdd = null;

        foreach (Item item in pickableItems)
        {
            if (pickedItemObject.name.Contains(item.itemName) || pickedItemObject.name.Contains(item.whereDoesPlayerGetItemFrom))
            {
                itemToAdd = item;
                break;
            }
        }

        inventoryUIScript.AddItem(itemToAdd, itemToAdd.amountToAddOnInv);
        Destroy(pickedItemObject);
        PickItemCanvas.gameObject.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        if (itemToAdd.itemName == "Axe")
        {
            CheckIfPickedItemWasAxe();
        }
    }

    private void CheckIfPickedItemWasAxe()
    {
        inventoryUIScript.Axe.transform.gameObject.SetActive(inventoryUIScript.CheckIfAxeIsEquipped());
        inventoryUIScript.CheckAxeAnimationState(inventoryUIScript.CheckIfAxeIsEquipped());
        didPlayerPickedUpStarterAxe = true;
    }
    // ----------------------------- Pickable Objects - END -----------------------------


    private void CheckForCraftingTableOpeningReq()
    {
        Transform playerCameraPos = transform.Find("CameraPos");

        if (Input.GetMouseButtonDown(1))
        {
            if (Physics.Raycast(playerCameraPos.position, playerCameraPos.forward, out RaycastHit hit, 50f, craftingTableLayer))
            {
                CraftingCanvas.gameObject.SetActive(true);
            }
        }

    }
}
