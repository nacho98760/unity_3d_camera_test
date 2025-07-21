using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

public class AxeAnimationHandler : MonoBehaviour
{
    private Animator animator;
    private string currentAnimation = "";
    private float chopAnimationLength = 3.125f;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        StartCoroutine(CheckForChop());
    }

    private void ChangeAnimation(string animation, float crossfade = 0.2f)
    {
        if (currentAnimation != animation)
        {
            currentAnimation = animation;
            animator.CrossFade(animation, crossfade);
        }
    }

    private IEnumerator CheckForChop()
    {
        if (Input.GetMouseButtonDown(0) && currentAnimation != "Chop")
        {
            ChangeAnimation("Chop");
            yield return new WaitForSeconds(chopAnimationLength);
            ChangeAnimation("Idle");
        }
    }
}
