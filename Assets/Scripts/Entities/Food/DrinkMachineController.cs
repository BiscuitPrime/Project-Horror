using Horror.DEBUG;
using Horror.Food;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Horror.Food
{
    public class DrinkMachineController : MonoBehaviour
    {
        [SerializeField,InspectorName("Content of the drink")] private DRINKS_CONTENT _content = DRINKS_CONTENT.WATER; //the content that will be poured by the drink machine, by default water
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
            _rider.GetComponent<DrinksController>().ChangeDrinkState(newDrinkState);
            _rider.GetComponent<DrinksController>().ChangeDrinkContent(_content);
            LogManager.InfoLog(this.GetType(), "Drink POURED to state : " + newDrinkState.ToString());
        }
    }
}

