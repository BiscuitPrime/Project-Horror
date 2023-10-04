using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Horror.Player
{
    /// <summary>
    /// This class is used by the player as its input manager, that will manage the player's inputs and call the adequate functions in other scripts
    /// </summary>
    [RequireComponent(typeof(PlayerMovementController))]
    [RequireComponent(typeof(PlayerLookController))]
    public class InputManager : MonoBehaviour
    {
        #region VARIABLES
        private PlayerInputs _input; //The player's input system
        private PlayerInputs.OnFootActions _onFootActions;
        private PlayerMovementController _playerMovementController;
        private PlayerLookController _playerLookController;
        #endregion

        void Awake()
        {
            _playerMovementController = GetComponent<PlayerMovementController>();
            _playerLookController = GetComponent<PlayerLookController>();

            _input = new PlayerInputs();
            _onFootActions = _input.OnFoot;
            _onFootActions.Interact.performed += ctx => _playerLookController.Interact(); //everytime the jump input is performed, we call the jump() function throught a callback context
        }

        // FIXEDUPDATE FOR PHYSICS MOVEMENTS
        private void FixedUpdate()
        {
            _playerMovementController.Move(_onFootActions.Movement.ReadValue<Vector2>());
        }

        // LATEUPDATE FOR CAMERA MOVEMENTS
        private void LateUpdate()
        {
            _playerLookController.Look(_onFootActions.Look.ReadValue<Vector2>());
        }

        //UPDATE FOR WEAPON/ACTIONS HANDLING
        private void Update()
        {

        }

        //we enable the player's input system when the player is enabled itself :
        private void OnEnable()
        {
            _onFootActions.Enable();
        }

        //we disable the player's input system when the player is disabled itself :
        private void OnDisable()
        {
            _onFootActions.Disable();
        }
    }
}
