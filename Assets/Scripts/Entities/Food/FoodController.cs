using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Horror.Food
{
    public enum FOOD_STATES
    {
        RAW,
        COOKED,
        OVERDONE
    }

    /// <summary>
    /// This is the script used by food that will be cooked. Foods have three states :
    /// - Raw : unprepared
    /// - Cooked : Prepared
    /// - Overdone : wasted, must be thrown out usually
    /// The states are changed by external actors
    /// </summary>
    public class FoodController : MonoBehaviour
    {
        #region VARIABLES
        [SerializeField, InspectorName("Raw")] private GameObject _rawState;
        [SerializeField, InspectorName("Cooked")] private GameObject _cookedState;
        [SerializeField, InspectorName("Overdone")] private GameObject _overdoneState;

        /// <summary>
        /// State of the food. Can be changed through ChangeFoodState(FOOD_STATES)
        /// </summary>
        public FOOD_STATES FoodState { get; private set; } //the setter of the food state should only be done through the ChangeFoodState function
        #endregion


        protected void Awake()
        {
            Assert.IsNotNull(_rawState);
            Assert.IsNotNull(_cookedState);
            Assert.IsNotNull(_overdoneState);
            ChangeFoodState(FOOD_STATES.RAW);
        }

        /// <summary>
        /// Function that will change the food's state and call the appropriate function handling the visual change of the food
        /// </summary>
        /// <param name="newState">new state that the food should take</param>
        public void ChangeFoodState(FOOD_STATES newState)
        {
            FoodState = newState;
            ChangeVisualFoodState();
        }

        public void ChangeVisualFoodState()
        {
            switch(FoodState) //Could probably be written MUCH better than right now, but it will do
            {
                case FOOD_STATES.RAW:
                    _rawState.SetActive(true);
                    _cookedState.SetActive(false);
                    _overdoneState.SetActive(false);
                    break;
                case FOOD_STATES.COOKED:
                    _rawState.SetActive(false);
                    _cookedState.SetActive(true);
                    _overdoneState.SetActive(false);
                    break;
                case FOOD_STATES.OVERDONE:
                    _rawState.SetActive(false);
                    _cookedState.SetActive(false);
                    _overdoneState.SetActive(true);
                    break;
            }
        }
    }
}
