using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    [DefaultExecutionOrder(-2)]
    public class PlayerActionsInput : MonoBehaviour, PlayerControls.IPlayerActionsMapActions
    {
        #region Class Variables
        private PlayerLocomotionInput _playerLocomotionInput;
        private PlayerState _playerState;
        private MagicBallController _magicBallController;

        public bool AttackPressed { get; private set; }
        public bool RollPressed { get; private set; }
        public bool BasicMagicPressed { get; private set; }
        #endregion

        #region Startup
        private void Awake()
        {
            _playerLocomotionInput = GetComponent<PlayerLocomotionInput>();
            _playerState = GetComponent<PlayerState>();
            _magicBallController = GetComponent<MagicBallController>();
        }
        private void OnEnable()
        {
            if (PlayerInputManager.Instance?.PlayerControls == null)
            {
                Debug.LogError("Player controls is not initialized - cannot enable");
                return;
            }

            PlayerInputManager.Instance.PlayerControls.PlayerActionsMap.Enable();
            PlayerInputManager.Instance.PlayerControls.PlayerActionsMap.SetCallbacks(this);
        }

        private void OnDisable()
        {
            if (PlayerInputManager.Instance?.PlayerControls == null)
            {
                Debug.LogError("Player controls is not initialized - cannot disable");
                return;
            }

            PlayerInputManager.Instance.PlayerControls.PlayerActionsMap.Disable();
            PlayerInputManager.Instance.PlayerControls.PlayerActionsMap.RemoveCallbacks(this);
        }
        #endregion

        #region Update
        public void SetAttackPressedFalse() 
        { 
            AttackPressed = false;
        }

        public void SetRollPressedFalse()
        {
            RollPressed = false;
        }
        public void SetBasicMagicPressedFalse()
        {
            BasicMagicPressed = false;
        }

        #endregion

        #region Input Callbacks
        public void OnAttacking(InputAction.CallbackContext context)
        {
            if (!context.performed)
                return;

            AttackPressed = true;
        }

        public void OnRoll(InputAction.CallbackContext context)
        {
            if (!context.performed)
                return;

            RollPressed = true;
        }

        public void OnBasicMagic(InputAction.CallbackContext context)
        {
            if (!context.performed)
                return;

            if (GlobalsVar.isBasicMagicalAbilities)
            {
                BasicMagicPressed = true;
            }
        }
        #endregion
    }
}
