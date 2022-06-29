using System;
using CustomGameEvent;
using UnityEngine;

public class InteractionPlayer : MonoBehaviour
{
   private void OnTriggerEnter(Collider collider)
   {
      IInteractable interaction = collider.gameObject.GetComponent<IInteractable>();

      if (interaction != null)
      {
         interaction.Act();
      }
   }
}
