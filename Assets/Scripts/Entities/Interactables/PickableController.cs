using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Horror.Interactable
{
    public class PickableController : InteractableController
    {
        public override void TriggerInteraction(GameObject playerHand)
        {
            base.TriggerInteraction(playerHand);
            gameObject.transform.position= playerHand.transform.position;
            gameObject.transform.rotation= playerHand.transform.rotation;
            gameObject.transform.parent=playerHand.transform;
        }
    }
}
