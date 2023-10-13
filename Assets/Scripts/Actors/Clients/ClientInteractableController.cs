using Horror.DEBUG;
using Horror.Interactable;
using Horror.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Horror.Clients
{
    [RequireComponent(typeof(ClientController))]
    public class ClientInteractableController : InteractableController
    {
        public override void TriggerInteraction(GameObject playerHand, ref bool isPlayerHoldingSomething)
        {
            base.TriggerInteraction(playerHand, ref isPlayerHoldingSomething);
            GetComponent<ClientController>().InteractionTriggered();
        }
    }
}
