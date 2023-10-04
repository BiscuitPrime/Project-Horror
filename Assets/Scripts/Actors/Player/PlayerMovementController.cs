using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Horror.Player
{
    public class PlayerMovementController : MonoBehaviour
    {
        #region VARIABLES
        [SerializeField] private AudioSource _walkAudio;
        private CharacterController _controller;
        private Vector3 _velocity;
        [SerializeField] private float _baseSpeed = 5f;
        private float _curSpeed;
        [SerializeField] private float _gravity = -9.8f;
        private bool _isGrounded;
        #endregion

        private void Awake()
        {
            _controller = GetComponent<CharacterController>();
            _curSpeed = _baseSpeed;
        }

        private void Update()
        {
            _isGrounded = _controller.isGrounded;
        }

        /// <summary>
        /// Class handling the movement of the player
        /// </summary>
        /// <param name="input">Input of the movement</param>
        public void Move(Vector2 input)
        {
            Vector3 direction = Vector3.zero;
            direction.x = input.x;
            direction.z = input.y;
            if (direction.magnitude > 0.5)
            {
                _walkAudio.enabled = true;
            }
            else
            {
                _walkAudio.enabled = false;
            }
            _controller.Move(_curSpeed * Time.deltaTime * transform.TransformDirection(direction));
            _velocity.y += _gravity * Time.deltaTime;
            if (_isGrounded && _velocity.y < 0)
            {
                _velocity.y = -2f;
            }
            _controller.Move(_velocity * Time.deltaTime);
        }
    }
}
