using Horror.Clients;
using Horror.DEBUG;
using Horror.Food;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem.XR;

namespace Horror.Interactable
{
    /// <summary>
    /// Script used by mount objects that will analyse the food and determine if it's in adequation with the wanted recipe
    /// </summary>
    public class FoodDrinkAnalyserController : MonoBehaviour
    {
        [SerializeField] private FoodRecipe[] _wantedFoodRecipes;
        [SerializeField] private DrinkRecipe[] _wantedDrinkRecipe;
        private ClientController _curClient;
        private List<GameObject> _foodPile;

        private void Awake()
        {
            _foodPile = new List<GameObject>();
        }

        public void SetFoodRecipes(FoodRecipe[] foodRecipes)
        {
            _wantedFoodRecipes = foodRecipes;
        }
        public void SetDrinkRecipes(DrinkRecipe[] drinkRecipes)
        {
            _wantedDrinkRecipe = drinkRecipes;
        }
        public void SetCurrentClient(ClientController client)
        {
            _curClient = client;
        }

        private void AcquireFoodPile(GameObject food)
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
            if (curFood != null)
            {
                _foodPile.Add(curFood);
            }
        }

        public void StartAnalyse(GameObject food) //This whole function is ugly : TODO : REWORK THAT FUNCTION TO BEMORE MODULAR/CLEAN
        {
            //NEW :
            //We start by testing the drink type recipe :
            if (food.GetComponent<DrinksController>() != null)
            {
                LogManager.InfoLog(this.GetType(), "Food is drink : comparing recipes...");
                DrinksController controller = food.GetComponent<DrinksController>();
                foreach (DrinkRecipe drinkRecipe in _wantedDrinkRecipe)
                {
                    if (CompareDrinks(drinkRecipe, controller)) //if one recipe corresponds to the drink, we're good
                    {
                        LogManager.InfoLog(this.GetType(), "Found corresponding drink recipe : " + drinkRecipe.name);
                        _curClient.ReceiveCorrectOrderFromCounter(drinkRecipe, food);
                        return;
                    }
                }
                LogManager.InfoLog(this.GetType(), "NO CORRECT RECIPES FOUND");
                _curClient.ReceiveIncorrectOrderFromCounter();
                return;
            }

            //we then acquire the food pile :
            AcquireFoodPile(food);

            //we then test the food type recipe :
            foreach (FoodRecipe recipe in _wantedFoodRecipes) //we compare for each recipe we have stored in the wanted recipes
            {
                if (recipe.FoodPile.Length != _foodPile.Count)
                {
                    LogManager.InfoLog(this.GetType(), "FoodPile and recipe do not have same amount of stuff : " + recipe.FoodPile.Length + " and foodpile : " + _foodPile.Count);
                    _curClient.ReceiveIncorrectOrderFromCounter();
                    return;
                }
                else
                {
                    if (IsFoodPileCorrectRecipe(recipe))
                    {
                        LogManager.InfoLog(this.GetType(), "GOOD RECIPE FOUND : " + recipe.name);
                        _curClient.ReceiveCorrectOrderFromCounter(recipe, food);
                        return;
                    }
                }
            }
            LogManager.InfoLog(this.GetType(), "NO CORRECT RECIPES FOUND");
            _curClient.ReceiveIncorrectOrderFromCounter();
            return;
        }

        /// <summary>
        /// Function that will 
        /// </summary>
        /// <returns></returns>
        private bool IsFoodPileCorrectRecipe(FoodRecipe recipe)
        {
            for (int i = 0; i < _foodPile.Count; i++)
            {
                LogManager.InfoLog(this.GetType(), "Comparing : " + _foodPile[i].name + " with : " + recipe.FoodPile[i].Food.name);
                if (_foodPile[i].name != recipe.FoodPile[i].Food.name) //First, we test the names : if they are the same, we go on, otherwise we end it here
                {
                    LogManager.InfoLog(this.GetType(), "WRONG RECIPE");
                    return false;
                }
                if (_foodPile[i].GetComponent<FoodController>()!=null)//if the item is a cookable food, we check that it is correctly cooked
                {
                    if(_foodPile[i].GetComponent<FoodController>().FoodState != recipe.FoodPile[i].FoodState)
                    {
                        LogManager.InfoLog(this.GetType(), "WRONG RECIPE");
                        return false;
                    }
                }
            }
            return true;
        }

        private bool CompareDrinks(DrinkRecipe recipe, DrinksController drinkController)
        {
            if (drinkController.DrinkBase != recipe.DrinkRecipeData.Base)
            {
                LogManager.InfoLog(this.GetType(), "Drink base is incorrect");
                return false;
            }
            foreach (var content in recipe.DrinkRecipeData.Content)
            {
                if (!drinkController.DrinkContents.Contains(content))
                {
                    LogManager.InfoLog(this.GetType(), "Drink content : " + content.ToString() + " is not present");
                    return false;
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
