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
    /// Script used by the drinks, that operate slightly differently than foods : they can only have two states : empty or filled.
    /// </summary>
    public class DrinksController : MonoBehaviour //TODO : REWORK THIS AS TO MAKE IT INHERIT FROM A SINGLE CLASS WITH FOODCONTROLLER TO AVOID HAVING BASICALLY THE SAME SHIT
    {
        #region VARIABLES
        [SerializeField, InspectorName("Empty State")] private GameObject _emptyState;
        [SerializeField, InspectorName("Filled State")] private GameObject _filledState;
        
        public DRINKS_STATES DrinkState { get; private set; }
        #endregion

        protected void Awake()
        {
            Assert.IsNotNull(_emptyState);
            Assert.IsNotNull(_filledState);
            ChangeDrinkState(DRINKS_STATES.EMPTY);
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
    }
}
