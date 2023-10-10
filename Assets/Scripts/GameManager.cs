using Horror.DEBUG;
using Horror.Interactable;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Horror.Player
{
    /// <summary>
    /// This script, that will be present as a standalone script on a dontdestroyonload object, will act as the main reference of objects/data for all entities present in the game
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        #region SINGLETON DESIGN PATTERN
        private static GameManager _instance;
        public static GameManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new GameManager();
                }
                return _instance;
            }
        }
        private void Awake()
        {
            _instance = this;
        }
        #endregion

        #region PREFAB VARIABLES
        [Header("Prefab objects (DEBUG DISPLAY)")]
        [SerializeField] private GameObject _counter;
        [SerializeField] private GameObject _player;
        #endregion

        #region GETTERS
        public GameObject GetPlayer() { return _player; }
        public GameObject GetCounter() { return _counter; }
        #endregion

        private void Start()
        {
            DontDestroyOnLoad(gameObject);

            ObtainObjectsInScene();
            
        }

        private void OnLevelWasLoaded(int level)
        {
            ObtainObjectsInScene();
        }

        /// <summary>
        /// Function that will, upon a level load or start, check for all the relevant objects the game manager needs to hold
        /// </summary>
        private void ObtainObjectsInScene()
        {
            _counter = GameObject.FindObjectOfType<FoodDrinkAnalyserController>().gameObject;
            if (_counter is null)
            {
                LogManager.ErrorLog(this.GetType(), "No Counter type object found in scene");
            }
            _player = GameObject.FindObjectOfType<PlayerLookController>().gameObject;
            if (_player is null)
            {
                LogManager.CriticalLog(this.GetType(), "No Player type object found in scene");
            }
        }
    }
}
