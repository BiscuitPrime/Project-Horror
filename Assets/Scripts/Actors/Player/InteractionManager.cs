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

        private void Awake()
        {
            Assert.IsNotNull(_playerHand);
        }

        public void Interact(Camera cam)
        {
            Ray ray = new Ray(cam.transform.position, cam.transform.forward);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100f))
            {
                LogManager.InfoLog(this.GetType(), "calling interaction with "+hit.transform.name);
                if(hit.transform.gameObject.GetComponent<InteractableController>() != null && hit.transform.gameObject.GetComponent<InteractableController>().GetInteractableStatus())
                {
                    hit.transform.gameObject.GetComponent<InteractableController>().TriggerInteraction(_playerHand);
                }
            }
        }
    }
}
