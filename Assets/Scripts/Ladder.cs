using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Ladder : MonoBehaviour, IInteractable
{
    public UnityEvent OnInteract;
    [SerializeField] private Transform ladderTop;
    [SerializeField] private Transform ladderBottom;
    [SerializeField] private string interactMessage;
    [SerializeField] private bool isInteractable;
    public bool IsInteractable { get { return isInteractable; } set { isInteractable = value; } }
    public string InteractMessage { get { return interactMessage; } set { interactMessage = value; } }
    private void Awake()
    {
        IsInteractable = isInteractable;
        InteractMessage = interactMessage;
    }
    public void Interact()
    {
        StartCoroutine("ClimbLadder");
        OnInteract.Invoke();
    }
    IEnumerator ClimbLadder()
    {
        var rb = Player.Instance.GetComponent<Rigidbody>();
        var boxCollider = GetComponent<BoxCollider>();
        boxCollider.enabled = false;
        Player.Instance.MovementEnabled = false;

        Vector3 startPos = Player.Instance.transform.position;
        Vector3 targetPos = new Vector3(ladderBottom.position.x, Player.Instance.transform.position.y, ladderBottom.position.z);
        var animationTime = Vector3.Distance(startPos, targetPos) / 2.1f;
        float elapsedTime = 0f;

        while (elapsedTime < animationTime)
        {
            rb.MovePosition(Vector3.Lerp(startPos, targetPos, elapsedTime / animationTime));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        boxCollider.enabled = true;
        Player.Instance.MovementEnabled = true;
    }
}
