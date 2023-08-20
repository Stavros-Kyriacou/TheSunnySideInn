using UnityEngine;
using UnityEngine.Events;

public class AnimatorController : MonoBehaviour
{
    public UnityEvent OnAnimationEnd;
    private Animator animator;
    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void SetAnimationTrigger(string trigger)
    {
        animator.SetTrigger(trigger);
    }
    public void AnimationEnd()
    {
        OnAnimationEnd.Invoke();
    }
}
