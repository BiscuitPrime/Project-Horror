using Horror.DEBUG;
using Horror.Food;
using Horror.Interactable;
using Horror.Player;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        private GameObject _counter;
        private Dictionary<Recipe, bool> _orderDict;
        private bool _hasGivenOrder;
        #endregion

        #region SETTERS AND GETTERS
        public ClientOrder GetCurrentOrder() { return _order; }
        public void SetCurrentOrder(ClientOrder order) { _order = order; }
        #endregion

        private void Awake()
        {
            _orderDict = new Dictionary<Recipe, bool>();
            _hasGivenOrder = false;
        }

        private void Start()
        {
            foreach (var item in _order.DrinkItems)
            {
                _orderDict.Add(item, false);
            }
            foreach (var item in _order.FoodItems)
            {
                _orderDict.Add(item, false);
            }
        }

        private string ObtainFormulatedOrder()
        {
            string formulatedOrder = "";
            foreach (var item in _orderDict.Keys)
            {
                formulatedOrder += item.name.ToString() + ", ";
            }
            return formulatedOrder;
        }

        public void InteractionTriggered()
        {
            string formulatedOrder = ObtainFormulatedOrder();
            if (!_hasGivenOrder)
            {
                LogManager.InfoLog(this.GetType(), "Interaction called with the client, displaying order and transmitting it");
                StartCoroutine(PlayerUIController.Instance.DisplayClientText("Here is my order please : "+formulatedOrder.Substring(0, formulatedOrder.Length - 3)));
                TransmitOrderToCounter();
                _hasGivenOrder=true;
            }
            else //if the client has already given the order, he just repeats it 
            {
                LogManager.InfoLog(this.GetType(), "Interaction called with the client, displaying order AGAIN");
                StartCoroutine(PlayerUIController.Instance.DisplayClientText("I already gave you my order... "+'\n'+"But alright, here it is again : " + formulatedOrder.Substring(0, formulatedOrder.Length - 3)));
            }
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
            controller.SetCurrentClient(this);
        }

        /// <summary>
        /// This function is called when a correct item from the order is received by the counter, which will indicate it to the client controller (this)
        /// </summary>
        public void ReceiveCorrectOrderFromCounter(Recipe recipe, GameObject food)
        {
            LogManager.InfoLog(this.GetType(), "Correct order received from counter");
            if (_orderDict.ContainsKey(recipe)) //if the recipe received is within the dictionary => then the recipe is set to "received" by the client, who will ""take it""
            {
                _orderDict[recipe]=true;
                _counter.GetComponent<MountController>().EndRide();
                Destroy(food); //To be changed ?
            }
            TestClientSatisfied();
        }

        /// <summary>
        /// This function is called when a FAILED item (correct item + wrong cooking) from the order is received by the counter, which will indicate it to the client controller (this)
        /// </summary>
        public void ReceiveFailedOrderFromCounter(Recipe recipe, GameObject food)
        {
            LogManager.InfoLog(this.GetType(), "Failed order received from counter");
            if (_orderDict.ContainsKey(recipe)) //if the recipe received is within the dictionary => then the recipe is set to "received" by the client, who will ""take it""
            {
                _orderDict[recipe] = true;
                _counter.GetComponent<MountController>().EndRide();
                Destroy(food); //To be changed ?
            }
            StartCoroutine(PlayerUIController.Instance.DisplayClientText("Huh... Well, that's a bit disappointing..."));
            TestClientSatisfied();
        }

        /// <summary>
        /// This function is called when an incorrect item from the order is received by the counter, which will indicate it to the client controller (this)
        /// </summary>
        public void ReceiveIncorrectOrderFromCounter()
        {
            LogManager.InfoLog(this.GetType(), "Incorrect order received from counter");
            StartCoroutine(PlayerUIController.Instance.DisplayClientText("What is this ?? I don't want it..."));
        }


        /// <summary>
        /// Function will test if the client has been satisfied
        /// </summary>
        private void TestClientSatisfied()
        {
            if(!_orderDict.Values.Contains(false))
            {
                LogManager.InfoLog(this.GetType(), "All recipes have been satisfied : CLIENT IS SATISFIED");
                StartCoroutine(PlayerUIController.Instance.DisplayClientText("Thank you"));
                gameObject.SetActive(false); //TODO : CHANGE THAT
            }
        }
    }
}
