using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Horror.Food
{

    /// <summary>
    /// Base of the drink
    /// </summary>
    public enum DRINKS_BASE
    {
        WATER,
        SODA,
        COFFEE
    }

    [Serializable]
    public struct DRINK_BASE_DATA
    {
        public DRINKS_BASE Base;
        public Material Material;
    }

    [CreateAssetMenu(fileName ="DrinkBaseData",menuName ="Scriptable Objects/Food/DrinkData")]
    public class DrinkBaseData : ScriptableObject
    {
        [SerializeField] public List<DRINK_BASE_DATA> Data;
    }
}
