using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Horror.DEBUG;
using UnityEngine.Assertions;

namespace Horror.Player
{
    [RequireComponent(typeof(InteractionManager))]
    public class PlayerLookController : MonoBehaviour
    {
        #region VARIABLES
        [SerializeField] private Camera _cam;
        private float _upRotation = 0f;
        private readonly float _xSensitivity = 40f;
        private readonly float _ySensitivity = 40f;
        private InteractionManager _interactionManager;
        #endregion

        private void Awake()
        {
            _interactionManager = GetComponent<InteractionManager>();
            Assert.IsNotNull( _interactionManager);
        }

        /// <summary>
        /// Main function handling the looking done by the player
        /// </summary>
        /// <param name="input">the inputted values from the player inputs</param>
        public void Look(Vector2 input)
        {
            float mouseX = input.x;
            float mouseY = input.y;
            //Looking up and down :
            _upRotation -= (mouseY * Time.deltaTime) * _ySensitivity;
            _upRotation = Mathf.Clamp(_upRotation, -80f, 80f);
            //apply it :
            _cam.transform.localRotation = Quaternion.Euler(_upRotation, 0f, 0f);
            //rotate left-right :
            transform.Rotate(_xSensitivity * (mouseX * Time.deltaTime) * Vector3.up);
        }

        /// <summary>
        /// This function is called when the player tries to interact by pressing the interact button
        /// </summary>
        public void Interact()
        {
            LogManager.InfoLog(this.GetType(), "Interact button called");
            _interactionManager.Interact(_cam);
        }
    }
}
