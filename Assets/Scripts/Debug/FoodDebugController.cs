using Horror.Food;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Horror.DEBUG
{
    /// <summary>
    /// Simple debug script to easily change the state of a food object (FOR DEBUG PURPOSES ONLY, SHOULD BE REMOVED ONCE GAME IS DONE)
    /// </summary>
    public class FoodDebugController : MonoBehaviour
    {
        [SerializeField] private bool changeStateToRaw = false;
        [SerializeField] private bool changeStateToCooked = false;
        [SerializeField] private bool changeStateToOverdone = false;

        // Update is called once per frame
        void Update()
        {
            if (changeStateToRaw)
            {
                GetComponent<FoodController>().ChangeFoodState(FOOD_STATES.RAW);
                changeStateToRaw=false;
            }
            if (changeStateToOverdone)
            {
                GetComponent<FoodController>().ChangeFoodState(FOOD_STATES.OVERDONE);
                changeStateToOverdone = false;
            }
            if (changeStateToCooked)
            {
                GetComponent<FoodController>().ChangeFoodState(FOOD_STATES.COOKED);
                changeStateToCooked = false;
            }
        }
    }
}
