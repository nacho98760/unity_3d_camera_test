using System.Collections;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering.UI;
using UnityEngine.UI;

public class AxeAnimationHandler : MonoBehaviour
{   
    public PlayerMovement player;
    public CameraControls playerCameraControls;

    [SerializeField] private TreeSpawningSystem treeSpawningSystem;
    [SerializeField] private GameObject InventoryUI;
    [SerializeField] private GameObject CraftingUI;

    [SerializeField] private InventoryUIScript inventoryUIScript;

    public Item logItem;

    private AudioSource ChopAudioComponent;

    private Animator animator;
    public string currentAnimation = "";
    private float chopAnimationLength = 1.667f;
    private float currentChopAnimationTime;
    private bool canChop = true;

    [SerializeField] private float damagePerHit = 10f;

    public GameObject playerCameraHolder;
    public Text playerHealth;
    public Canvas playerDeathCanvas;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        ChopAudioComponent = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && gameObject.activeSelf && currentAnimation != "Chop" && playerCameraControls.CheckIfAnyUIIsOpen() == false)
        {
            StartCoroutine(Chop());
        }

        GetCurrentChopAnimationTime();
    }

    public void ChangeAnimation(string animation, float crossfade = 0.2f)
    {
        if (currentAnimation != animation)
        {
            currentAnimation = animation;
            animator.CrossFade(animation, crossfade);
        }
    }

    private IEnumerator Chop()
    {
        ChangeAnimation("Chop");
        yield return new WaitForSeconds(chopAnimationLength);
        ChangeAnimation("Idle");
    }

    private void GetCurrentChopAnimationTime()
    {
        if (currentAnimation == "Chop")
        {
            AnimatorStateInfo state = animator.GetCurrentAnimatorStateInfo(0);
            currentChopAnimationTime = state.normalizedTime * chopAnimationLength;
        }
        else
        {
            currentChopAnimationTime = 0f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // If currentChopAnimationTime != null, it means that the current animation can only be "Chop". Therefore, its not necessary to check what the current animation is.
        if ((currentChopAnimationTime > 0.65f && currentChopAnimationTime < 0.95f) && canChop)
        {
            if (other.gameObject.GetComponent<HealthComponent>() == null)
                return;

            StartCoroutine(ChopCooldown());
            ChopAudioComponent.Play();

            other.gameObject.GetComponent<HealthComponent>().DamageObject(damagePerHit);

            player.gameObject.GetComponent<HealthComponent>().OnObjectHit += ChangePlayerHealthText;
            player.gameObject.GetComponent<HealthComponent>().DamageObject(damagePerHit);


            if (transform.root.gameObject.GetComponent<HealthComponent>().isAlive == false)
            {
                player.isPlayerAlive = false;
                playerDeathCanvas.gameObject.SetActive(true);
                player.gameObject.GetComponent<HealthComponent>().OnObjectHit -= ChangePlayerHealthText;
            }

            if (other.gameObject.GetComponent<HealthComponent>().isAlive == false)
            {
                inventoryUIScript.AddItem(logItem, logItem.amountToAddOnInv);
                Destroy(other.gameObject);
              
            }
        }
    }


    public void ChangePlayerHealthText(float currentHealth, float maxHealth)
    {
        playerHealth.text = currentHealth.ToString() + "/" + maxHealth.ToString();
        print(currentHealth);
    }

    private IEnumerator ChopCooldown()
    {
        canChop = false;
        yield return new WaitForSeconds(1);
        canChop = true;
    }
}

