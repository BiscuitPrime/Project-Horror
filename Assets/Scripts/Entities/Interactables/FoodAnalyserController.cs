using Horror.DEBUG;
using Horror.Food;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace Horror.Interactable
{
    /// <summary>
    /// Script used by mount objects that will analyse the food and determine if it's in adequation with the wanted recipe
    /// </summary>
    public class FoodAnalyserController : MonoBehaviour
    {
        [SerializeField] private FoodRecipe _wantedRecipe;
        private List<GameObject> _foodPile;

        private void Awake()
        {
            _foodPile = new List<GameObject>();
        }

        public void StartAnalyse(GameObject food)
        {
            GameObject curFood = food;
            _foodPile = new List<GameObject>();
            while (curFood.GetComponent<MountController>() != null)
            {
                _foodPile.Add(curFood);
                if (curFood.GetComponent<MountController>().GetRider() != null)
                {
                    curFood = curFood.GetComponent<MountController>().GetRider();
                }
                else
                {
                    curFood = null;
                    break;
                }
            }
            if(curFood != null)
            {
                _foodPile.Add(curFood);
            }
            
            LogManager.InfoLog(this.GetType(), "Comparing with recipe :");
            if(_wantedRecipe.FoodPile.Length != _foodPile.Count)
            {
                LogManager.InfoLog(this.GetType(), "FoodPile and recipe do not have same amount of stuff : "+ _wantedRecipe.FoodPile.Length+" and foodpile : "+_foodPile.Count);
                LogManager.InfoLog(this.GetType(), "WRONG RECIPE");
                return;
            }
            else
            {
                if (IsFoodPileCorrectRecipe())
                {
                    LogManager.InfoLog(this.GetType(), "GOOD RECIPE");
                    return;
                }
            }
        }

        private bool IsFoodPileCorrectRecipe()
        {
            for (int i = 0; i < _foodPile.Count; i++)
            {
                LogManager.InfoLog(this.GetType(), "Comparing : " + _foodPile[i].name + " with : " + _wantedRecipe.FoodPile[i].Food.name);
                if (_foodPile[i].name != _wantedRecipe.FoodPile[i].Food.name) //First, we test the names : if they are the same, we go on, otherwise we end it here
                {
                    LogManager.InfoLog(this.GetType(), "WRONG RECIPE");
                    return false;
                }
                if (_foodPile[i].GetComponent<FoodController>()!=null)//if the item is a cookable food, we check that it is correctly cooked
                {
                    if(_foodPile[i].GetComponent<FoodController>().FoodState != _wantedRecipe.FoodPile[i].FoodState)
                    {
                        LogManager.InfoLog(this.GetType(), "WRONG RECIPE");
                        return false;
                    }
                }
            }
            return true;
        }

        public void EndAnalyse()
        {
            _foodPile.Clear();
        }
    }
}
