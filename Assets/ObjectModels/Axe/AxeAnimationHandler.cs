using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering.UI;
using UnityEngine.UI;

public class AxeAnimationHandler : MonoBehaviour
{   
    [SerializeField] private TreeSpawningSystem treeSpawningSystem;
    [SerializeField] private GameObject InventoryUI;

    [SerializeField] private InventoryUIScript inventoryUIScript;

    public Item logItem;

    private AudioSource ChopAudioComponent;

    private Animator animator;
    private string currentAnimation = "";
    private float chopAnimationLength = 1.667f;
    private float currentChopAnimationTime;
    private bool canChop = true;

    [SerializeField] private float damagePerHit = 10f;


    private void Start()
    {
        animator = GetComponent<Animator>();
        ChopAudioComponent = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && currentAnimation != "Chop" && InventoryUI.activeSelf == false)
        {
            StartCoroutine(Chop());
        }

        GetCurrentChopAnimationTime();
    }

    private void ChangeAnimation(string animation, float crossfade = 0.2f)
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
            StartCoroutine(ChopCooldown());
            ChopAudioComponent.Play();
            other.gameObject.GetComponent<HealthComponent>().DamageObject(damagePerHit);
            
            if (other.gameObject.GetComponent<HealthComponent>().isAlive == false)
            {
                Destroy(other.gameObject);
                inventoryUIScript.AddItem(logItem);
            }
        }
    }

    private IEnumerator ChopCooldown()
    {
        canChop = false;
        yield return new WaitForSeconds(1);
        canChop = true;
    }


    private void ChangeAxeVisibilityIfEquipped()
    {

    }
}
