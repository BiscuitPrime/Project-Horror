using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Horror.Food
{
    [Serializable]
    public struct DrinkData
    {
        public List<DRINKS_CONTENT> Content;
        public DRINKS_BASE Base;
    }

    public abstract class ReadOnlyDrinkRecipe : Recipe
    {
        public abstract DrinkData DrinkRecipeData { get; }
    }

    [CreateAssetMenu(fileName = "DrinkRecipe", menuName = "Scriptable Objects/Food/DrinkRecipe")]
    public class DrinkRecipe : ReadOnlyDrinkRecipe
    {
        public override DrinkData DrinkRecipeData => _drinkData;
        [SerializeField, InspectorName("Drink Data")] private DrinkData _drinkData;
    }
}
