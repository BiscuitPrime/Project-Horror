using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Horror.Food
{
    /// <summary>
    /// This script is used to change the material of a drink depending on the base chosen. 
    /// it should be present on the CUP.
    /// </summary>
    public class DrinkBasePickerController : MonoBehaviour
    {
        [SerializeField] private DrinkBaseData _drinkData;
        [SerializeField] private GameObject _drinkFillerMesh;
        private Dictionary<DRINKS_BASE, Material> _dictDrinksMaterials;

        public void Start()
        {
            Assert.IsNotNull(_drinkData);
            Assert.IsNotNull(_drinkFillerMesh);
            _dictDrinksMaterials = new Dictionary<DRINKS_BASE, Material>();
            foreach(var data in _drinkData.Data)
            {
                _dictDrinksMaterials.Add(data.Base, data.Material);
            }
        }

        public void ChangeDrinkBase(DRINKS_BASE newBase)
        {
            _drinkFillerMesh.GetComponent<MeshRenderer>().material = _dictDrinksMaterials[newBase];
        }
    }
}
