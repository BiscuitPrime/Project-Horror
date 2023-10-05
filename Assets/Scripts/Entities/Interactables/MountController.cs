using Horror.DEBUG;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions;

namespace Horror.Interactable
{
    /// <summary>
    /// Script used by "mount" objects, that can take another object upon them (ex: like a stove)
    /// </summary>
    public class MountController : InteractableController
    {
        [SerializeField, InspectorName("Ride position")] private GameObject _ridePosition;
        private GameObject _rider; //object that is currently riding the mount associated to this script
        
        private void Start()
        {
            Assert.IsNotNull(_ridePosition);
            _rider = null;
        }

        public override void TriggerInteraction(GameObject playerHand, ref bool isPlayerHoldingSomething)
        {
            base.TriggerInteraction(playerHand, ref isPlayerHoldingSomething);
            if (isPlayerHoldingSomething && _rider is null /*TODO : TEST IF SUITABLE TO MOUNT*/ ) //if the player is holding something, and that thing is suitable to ride the mount, then it is dropped on the mount
            {
                if(playerHand.gameObject.GetComponentInChildren<PickableController>() is null)
                {
                    LogManager.ErrorLog(this.GetType(), "The player doesn't have any pickable controllers as children right now");
                }
                else
                {
                    _rider = playerHand.gameObject.GetComponentInChildren<PickableController>().gameObject;
                    _rider.transform.position = _ridePosition.transform.position;
                    _rider.transform.rotation = _ridePosition.transform.rotation;
                    _rider.transform.parent = this.transform;
                    isPlayerHoldingSomething = false;
                    _rider.GetComponent<PickableController>().IsRiding = true;
                    _rider.GetComponent<PickableController>().Mount = this.gameObject;
                    _rider.GetComponent<InteractableController>().SetInteractableStatus(true); //TODO : MAYBE MOVE THIS SOMEWHERE ELSE => AFTER THE ACTION/SUBROUTINE THAT HANDLES THE OBJECT ONCE ON THE MOUNT
                }
            }
        }

        /// <summary>
        /// Called by the rider when they are picked up, this function cuts the existing link mount/rider established before
        /// </summary>
        public void EndRide()
        {
            _rider = null;
        }
    }
}
