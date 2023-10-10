using Horror.Food;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Horror.Clients
{
    public abstract class ReadOnlyClientOrder : ScriptableObject
    {
        public abstract int Id { get; }
        public abstract FoodRecipe[] FoodItems { get; }
        public abstract DrinkRecipe[] DrinkItems { get; }
    }

    [CreateAssetMenu(fileName = "Order", menuName = "Scriptable Objects/Order/Client Order")]
    public class ClientOrder : ReadOnlyClientOrder
    {
        public override int Id => _id;

        public override FoodRecipe[] FoodItems => _foodItems;

        public override DrinkRecipe[] DrinkItems => _drinkItems;

        [SerializeField, InspectorName("Id")] private int _id;
        [SerializeField, InspectorName("Food Items")] private FoodRecipe[] _foodItems;
        [SerializeField, InspectorName("Drink Items")] private DrinkRecipe[] _drinkItems;
    }
}
