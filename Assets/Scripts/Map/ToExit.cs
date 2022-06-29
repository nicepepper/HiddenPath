using CustomGameEvent;
using UnityEngine;

namespace Map
{
    public class ToExit : MonoBehaviour, IInteractable
    {
        public void Act()
        {
            GameEvent.Current = GameStage.WIN;
        }
    }
}
