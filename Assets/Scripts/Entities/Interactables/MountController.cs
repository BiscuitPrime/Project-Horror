using Horror.DEBUG;
using Horror.Food;
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
        public GameObject GetRider() { return _rider; }
        
        private void Start()
        {
            Assert.IsNotNull(_ridePosition);
            _rider = null;
        }

        /// <summary>
        /// The main function of the mount controller.
        /// Basically, upon being clicked by the player, it will detect wether or not the player is holding something.
        /// If he is, then we correctly place the object on the mount, then trigger the appropriate behaviour if the mount is a cooker, analyser, drink machine etc
        /// </summary>
        /// <param name="playerHand">the player's hand, contains as a child the current object held by the player</param>
        /// <param name="isPlayerHoldingSomething">the boolean passed as a REFERENCE from the player's script, that indicates to it wether or not the player holds something</param>
        public override void TriggerInteraction(GameObject playerHand, ref bool isPlayerHoldingSomething)
        {
            base.TriggerInteraction(playerHand, ref isPlayerHoldingSomething);
            if (isPlayerHoldingSomething && _rider is null && playerHand.gameObject.GetComponentInChildren<PickableController>().gameObject != gameObject/*TODO : TEST IF SUITABLE TO MOUNT*/ ) //if the player is holding something, and that thing is suitable to ride the mount, then it is dropped on the mount
            {
                if(playerHand.gameObject.GetComponentInChildren<PickableController>() is null)
                {
                    LogManager.InfoLog(this.GetType(), "The player doesn't have any pickable controllers as children right now");
                }
                else
                {
                    LogManager.InfoLog(this.GetType(), "Player mounting object " + playerHand.gameObject.GetComponentInChildren<PickableController>().gameObject + " on mount : " + this.gameObject);
                    _rider = playerHand.gameObject.GetComponentInChildren<PickableController>().gameObject;
                    _rider.transform.position = _ridePosition.transform.position;
                    _rider.transform.rotation = _ridePosition.transform.rotation;
                    _rider.transform.parent = this.transform;
                    isPlayerHoldingSomething = false;
                    _rider.GetComponent<PickableController>().IsRiding = true;
                    _rider.GetComponent<PickableController>().Mount = this.gameObject;
                    _rider.GetComponent<InteractableController>().SetInteractableStatus(true); //TODO : MAYBE MOVE THIS SOMEWHERE ELSE => AFTER THE ACTION/SUBROUTINE THAT HANDLES THE OBJECT ONCE ON THE MOUNT
                    

                    //TODO : THE FOLLOWING BULLSHIT COULD BE WAAAAY BETTER HANDLED => PERHAPS WITH TEMPLATES OR INHERITED FUNCTIONS ?
                    //   => TO INVESTIGATE
                    if(GetComponent<CookerController>() !=null)
                    {
                        LogManager.InfoLog(this.GetType(), "Mount is a cooker : starting cooking");
                        GetComponent<CookerController>().StartCooking(_rider);
                    }

                    if(GetComponent<FoodDrinkAnalyserController>() !=null)
                    {
                        LogManager.InfoLog(this.GetType(), "Mount is an analyser : starting analyse");
                        GetComponent<FoodDrinkAnalyserController>().StartAnalyse(_rider);
                    }

                    if(GetComponent<DrinkBaseMachineController>() !=null)
                    {
                        LogManager.InfoLog(this.GetType(), "Mount is an drink machine : starting pouring");
                        GetComponent<DrinkBaseMachineController>().StartPouring(_rider);
                    }

                    if (GetComponent<DrinkContentMachineController>() != null)
                    {
                        LogManager.InfoLog(this.GetType(), "Mount is an drink content machine : starting pouring");
                        GetComponent<DrinkContentMachineController>().StartPouring(_rider);
                    }
                }
            }
        }

        /// <summary>
        /// Called by the rider when they are picked up, this function cuts the existing link mount/rider established before
        /// </summary>
        public void EndRide()
        {
            _rider = null;
            if (GetComponent<CookerController>() != null)
            {
                GetComponent<CookerController>().EndCooking();
                GetComponent<CookerController>().Dismount();
            }
            if(GetComponent<DrinkBaseMachineController>() !=null)
            {
                GetComponent<DrinkBaseMachineController>().EndPouring();
                GetComponent<DrinkBaseMachineController>().Dismount();
            }
            if (GetComponent<DrinkContentMachineController>() != null)
            {
                GetComponent<DrinkContentMachineController>().EndPouring();
                GetComponent<DrinkContentMachineController>().Dismount();
            }
            if (GetComponent<FoodDrinkAnalyserController>() != null)
            {
                GetComponent<FoodDrinkAnalyserController>().EndAnalyse();
            }
        }
    }
}
