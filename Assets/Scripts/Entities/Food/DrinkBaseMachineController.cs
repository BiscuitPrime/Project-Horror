using Horror.DEBUG;
using Horror.Food;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Horror.Food
{
    /// <summary>
    /// Script used by machines that serve the base of a drink
    /// </summary>
    public class DrinkBaseMachineController : MonoBehaviour
    {
        [SerializeField,InspectorName("Base of the drink")] private DRINKS_BASE _content = DRINKS_BASE.WATER; //the content that will be poured by the drink machine, by default water
        protected GameObject _rider; //the drink riding on the current mount

        /// <summary>
        /// Function that will start the pouring of the drink 
        /// Triggered by calls from the mount controller
        /// </summary>
        /// <param name="drink"></param>
        public void StartPouring(GameObject drink)
        {
            _rider = drink;
            if (drink.GetComponent<DrinksController>() is null)
            {
                LogManager.WarnLog(this.GetType(), "The object riding is not drink");
                return;
            }
            else
            {
                DrinksController _riderController = drink.GetComponent<DrinksController>();
                if (_riderController.DrinkState == DRINKS_STATES.EMPTY)
                {
                    StartCoroutine(Pour(DRINKS_STATES.FILLED));
                }
            }
        }

        public void EndPouring()
        {
            StopAllCoroutines();
        }

        public void Dismount()
        {
            _rider = null;
        }

        /// <summary>
        /// Subroutine that handles the pouring
        /// </summary>
        /// <param name="newDrinkState"></param>
        /// <returns></returns>
        protected IEnumerator Pour(DRINKS_STATES newDrinkState) //TODO : SHOULD HANDLE THE CASE OF FOOD REMOVED BEFORE COOKING IS DONE
        {
            LogManager.InfoLog(this.GetType(), "Pouring drink to state : " + newDrinkState.ToString());
            yield return new WaitForSeconds(3f);
            DrinksController drinksController = _rider.GetComponent<DrinksController>();
            drinksController.ChangeDrinkState(newDrinkState);
            drinksController.ChangeDrinkBase(_content);
            _rider.GetComponent<DrinkBasePickerController>().ChangeDrinkBase(_content);
            LogManager.InfoLog(this.GetType(), "Drink POURED to state : " + newDrinkState.ToString());
        }
    }
}

