using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public bool Luggage_Picked_Up { get; set; }
    public bool Speak_To_Receptionist { get; set; }
    public bool Room_Key_Acquired { get; set; }

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



}
