using Horror.DEBUG;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Horror.Food
{
    public enum DRINKS_STATES
    {
        EMPTY,
        FILLED
    }


    /// <summary>
    /// Contents that can be added to a drink, when a base has been added
    /// </summary>
    public enum DRINKS_CONTENT
    {
        CHOCOLATE_CHIPS,
        CREAM
    }

    /// <summary>
    /// Script used by the drinks, that operate slightly differently than foods : they can only have two states : empty or filled.
    /// </summary>
    [RequireComponent(typeof(DrinkBasePickerController))]
    public class DrinksController : MonoBehaviour //TODO : REWORK THIS AS TO MAKE IT INHERIT FROM A SINGLE CLASS WITH FOODCONTROLLER TO AVOID HAVING BASICALLY THE SAME SHIT
    {
        #region VARIABLES
        [SerializeField, InspectorName("Empty State")] private GameObject _emptyState;
        [SerializeField, InspectorName("Filled State")] private GameObject _filledState;
        
        public DRINKS_STATES DrinkState { get; private set; }
        public DRINKS_BASE DrinkBase { get; private set; } //The base of the drink, that will form the basis of the drink. The player can add more stuff => the drinks_content
        [SerializeField] public List<DRINKS_CONTENT> DrinkContents { get; private set; }
        #endregion

        protected void Awake()
        {
            Assert.IsNotNull(_emptyState);
            Assert.IsNotNull(_filledState);
            ChangeDrinkState(DRINKS_STATES.EMPTY);
            DrinkContents = new List<DRINKS_CONTENT>();
        }

        public void ChangeDrinkState(DRINKS_STATES newState)
        {
            DrinkState = newState;
            ChangeVisualDrinkState(DrinkState);
        }

        public void ChangeVisualDrinkState(DRINKS_STATES newState)
        {
            switch(newState)
            {
                case DRINKS_STATES.EMPTY:
                    _emptyState.SetActive(true);
                    _filledState.SetActive(false);
                    break;
                case DRINKS_STATES.FILLED:
                    _emptyState.SetActive(false);
                    _filledState.SetActive(true);
                    break;
            }
        }

        /// <summary>
        /// Function to change the base of a drink.
        /// </summary>
        /// <param name="drinkBase"></param>
        public void ChangeDrinkBase(DRINKS_BASE drinkBase)
        {
            DrinkBase = drinkBase;
            LogManager.InfoLog(this.GetType(), "Drink now contains base : " + DrinkBase);
        }

        /// <summary>
        /// Function to add a content to a drink (will only add one version of that content)
        /// </summary>
        /// <param name="content"></param>
        public void AddContentToDrink(DRINKS_CONTENT content)
        {
            if (!DrinkContents.Contains(content)) //to alleviate missclicks and other shits like that, players can only add content ONCE
            {
                LogManager.InfoLog(this.GetType(), "Drink now contains content : " + content);
                DrinkContents.Add(content);
            }
        }
    }
}
