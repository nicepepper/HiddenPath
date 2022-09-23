using UnityEngine;

namespace CustomGameEvent
{
    public class GameController : MonoBehaviour
    {
        private void Start()
        {
            GameEvent.Current = GameStage.PREPARE;
        }

        public static void PreapareGame()
        {
            GameEvent.Current = GameStage.PREPARE;
        }
    
        public static void StartGame()
        {
            GameEvent.Current = GameStage.START;
        }

        public static void StopGame()
        {
            GameEvent.Current = GameStage.STOP;
        }
    
        public static void QuitGame()
        {
            GameEvent.Current = GameStage.QUIT;
        }

        public static void WinGame()
        {
            GameEvent.Current = GameStage.WIN;
        }

        public static void LoseGame()
        {
            GameEvent.Current = GameStage.LOSE;
        }
    }
}
