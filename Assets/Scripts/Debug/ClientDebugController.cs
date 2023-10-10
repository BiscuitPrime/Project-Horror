using Horror.Clients;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Horror.DEBUG
{
    [RequireComponent(typeof(ClientController))]
    public class ClientDebugController : MonoBehaviour
    {
        [SerializeField] private bool _logOrder;
        [SerializeField] private bool _transmitOrder;

        private void Update()
        {
            if (_logOrder)
            {
                LogCurrentOrder(GetComponent<ClientController>().GetCurrentOrder());
                _logOrder = false;
            }
            if (_transmitOrder)
            {
                GetComponent<ClientController>().TransmitOrderToCounter();
                _transmitOrder = false;
            }
        }

        private void LogCurrentOrder(ClientOrder order)
        {
            LogManager.InfoLog(this.GetType(), "Order " + order.Id);
            foreach(var item in order.FoodItems)
            {
                LogManager.InfoLog(this.GetType(), "Food Item : " + item.name);
            }
            foreach (var item in order.DrinkItems)
            {
                LogManager.InfoLog(this.GetType(), "Drink Item : " + item.name);
            }
        }
    }
}
