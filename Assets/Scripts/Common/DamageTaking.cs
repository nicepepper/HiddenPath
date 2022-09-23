using CustomGameEvent;
using UnityEngine;

public class DamageTaking : MonoBehaviour
{
   [SerializeField] private int _hitPoint = 1;
   [SerializeField] private bool _gameOverOnDestoyed = false;

   public void TakeDamage(int amount)
   {
      _hitPoint -= amount;
      if (_hitPoint <= 0)
      {
         if (_gameOverOnDestoyed)
         {
            GameEvent.Current = GameStage.LOSE;
         }
      }
   }
}
