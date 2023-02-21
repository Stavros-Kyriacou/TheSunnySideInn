using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class CombinationLock : MonoBehaviour, IInteractable
{
    public bool IsLocked
    {
        get
        {
            return isLocked;
        }
        private set
        {
            isLocked = value;
        }
    }

    public bool IsInteractable { get; set; }

    [SerializeField] private int[] combination;
    [SerializeField] private Transform[] rings;
    [SerializeField] private Material ringMaterial;
    [SerializeField] private Material ringSelectedMaterial;
    [SerializeField] private Transform lockBodyTransform;
    [SerializeField] private Transform lockedLocation;
    [SerializeField] private Transform unlockedLocation;
    [SerializeField] private float unlockAnimationDuration;
    [SerializeField] private MoveCamera cameraHolder;
    [SerializeField] private Transform cameraPosition;
    private Animator animator;
    public UnityEvent OnUnlocked;
    private Transform selectedRing;
    private int currentIndex;
    private int[] currentCombination;
    private Lock_Controller lockController;
    private bool isLocked = true;
    private void Awake()
    {
        IsLocked = true;
        IsInteractable = true;
        animator = GetComponent<Animator>();

        lockController = new Lock_Controller();
        lockController.Player_Map.Up.performed += x => SelectRing(RingSelection.UP);
        lockController.Player_Map.Down.performed += x => SelectRing(RingSelection.DOWN);
        lockController.Player_Map.Left.performed += x => RotateRing(RingRotation.LEFT);
        lockController.Player_Map.Right.performed += x => RotateRing(RingRotation.RIGHT);
        lockController.Player_Map.Exit.performed += x => StopInteraction();
    }
    private void Start()
    {
        ChangeRing(-1);
        currentCombination = new int[] { 0, 0, 0 };
    }

    private void ChangeRing(int ringIndex)
    {
        if (ringIndex == -1)
        {
            currentIndex = 0;
            selectedRing = rings[0];
            foreach (var ring in rings)
            {
                var mat = ring.GetComponent<MeshRenderer>();

                if (mat == null) return;

                mat.material = ringMaterial;
            }
            return;
        }

        currentIndex = ringIndex;
        selectedRing = rings[ringIndex];

        foreach (var ring in rings)
        {
            var mat = ring.GetComponent<MeshRenderer>();

            if (mat == null) return;

            mat.material = ringMaterial;
        }

        selectedRing.GetComponent<MeshRenderer>().material = ringSelectedMaterial;
    }

    private void SelectRing(RingSelection selection)
    {
        switch (selection)
        {
            case RingSelection.UP:
                if (currentIndex > 0)
                {
                    currentIndex--;
                    ChangeRing(currentIndex);
                }
                break;
            case RingSelection.DOWN:
                if (currentIndex < 2)
                {
                    currentIndex++;
                    ChangeRing(currentIndex);
                }
                break;
            default:
                break;
        }
        CheckCombination();
    }
    private void RotateRing(RingRotation direction)
    {
        switch (direction)
        {
            case RingRotation.LEFT:
                {
                    currentCombination[currentIndex]++;
                    if (currentCombination[currentIndex] > 9)
                    {
                        currentCombination[currentIndex] = 0;
                    }
                    selectedRing.transform.localRotation = Quaternion.identity;
                    selectedRing.transform.Rotate(0, 360 - (currentCombination[currentIndex] * 36), 0);
                }
                break;
            case RingRotation.RIGHT:
                {
                    currentCombination[currentIndex]--;
                    if (currentCombination[currentIndex] < 0)
                    {
                        currentCombination[currentIndex] = 9;
                    }
                    selectedRing.transform.localRotation = Quaternion.identity;
                    selectedRing.transform.Rotate(0, 360 - (currentCombination[currentIndex] * 36), 0);
                }
                break;
            default:
                break;
        }
        CheckCombination();
    }
    private void CheckCombination()
    {
        if (currentCombination[0] == combination[0] &&
        currentCombination[1] == combination[1] &&
        currentCombination[2] == combination[2])
        {
            IsInteractable = false;
            StartCoroutine(OpenLock());
        }
    }

    IEnumerator OpenLock()
    {
        Vector3 startPos = lockBodyTransform.transform.position;
        float elapsedTime = 0f;
        yield return new WaitForSeconds(0.4f);
        while (elapsedTime < unlockAnimationDuration)
        {
            lockBodyTransform.transform.position = Vector3.Lerp(startPos, unlockedLocation.transform.position, elapsedTime / unlockAnimationDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        lockBodyTransform.transform.position = unlockedLocation.transform.position;

        if (animator != null)
        {
            animator.SetTrigger("OnUnlocked");
        }
        else
        {
            Debug.Log("Animator not assigned");
        }
    }
    public void Unlock()
    {
        Debug.Log("unlock event called");
        this.IsLocked = false;
        OnUnlocked.Invoke();
        StopInteraction();
    }

    public void Interact()
    {
        //Disable player and camera movement
        Player.Instance.MovementEnabled = false;
        Player.Instance.InteractionEnabled = false;

        //Move camera to look position and rotate towards the lock
        cameraHolder.CameraState = CameraState.UNLOCK_DOOR;
        var cameraLookDirection = gameObject.transform.position - cameraPosition.position;
        cameraHolder.transform.localRotation = Quaternion.LookRotation(cameraLookDirection);

        //Enable combo lock controls
        lockController.Enable();

        //Select ring
        ChangeRing(currentIndex);
    }
    public void StopInteraction()
    {
        ChangeRing(-1);
        lockController.Disable();

        cameraHolder.CameraState = CameraState.PLAYER;

        Player.Instance.MovementEnabled = true;
        Player.Instance.InteractionEnabled = true;
    }
}
public enum RingSelection
{
    UP,
    DOWN
}
public enum RingRotation
{
    LEFT,
    RIGHT
}
