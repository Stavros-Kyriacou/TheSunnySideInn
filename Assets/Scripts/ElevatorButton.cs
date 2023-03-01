using UnityEngine;
using UnityEngine.Events;

public class ElevatorButton : MonoBehaviour, IInteractable
{
    [SerializeField] private ElevatorButtonType buttonType;
    [SerializeField] private Elevator elevator;
    [SerializeField] private BasementElevator basementElevator;
    public UnityEvent OnInteract;

    public bool IsInteractable { get; set; }
    public string InteractMessage { get; set; }
    [SerializeField] private string interactMessage;
    private void Awake()
    {
        IsInteractable = true;
        InteractMessage = interactMessage;
    }

    public void Interact()
    {  

        if (OnInteract.GetPersistentEventCount() != 0) {
            
            OnInteract.Invoke();
            return;
        }

        if (elevator != null)
        {
            switch (buttonType)
            {
                case ElevatorButtonType.GroundButton:
                    elevator.GroundButtonPress();
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
            return;
        }

        if (basementElevator != null)
        {
            switch (buttonType)
            {
                case ElevatorButtonType.GroundButton:
                    basementElevator.GroundButtonPress();
                    break;
                case ElevatorButtonType.TopButton:
                    basementElevator.TopButtonPress();
                    break;
                case ElevatorButtonType.BasementButton:
                    basementElevator.BasementButtonPress();
                    break;
                case ElevatorButtonType.CarriageButton:
                    basementElevator.CarriageButtonPress();
                    break;
                default:
                    Debug.Log("Button type not assigned");
                    break;
            }
            return;
        }
    }
}
public enum ElevatorButtonType
{
    GroundButton,
    TopButton,
    BasementButton,
    CarriageButton
}
