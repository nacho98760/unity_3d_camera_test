using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering.UI;
using UnityEngine.UI;

public class AxeAnimationHandler : MonoBehaviour
{   
    [SerializeField] private TreeSpawningSystem treeSpawningSystem;

    public GameObject treePrefab;
    public Text treeChoppedText;
    public GameObject treeContainer;
    private AudioSource ChopAudioComponent;
    public SavingSystem savingSystem;

    private Animator animator;
    private string currentAnimation = "";
    private float chopAnimationLength = 1.667f;
    private float currentChopAnimationTime;

    private void Start()
    {
        animator = GetComponent<Animator>();
        treeSpawningSystem = gameObject.AddComponent<TreeSpawningSystem>();
        treeSpawningSystem.treePrefab = treePrefab;
        treeSpawningSystem.treeChoppedText = treeChoppedText;
        treeSpawningSystem.treeContainer = treeContainer;
        ChopAudioComponent = GetComponent<AudioSource>();
        treeSpawningSystem.savingSystem = savingSystem;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && currentAnimation != "Chop")
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
        // If currentChopAnimationTime > 0f, it means that the current animation can only be "Chop". Therefore, its not necessary to check what the current animation is.
        if (currentChopAnimationTime > 0.65f)
        {
            ChopAudioComponent.Play();
            treeSpawningSystem.DestroyAndChangeTextWhenChopped(other.gameObject);
        }
    }
}
