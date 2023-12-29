using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DirectionDetection : MonoBehaviour
{
    public UnityEvent OnTrigger;
    [SerializeField] private Transform playerCam;
    [SerializeField] private float rayDistance;
    [SerializeField] private LayerMask mask;
    void Awake()
    {
        this.enabled = false;
    }
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(playerCam.transform.position, playerCam.transform.TransformDirection(Vector3.forward), out hit, rayDistance, mask))
        {
            OnTrigger.Invoke();
            this.enabled = false;
        }
    }
}
