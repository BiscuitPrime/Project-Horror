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
    
    //TODO : RE-WRITE THAT WITH READONLY VERSION
    [CreateAssetMenu(fileName ="Recipe",menuName ="Scriptable Objects/Food/Recipe")]
    public class FoodRecipe : ScriptableObject
    {
        [SerializeField] public FoodItemType[] FoodPile;
    }
}
