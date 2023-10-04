using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Horror.Player
{
    public class PlayerLookController : MonoBehaviour
    {
        #region VARIABLES
        [SerializeField] private Camera _cam;
        private float _upRotation = 0f;
        private readonly float _xSensitivity = 40f;
        private readonly float _ySensitivity = 40f;
        #endregion

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
            Debug.Log("[PLAYERLOOKMANAGER] : Interact button called");
        }
    }
}
