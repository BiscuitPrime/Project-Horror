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

        /// <summary>
        /// This function will transmit the client's order to the counter, which will act as the main interface player-client
        /// The player will drop the food/drink on the counter, that is a FoodDrinkAnalyser, and if said food/drink is valid according to the client's order
        /// => analyser transmits success to the ClientController
        /// </summary>
        public void TransmitOrderToCounter()
        {

        }

    }
}
