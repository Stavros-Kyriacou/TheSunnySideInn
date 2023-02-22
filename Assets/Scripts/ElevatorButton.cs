using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorButton : MonoBehaviour, IInteractable
{
    [SerializeField] private ElevatorButtonType buttonType;
    [SerializeField] private Elevator elevator;

    public bool IsInteractable { get; set; }
    private void Awake()
    {
        IsInteractable = true;
    }

    public void Interact()
    {
        switch (buttonType)
        {
            case ElevatorButtonType.LowerButton:
                elevator.LowerButtonPress();
                break;
            case ElevatorButtonType.TopButton:
                elevator.TopButtonPress();
                break;
            case ElevatorButtonType.CarriageButton:
                elevator.CarriageButtonPress();
                break;
            default:
                Debug.Log("Button type not assigned");
                break;
        }
    }
}
public enum ElevatorButtonType
{
    LowerButton,
    TopButton,
    CarriageButton
}
