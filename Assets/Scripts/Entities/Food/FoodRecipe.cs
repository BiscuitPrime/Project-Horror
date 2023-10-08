using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Horror.Food
{
    [Serializable]
    public struct FoodItemType
    {
        public GameObject Food;
        public FOOD_STATES FoodState;
    }
    
    public abstract class ReadOnlyFoodRecipe : ScriptableObject
    {
        public abstract FoodItemType[] FoodPile { get; }
    }

    [CreateAssetMenu(fileName ="Recipe",menuName ="Scriptable Objects/Food/Recipe")]
    public class FoodRecipe : ReadOnlyFoodRecipe
    {
        [SerializeField, InspectorName("Food Pile")] private FoodItemType[] _foodPile;

        public override FoodItemType[] FoodPile => _foodPile;
    }
}
