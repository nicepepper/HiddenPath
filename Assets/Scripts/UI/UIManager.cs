using CustomGameEvent;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private GameObject _uiMainMenu;
        [SerializeField] private GameObject _uiGameplay;
        [SerializeField] private GameObject _uiWin;
        [SerializeField] private GameObject _uiLose;
        [SerializeField] private GameObject _uiEnd;
        [SerializeField] private Button _btnStart;
        [SerializeField] private Button _btnQuit;
        [SerializeField] private Button _btnToMenu;
        [SerializeField] private Text _endText;

        private void Awake()
        {
            GameEvent.OnChangedStage += OnChangedGameStage;
            _btnStart.onClick.AddListener(GameController.StartGame);
            _btnQuit.onClick.AddListener(GameController.QuitGame);
            _btnToMenu.onClick.AddListener(GameController.PreapareGame);
        }

        private void OnDestroy()
        {
            GameEvent.OnChangedStage -= OnChangedGameStage;
        }

        private void OnChangedGameStage()
        {
            var state = GameEvent.Current;
            _uiMainMenu.SetActive(state == GameStage.PREPARE);
            _uiGameplay.SetActive(state == GameStage.START);
            if (state == GameStage.WIN || state == GameStage.LOSE)
            {
                _uiEnd.SetActive(true);
                _endText.text = (state == GameStage.WIN ? "Victory!" : "You Lose :(");
            }
            else
            {
                _uiEnd.SetActive(false);
            }
        }
    }
}


