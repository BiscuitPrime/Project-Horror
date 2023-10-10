using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Horror.Clients
{
    /// <summary>
    /// This will be the basic script used by the Clients, that will control their basic behaviors.
    /// </summary>
    public class ClientController : MonoBehaviour
    {
        #region VARIABLES
        [SerializeField, InspectorName("Order")] private ClientOrder _order;
        #endregion

        public ClientOrder GetCurrentOrder() { return _order; }
        public void SetCurrentOrder(ClientOrder order) { _order = order; }

        public void RequestOrder()
        {

        }

    }
}
