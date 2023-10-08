using Horror.DEBUG;
using Horror.Food;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Horror.Food
{
    /// <summary>
    /// Script used by machines that serve the content of a drink
    /// </summary>
    public class DrinkContentMachineController : MonoBehaviour
    {
        [SerializeField,InspectorName("Content of the drink")] private DRINKS_CONTENT _content = DRINKS_CONTENT.CHOCOLATE_CHIPS; //the content that will be poured by the drink machine, by default Chocolate_chips
        protected GameObject _rider; //the drink riding on the current mount

        /// <summary>
        /// Function that will start the pouring of the content to the drink 
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
                StartCoroutine(PourContent());
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
        /// <returns></returns>
        protected IEnumerator PourContent()
        {
            LogManager.InfoLog(this.GetType(), "Pouring content : " + _content.ToString());
            yield return new WaitForSeconds(3f);
            _rider.GetComponent<DrinksController>().AddContentToDrink(_content);
        }
    }
}

