using Horror.DEBUG;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Horror.Interactable
{
    /// <summary>
    /// This class is used by the spawners that will spawn the pickable items that the player can pick and use, such as food etc...
    /// </summary>
    public class PickableSpawnerController : InteractableController
    {
        [SerializeField, InspectorName("Pickable Object")] private GameObject _spawnedObject;

        private void Start()
        {
            Assert.IsNotNull(_spawnedObject.GetComponent<PickableController>());
        }

        public override void TriggerInteraction(GameObject playerHand, ref bool isPlayerHoldingSomething)
        {
            base.TriggerInteraction(playerHand, ref isPlayerHoldingSomething);
            if (!isPlayerHoldingSomething) //if the player is already holding something, we do not take the object again
            {
                LogManager.InfoLog(this.GetType(), "Taking object in player hand from pile !");
                isPlayerHoldingSomething = true;
                GameObject pickedObject = Instantiate(_spawnedObject);
                pickedObject.gameObject.transform.position = playerHand.transform.position;
                pickedObject.gameObject.transform.rotation = playerHand.transform.rotation;
                pickedObject.gameObject.transform.parent = playerHand.transform;
            }
        }
    }
}
