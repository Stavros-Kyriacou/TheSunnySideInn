using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public UnityEvent OnStart;

    public bool Luggage_Picked_Up { get; set; }
    public bool Speak_To_Receptionist { get; set; }
    public bool Room_Key_Acquired { get; set; }
    public bool Luggage_Placed { get; set; }
    public bool Inspect_Room_207 { get; set; }
    public bool Room_207_Key_Acquired { get; set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }
    private void Start()
    {
        OnStart.Invoke();
    }



}
