using UnityEngine;
using UnityEngine.Events;

public class ElevatorButton : MonoBehaviour, IInteractable
{
    public bool IsInteractable { get { return isInteractable; } set { isInteractable = value; } }
    public string InteractMessage { get { return interactMessage; } set { interactMessage = value; } }
    [SerializeField] private ElevatorButtonType buttonType;
    [SerializeField] private Elevator elevator;
    [SerializeField] private BasementElevator basementElevator;
    [SerializeField] private string interactMessage;
    [SerializeField] private bool isInteractable;
    public UnityEvent OnInteract;
    private void Awake()
    {
        IsInteractable = true;
        InteractMessage = interactMessage;
        IsInteractable = isInteractable;
    }

    public void Interact()
    {
        if (OnInteract.GetPersistentEventCount() != 0)
        {
            OnInteract.Invoke();
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
                case ElevatorButtonType.BasementButton:
                    elevator.BasementButtonPress();
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
