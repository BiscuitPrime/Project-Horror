using Horror.DEBUG;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Horror.Food
{
    /// <summary>
    /// This script will be used by mount objects that receive food and will cook them.
    /// The "cooking" is done automatically, and in two phases :
    /// - first routine : raw -> cooked
    /// - second routine : cooked -> overdone
    /// This script SHOULD be modular enough to be adapted to various cooking systems (stoves, milkshakes, coffee machines etc)
    /// </summary>
    public class CookerController : MonoBehaviour
    {
        protected GameObject _rider; //the food riding on the current mount

        /// <summary>
        /// Function that will start the cooking.
        /// Triggered by calls from the mount controller
        /// </summary>
        /// <param name="food"></param>
        public void StartCooking(GameObject food)
        {
            _rider = food;
            if(food.GetComponent<FoodController>() is null )
            {
                LogManager.WarnLog(this.GetType(), "The object riding is not food");
                return;
            }
            else
            {
                FoodController _riderController = food.GetComponent<FoodController>();
                if(_riderController.FoodState==FOOD_STATES.RAW)
                {
                    StartCoroutine(Cook(FOOD_STATES.COOKED));
                }
                else if(_riderController.FoodState == FOOD_STATES.COOKED)
                {
                    StartCoroutine(Cook(FOOD_STATES.OVERDONE));
                }
            }
        }

        public void EndCooking()
        {
            StopAllCoroutines();
        }

        public void Dismount()
        {
            _rider = null;
        }

        /// <summary>
        /// Subroutine that handles the cooking
        /// </summary>
        /// <param name="newFoodState"></param>
        /// <returns></returns>
        protected IEnumerator Cook(FOOD_STATES newFoodState) //TODO : SHOULD HANDLE THE CASE OF FOOD REMOVED BEFORE COOKING IS DONE
        {
            LogManager.InfoLog(this.GetType(),"Cooking food to state : "+newFoodState.ToString());
            yield return new WaitForSeconds(3f);
            _rider.GetComponent<FoodController>().ChangeFoodState(newFoodState);
            LogManager.InfoLog(this.GetType(), "Food COOKED to state : " + newFoodState.ToString());
            StartCoroutine(DelayCooking());
        }

        /// <summary>
        /// This function will be used to simulate the fact that the cooker "continues" to cook, regardless of the state. However, to facilitate gameplay, there's a delay (small) before the cooking continues.
        /// </summary>
        /// <returns></returns>
        protected IEnumerator DelayCooking()
        {
            LogManager.InfoLog(this.GetType(), "Start delay before next cooking");
            yield return new WaitForSeconds(2f);
            StartCooking(_rider);
        }

    }
}
