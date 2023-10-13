using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;

namespace Horror.Player
{
    /// <summary>
    /// Script used by the Player's UI controller
    /// </summary>
    public class PlayerUIController : MonoBehaviour
    {
        #region SINGLETON DESIGN PATTERN
        private static PlayerUIController _instance;
        public static PlayerUIController Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new PlayerUIController();
                }
                return _instance;
            }
        }
        private void Awake()
        {
            _instance = this;
        }
        #endregion

        #region VARIABLES
        [Header("UI Elements")]
        [SerializeField, InspectorName("Client Text")] private TextMeshProUGUI _clientText;
        #endregion

        private void Start()
        {
            Assert.IsNotNull(_clientText);
            _clientText.gameObject.SetActive(false);
        }

        public IEnumerator DisplayClientText(string text)
        {
            _clientText.text = text;
            _clientText.gameObject.SetActive(true);
            yield return new WaitForSeconds(2f);
            _clientText.gameObject.SetActive(false);
        }
    }
}
