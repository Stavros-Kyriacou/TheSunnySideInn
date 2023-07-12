using UnityEngine;
using UnityEngine.Events;

public class AnimatorController : MonoBehaviour
{
    public UnityEvent OnAnimationEnd;

    public void AnimationEnd()
    {
        OnAnimationEnd.Invoke();
    }
}
