using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using Character.Components;
using System.Collections;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;
        public UnityEvent OnStart;
        public bool PlayOpeningSequence;
        [SerializeField] private bool usePresetStartLocation;
        [SerializeField] PlayerStartLocation startLocation;
        [SerializeField] List<Transform> startLocations;

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
            if (!usePresetStartLocation || PlayOpeningSequence) return;

            if (startLocations == null) return;

            Rigidbody rb = Player.Instance.RigidBody;

            switch (startLocation)
            {
                case PlayerStartLocation.Zero:
                    rb.MovePosition(startLocations[0].position);
                    break;
                case PlayerStartLocation.Lobby:
                    rb.MovePosition(startLocations[1].position);
                    break;
                case PlayerStartLocation.SecurityRoom:
                    rb.MovePosition(startLocations[2].position);
                    break;
                case PlayerStartLocation.StaffRoom:
                    rb.MovePosition(startLocations[3].position);
                    break;
                case PlayerStartLocation.Kitchen:
                    rb.MovePosition(startLocations[4].position);
                    break;
                case PlayerStartLocation.Dumpsters:
                    rb.MovePosition(startLocations[5].position);
                    break;
                case PlayerStartLocation.Taxi:
                    rb.MovePosition(startLocations[6].position);
                    break;
                case PlayerStartLocation.Mainenance:
                    rb.MovePosition(startLocations[7].position);
                    break;
                case PlayerStartLocation.PlayerRoom:
                    rb.MovePosition(startLocations[8].position);
                    break;
                case PlayerStartLocation.BasementStart:
                    rb.MovePosition(startLocations[9].position);
                    break;
                case PlayerStartLocation.BasementEnd:
                    rb.MovePosition(startLocations[10].position);
                    break;
                default:
                    rb.MovePosition(startLocations[0].position);
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
        public void OpenSettings()
        {

        }
    }
}
public enum PlayerStartLocation
{
    Zero,
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