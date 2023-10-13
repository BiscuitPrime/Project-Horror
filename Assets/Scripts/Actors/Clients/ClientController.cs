using Horror.DEBUG;
using Horror.Interactable;
using Horror.Player;
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
        [SerializeField, InspectorName("Scale")] private int _scale;
        private GameObject _counter;
        #endregion

        #region SETTERS AND GETTERS
        public ClientOrder GetCurrentOrder() { return _order; }
        public void SetCurrentOrder(ClientOrder order) { _order = order; }
        #endregion

        private void Awake()
        {
            _scale = 100;
        }

        public void InteractionTriggered()
        {
            LogManager.InfoLog(this.GetType(), "Interaction called with the client, displaying order and transmitting it");
            string formulatedOrder = "";
            foreach(var item in _order.DrinkItems)
            {
                formulatedOrder += item.name.ToString()+", ";
            }
            foreach(var item in _order.FoodItems)
            {
                formulatedOrder += item.name.ToString()+", ";
            }
            StartCoroutine(PlayerUIController.Instance.DisplayClientText("Here is my order please : "+formulatedOrder.Substring(0, formulatedOrder.Length - 3)));
            TransmitOrderToCounter();
        }

        /// <summary>
        /// This function will transmit the client's order to the counter, which will act as the main interface player-client
        /// The player will drop the food/drink on the counter, that is a FoodDrinkAnalyser, and if said food/drink is valid according to the client's order
        /// => analyser transmits success to the ClientController
        /// </summary>
        public void TransmitOrderToCounter()
        {
            LogManager.InfoLog(this.GetType(), "Transmitting order to counter");
            _counter = GameManager.Instance.GetCounter();
            FoodDrinkAnalyserController controller = _counter.GetComponent<FoodDrinkAnalyserController>();
            controller.SetDrinkRecipes(_order.DrinkItems);
            controller.SetFoodRecipes(_order.FoodItems);
        }


    }
}
