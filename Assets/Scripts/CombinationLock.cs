using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombinationLock : MonoBehaviour, IInteractable
{
    [SerializeField] private int[] combination;
    [SerializeField] private Transform[] rings;
    [SerializeField] private Material ringMaterial;
    [SerializeField] private Material ringSelectedMaterial;
    [SerializeField] private Transform lockTransform;
    [SerializeField] private Transform lockedLocation;
    [SerializeField] private Transform unlockedLocation;
    [SerializeField] private float unlockAnimationDuration;

    private Transform selectedRing;
    private int currentIndex;
    private int[] currentCombination;
    private Lock_Controller lockController;
    private void Awake()
    {
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
                    selectedRing.transform.rotation = Quaternion.identity;
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
                    selectedRing.transform.rotation = Quaternion.identity;
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
            StartCoroutine(OpenLock());
        }
    }

    IEnumerator OpenLock()
    {
        Vector3 startPos = lockTransform.transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < unlockAnimationDuration)
        {
            lockTransform.transform.position = Vector3.Lerp(startPos, unlockedLocation.transform.position, elapsedTime / unlockAnimationDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        lockTransform.transform.position = unlockedLocation.transform.position;
    }

    public void Interact()
    {
        ChangeRing(currentIndex);
        Player.Instance.MovementEnabled = false;
        Player.Instance.InteractionEnabled = false;
        lockController.Enable();
    }
    public void StopInteraction()
    {
        ChangeRing(-1);
        Player.Instance.MovementEnabled = true;
        Player.Instance.InteractionEnabled = true;
        lockController.Disable();
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
