using Horror.DEBUG;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Horror.Interactable
{
    /// <summary>
    /// Class used by objects that can be picked up by the player (such as food).
    /// </summary>
    public class PickableController : InteractableController
    {
        public GameObject Mount; //the mount that the object is currently riding on
        public bool IsRiding; //boolean that indicates wether or not the object is currently riding something

        public override void TriggerInteraction(GameObject playerHand, ref bool isPlayerHoldingSomething)
        {
            base.TriggerInteraction(playerHand, ref isPlayerHoldingSomething);
            if (!isPlayerHoldingSomething) //if the player is already holding something, we do not take the object again
            {
                LogManager.InfoLog(this.GetType(),"Taking object in player hand !");
                if(IsRiding) //If the object was riding a mount, since it's the player that takes it, it informs the mount that it is being taken (and as such, the mount ends the ride)
                {
                    Mount.GetComponent<MountController>().EndRide();
                }
                isPlayerHoldingSomething = true;
                _isInteractable = false;
                gameObject.transform.position = playerHand.transform.position;
                gameObject.transform.rotation = playerHand.transform.rotation;
                gameObject.transform.parent = playerHand.transform;
            }
        }
    }
}
