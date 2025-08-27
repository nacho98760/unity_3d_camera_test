using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.IMGUI.Controls.PrimitiveBoundsHandle;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] public GameObject InventoryUI;


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

        isPlayerAlive = true;
        playerRigidBody = GetComponent<Rigidbody>();
        playerRigidBody.freezeRotation = true;

        if (didPlayerPickedUpStarterAxe == false)
        {
            AddStarterAxe();
        }
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

        starterAxe.AddComponent<CapsuleCollider>();
        starterAxe.GetComponent<CapsuleCollider>().isTrigger = true;
        starterAxe.GetComponent<CapsuleCollider>().includeLayers = LayerMask.GetMask("PlayerLayer");
        starterAxe.GetComponent<CapsuleCollider>().excludeLayers = 0;
    }

    //----------------Check----------------
    private void OnTriggerEnter(Collider other)
    {   
        // If layer == 10, it means the object the player is colliding with is the starter axe
        print("Entered");
        print(other.gameObject.layer);
        if (other.gameObject.layer == 10)
        {
            inventoryUIScript.AddItem(AxeItem, AxeItem.amountToAddOnInv);
            inventoryUIScript.Axe.transform.gameObject.SetActive(inventoryUIScript.CheckIfAxeIsEquipped());
            inventoryUIScript.CheckAxeAnimationState(inventoryUIScript.CheckIfAxeIsEquipped());
            Destroy(other.gameObject);
            didPlayerPickedUpStarterAxe = true;

        }
    }
    //----------------Check----------------
}
