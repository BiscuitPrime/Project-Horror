using Horror.DEBUG;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Horror.Interactable
{
    /// <summary>
    /// Class used by entities that are interactable with. Allows the player to pick them or 
    /// </summary>
    public class InteractableController : MonoBehaviour
    {
        protected bool _isInteractable;
        public bool GetInteractableStatus() { return _isInteractable; }
        public void SetInteractableStatus(bool status) { _isInteractable = status; }

        private void Awake()
        {
            _isInteractable = true;
        }

        /// <summary>
        /// Function called when the interaction is effectively triggered by the player
        /// </summary>
        public virtual void TriggerInteraction(GameObject playerHand, ref bool isPlayerHoldingSomething)
        {
            LogManager.InfoLog(this.GetType(), gameObject.name + " interacted with !");
        }
    }
}
