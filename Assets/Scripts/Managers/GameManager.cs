using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using Character.Components;
using System.Collections;
using Objects;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;
        public UnityEvent OnStart;
        public PlayerStartLocation startLocation;
        [SerializeField] private List<Transform> startLocations;
        [SerializeField] private Taxi taxi;

        [Header("Menu Loading Screen")]
        [SerializeField] private GameObject loadingScreen;
        [SerializeField] private GameObject mainMenu;
        public bool Luggage_Picked_Up { get; set; }
        public bool Speak_To_Receptionist { get; set; }
        public bool Room_Key_Acquired { get; set; }
        public bool Luggage_Placed { get; set; }
        public bool Inspect_Room_207 { get; set; }
        public bool Room_207_Key_Acquired { get; set; }
        public bool Screwdriver_Acquired { get; set; }
        public bool Page_3_Acquired { get; set; }
        public bool Security_Guard_Gone { get; set; }
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
            MovePlayerStartLocation();
        }
        private void MovePlayerStartLocation()
        {
            if (startLocations == null) return;

            if (SceneManager.GetActiveScene().buildIndex != 1) return;

            switch (startLocation)
            {
                case PlayerStartLocation.OpeningSequence:
                    Player.Instance.RigidBody.MovePosition(startLocations[6].position);
                    taxi.StartDriving();
                    break;
                case PlayerStartLocation.Lobby:
                    Player.Instance.RigidBody.MovePosition(startLocations[1].position);
                    break;
                case PlayerStartLocation.SecurityRoom:
                    Player.Instance.RigidBody.MovePosition(startLocations[2].position);
                    break;
                case PlayerStartLocation.StaffRoom:
                    Player.Instance.RigidBody.MovePosition(startLocations[3].position);
                    break;
                case PlayerStartLocation.Kitchen:
                    Player.Instance.RigidBody.MovePosition(startLocations[4].position);
                    break;
                case PlayerStartLocation.Dumpsters:
                    Player.Instance.RigidBody.MovePosition(startLocations[5].position);
                    break;
                case PlayerStartLocation.Taxi:
                    Player.Instance.RigidBody.MovePosition(startLocations[6].position);
                    break;
                case PlayerStartLocation.Mainenance:
                    Player.Instance.RigidBody.MovePosition(startLocations[7].position);
                    break;
                case PlayerStartLocation.PlayerRoom:
                    Player.Instance.RigidBody.MovePosition(startLocations[8].position);
                    break;
                case PlayerStartLocation.BasementStart:
                    Player.Instance.RigidBody.MovePosition(startLocations[9].position);
                    break;
                case PlayerStartLocation.BasementEnd:
                    Player.Instance.RigidBody.MovePosition(startLocations[10].position);
                    break;
                default:
                    Player.Instance.RigidBody.MovePosition(startLocations[0].position);
                    break;
            }
        }
        private IEnumerator LoadLevelAsync()
        {
            SceneManager.LoadSceneAsync(1);

            yield return null;
        }
        public void PlayGame()
        {
            if (SceneManager.GetActiveScene().buildIndex != 0) return;
            mainMenu.SetActive(false);
            loadingScreen.SetActive(true);

            StartCoroutine(LoadLevelAsync());
        }
        public void QuitGame()
        {
            Application.Quit();
        }
        public void OpenMainMenu()
        {
            if (SceneManager.GetActiveScene().buildIndex == 0) return;
            SceneManager.LoadScene(0);
        }
        public void OpenSettings()
        {

        }
    }
}
public enum PlayerStartLocation
{
    OpeningSequence,
    Lobby,
    SecurityRoom,
    StaffRoom,
    Kitchen,
    Dumpsters,
    Taxi,
    Mainenance,
    PlayerRoom,
    BasementStart,
    BasementEnd
}