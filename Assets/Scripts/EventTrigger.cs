using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class EventTrigger : MonoBehaviour
{
    public bool EnterTriggerActive { get; set; }
    public bool ExitTriggerActive { get; set; }
    [SerializeField] private float triggerEnterDelay = 0f;
    [SerializeField] private float triggerExitDelay = 0f;
    public UnityEvent OnEnter;
    public UnityEvent OnExit;
    private void Awake()
    {
        EnterTriggerActive = true;
        ExitTriggerActive = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!EnterTriggerActive) return;

        EnterTriggerActive = false;
        StartCoroutine(TriggerEnterDelay(triggerEnterDelay));
    }
    private void OnTriggerExit(Collider other)
    {
        if (!ExitTriggerActive) return;

        ExitTriggerActive = false;
        StartCoroutine(TriggerExitDelay(triggerExitDelay));
    }
    private IEnumerator TriggerEnterDelay(float delayDuration)
    {
        yield return new WaitForSeconds(delayDuration);
        OnEnter.Invoke();
    }
    private IEnumerator TriggerExitDelay(float delayDuration)
    {
        yield return new WaitForSeconds(delayDuration);
        OnExit.Invoke();
    }
}
