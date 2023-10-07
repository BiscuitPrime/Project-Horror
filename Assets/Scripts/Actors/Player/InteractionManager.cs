using Horror.DEBUG;
using Horror.Interactable;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Horror.Player
{
    public class InteractionManager : MonoBehaviour
    {
        [SerializeField] private GameObject _playerHand;
        private bool _isHoldingSomething;

        private void Awake()
        {
            Assert.IsNotNull(_playerHand);
        }

        public void Interact(Camera cam)
        {
            Ray ray = new Ray(cam.transform.position, cam.transform.forward);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 2f))
            {
                LogManager.InfoLog(this.GetType(), "calling interaction with "+hit.transform.name);
                if(hit.transform.gameObject.GetComponent<InteractableController>() != null && hit.transform.gameObject.GetComponent<InteractableController>().GetInteractableStatus())
                {
                    InteractableController[] controllers = hit.transform.gameObject.GetComponents<InteractableController>(); //We are going to call one after the other all the interactable controllers present on the object
                    //Indeed, mountable AND pickable objects existing, we need to handle all their cases, since the classes inheriting from InteractableController SHOULD handle the checks to ensure that the correct operation is done corresponding to the player's situation
                    foreach(InteractableController controller in controllers)
                    {
                        controller.TriggerInteraction(_playerHand, ref _isHoldingSomething);
                    }
                }
            }
        }
    }
}
