using Horror.DEBUG;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Horror.Interactable
{
    public class TrashController : InteractableController
    {
        public override void TriggerInteraction(GameObject playerHand, ref bool isPlayerHoldingSomething)
        {
            base.TriggerInteraction(playerHand, ref isPlayerHoldingSomething);
            if (isPlayerHoldingSomething) 
            {
                if (playerHand.gameObject.GetComponentInChildren<PickableController>() is null)
                {
                    LogManager.InfoLog(this.GetType(), "The player doesn't have any pickable controllers as children right now");
                }
                else
                {
                    GameObject _rider = playerHand.gameObject.GetComponentInChildren<PickableController>().gameObject;
                    Destroy(_rider);
                    isPlayerHoldingSomething = false;
                }
            }
        }
    }
}
