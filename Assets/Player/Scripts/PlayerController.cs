using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using System;
using UnityEngine.InputSystem;
using static UnityEditor.Experimental.GraphView.GraphView;

namespace Player
{
    [DefaultExecutionOrder(-1)]
    public class PlayerController : MonoBehaviour
    {
        #region Class Variables
        [Header("����������")]
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private Camera _playerCamera;
        public float RotationMismatch { get; private set; } = 0f;
        public bool IsRotatingToTarget { get; private set; } = false;

        [Header("������������")]
        public float walkAcceleration = 25f;
        public float walkSpeed = 2f;
        public float runAcceleration = 35f;
        public float runSpeed = 4f;
        public float sprintAcceleration = 50f;
        public float sprintSpeed = 7f;
        public float inAirAcceleration = 25f;
        public float drag = 20f;
        public float inAirDrag = 5f;
        public float gravity = 25f;
        public float terminalVelocity = 50f;
        public float jumpSpeed = 0.8f;
        public float movingThreshold = 0.01f;

        public float rollSpeed = 0.8f;

        [Header("��������")]
        public float playerModelRotationSpeed = 10f;
        public float rotateToTargetTime = 0.67f;

        [Header("��������� ������")]
        public float lookSenseH = 0.1f;
        public float lookSenseV = 0.1f;
        public float lookLimitV = 89f;

        [Header("������ �������")]
        [SerializeField] private LayerMask _groundLayers;
        /*[SerializeField] private GameObject _magicFloor;
        [SerializeField] private GameObject _magicFloor2;*/

        private List<GameObject> _magicFloor = new List<GameObject>();
        private List<GameObject> _magicFloor2 = new List<GameObject>();

        private PlayerLocomotionInput _playerLocomotionInput;
        private PlayerState _playerState;
        private PlayerActionsInput _playerActionsInput;

        private Vector2 _cameraRotation = Vector2.zero;
        private Vector2 _playerTargetRotation = Vector2.zero;

        private bool _jumpedLastFrame = false;
        private bool _isRotatingClockwise = false;
        private float _rotatingToTargetTimer = 0f;
        private float _verticalVelocity = 0f;
        private float _antiBump;
        private float _stepOffset;
        private float _magicRotate = 0f;

        private PlayerMovementState _lastMovementState = PlayerMovementState.Falling;
        #endregion

        #region Startup

        private void Start()
        {
            for(int i = 0; i < 2; ++i)
            {
                GameObject mF1 = GameObject.Find("MagicGround");

                if(mF1 != null)
                {
                    _magicFloor.Add(mF1);
                }    
            }

            for (int i = 0; i < 2; ++i)
            {
                GameObject mF2 = GameObject.Find("MagicGround2");

                if(mF2 != null)
                {
                    _magicFloor2.Add(mF2);
                }   
            }
        }
        private void Awake()
        {
            _playerLocomotionInput = GetComponent<PlayerLocomotionInput>();
            _playerActionsInput = GetComponent <PlayerActionsInput>();
            _playerState = GetComponent<PlayerState>();

            _antiBump = sprintSpeed;
            _stepOffset = _characterController.stepOffset;
        }
        #endregion

        #region Update Logic
        private void Update()
        {
            if(_magicFloor.Count == 2 && _magicFloor2.Count == 2)
            {
                ChangeRotate();
            }
            
            UpdateMovementState();
            HandleVerticalMovement();
            HandleLateralMovement();
        }

        private void ChangeRotate()
        {
            
            float dist = Math.Min(Vector3.Distance(_magicFloor[0].transform.position, transform.position),
                Vector3.Distance(_magicFloor[1].transform.position, transform.position));

            float dist2 = Math.Min(Vector3.Distance(_magicFloor2[0].transform.position, transform.position),
                Vector3.Distance(_magicFloor2[1].transform.position, transform.position));

            if (dist < 4f)
            {
                _magicRotate = 180f;
            }

            else if (dist2 < 4f)
            {
                _magicRotate = 0f;
            }
        }

        private void UpdateMovementState()
        {
            _lastMovementState = _playerState.CurrentPlayerMovementState;

            bool canRun = CanRun();
            bool isMovementInput = _playerLocomotionInput.MovementInput != Vector2.zero;            
            bool isMovingLaterally = IsMovingLaterally();                                           
            bool isSprinting = _playerLocomotionInput.SprintToggledOn && isMovingLaterally;          
            bool isWalking = isMovingLaterally && (!canRun || _playerLocomotionInput.WalkToggledOn); 
            bool isGrounded = IsGrounded();

            PlayerMovementState lateralState = isWalking ? PlayerMovementState.Walking :
                                               isSprinting ? PlayerMovementState.Sprinting :
                                               isMovingLaterally || isMovementInput ? PlayerMovementState.Running : PlayerMovementState.Idling;

            _playerState.SetPlayerMovementState(lateralState);


            // ������������ �������� �� ����� ������
            if ((!isGrounded || _jumpedLastFrame) && _characterController.velocity.y > 0f)
            {
                _playerState.SetPlayerMovementState(PlayerMovementState.Jumping);
                _jumpedLastFrame = false;
                _characterController.stepOffset = 0f;   
            }
            else if ((!isGrounded || _jumpedLastFrame) && _characterController.velocity.y <= 0f)
            {
                _playerState.SetPlayerMovementState(PlayerMovementState.Falling);
                _jumpedLastFrame = false;
                _characterController.stepOffset = 0f;
            }
            else
            {
                _characterController.stepOffset = _stepOffset;
            }
        }

        private void HandleVerticalMovement()
        {
            bool isGrounded = _playerState.InGroundedState();

            _verticalVelocity -= gravity * Time.deltaTime;

            if (isGrounded && _verticalVelocity < 0)
                _verticalVelocity = -_antiBump;

            if (_playerLocomotionInput.JumpPressed && isGrounded)
            {
                _verticalVelocity += Mathf.Sqrt(jumpSpeed * 3 * gravity);
                _jumpedLastFrame = true;
            }

            if (_playerState.IsStateGroundedState(_lastMovementState) && !isGrounded)
            {
                _verticalVelocity += _antiBump;
            }

            // ���������� ��������
            if (Mathf.Abs(_verticalVelocity) > Mathf.Abs(terminalVelocity))
            {
                _verticalVelocity = -1f * Mathf.Abs(terminalVelocity);
            }
        }

        private void HandleLateralMovement()
        {
            // ������ �� ������� ���������
            bool isSprinting = _playerState.CurrentPlayerMovementState == PlayerMovementState.Sprinting;
            bool isGrounded = _playerState.InGroundedState();
            bool isWalking = _playerState.CurrentPlayerMovementState == PlayerMovementState.Walking;

            // ��������� � ��������, ��������� �� ���������
            float lateralAcceleration = !isGrounded ? inAirAcceleration :
                                        isWalking ? walkAcceleration :
                                        isSprinting ? sprintAcceleration : runAcceleration;

            float clampLateralMagnitude = !isGrounded ? sprintSpeed :
                                          isWalking ? walkSpeed :
                                          isSprinting ? sprintSpeed : runSpeed;

            Vector3 cameraForwardXZ = new Vector3(_playerCamera.transform.forward.x, 0f, _playerCamera.transform.forward.z).normalized;
            Vector3 cameraRightXZ = new Vector3(_playerCamera.transform.right.x, 0f, _playerCamera.transform.right.z).normalized;
            Vector3 movementDirection = cameraRightXZ * _playerLocomotionInput.MovementInput.x + cameraForwardXZ * _playerLocomotionInput.MovementInput.y;

            Vector3 movementDelta = movementDirection * lateralAcceleration * Time.deltaTime;
            Vector3 newVelocity = _characterController.velocity + movementDelta;

            // ��������� ����������� ���������
            float dragMagnitude = isGrounded ? drag : inAirDrag;
            Vector3 currentDrag = newVelocity.normalized * dragMagnitude * Time.deltaTime;
            newVelocity = (newVelocity.magnitude > dragMagnitude * Time.deltaTime) ? newVelocity - currentDrag : Vector3.zero;
            newVelocity = Vector3.ClampMagnitude(new Vector3(newVelocity.x, 0f, newVelocity.z), clampLateralMagnitude);
            newVelocity.y += _verticalVelocity;
            newVelocity = !isGrounded ? HandleSteepWalls(newVelocity) : newVelocity;

            // ������������ ���������
            _characterController.Move(newVelocity * Time.deltaTime);  
        }

        private Vector3 HandleSteepWalls(Vector3 velocity)
        {
            Vector3 normal = CharacterControllerUtils.GetNormalWithSphereCast(_characterController, _groundLayers);
            float angle = Vector3.Angle(normal, Vector3.up);
            bool validAngle = angle <= _characterController.slopeLimit;

            if (!validAngle && _verticalVelocity < 0f)
                velocity = Vector3.ProjectOnPlane(velocity, normal);
            return velocity;
        }
        #endregion

        #region Late Update Logic
        private void LateUpdate()
        {
            UpdateCameraRotation();
        }

        private void UpdateCameraRotation()
        {
            _cameraRotation.x += lookSenseH * _playerLocomotionInput.LookInput.x;
            _cameraRotation.y = Mathf.Clamp(_cameraRotation.y - lookSenseV * _playerLocomotionInput.LookInput.y, -lookLimitV, lookLimitV);

            _playerTargetRotation.x += transform.eulerAngles.x + lookSenseH * _playerLocomotionInput.LookInput.x;

            float rotationTolerance = 90f;
            bool isIdling = _playerState.CurrentPlayerMovementState == PlayerMovementState.Idling;
            IsRotatingToTarget = _rotatingToTargetTimer > 0;

            // �������, ���� �� �� �����
            if (!isIdling)
            {
                RotatePlayerToTarget();
            }
            // �������, ����� �����
            else if (Mathf.Abs(RotationMismatch) > rotationTolerance || IsRotatingToTarget)
            {
                UpdateIdleRotation(rotationTolerance);
            }

            _playerCamera.transform.rotation = Quaternion.Euler(_cameraRotation.y, _cameraRotation.x, _magicRotate);

            // ���� ����� ������� � �������
            Vector3 camForwardProjectedXZ = new Vector3(_playerCamera.transform.forward.x, 0f, _playerCamera.transform.forward.z).normalized;
            Vector3 crossProduct = Vector3.Cross(transform.forward, camForwardProjectedXZ);
            float sign = Mathf.Sign(Vector3.Dot(crossProduct, transform.up));
            RotationMismatch = sign * Vector3.Angle(transform.forward, camForwardProjectedXZ);
        }

        private void UpdateIdleRotation(float rotationTolerance)
        {
            // ����� ����������� ��������
            if (Mathf.Abs(RotationMismatch) > rotationTolerance)
            {
                _rotatingToTargetTimer = rotateToTargetTime;
                _isRotatingClockwise = RotationMismatch > rotationTolerance;
            }
            _rotatingToTargetTimer -= Time.deltaTime;

            // ������� ������
            if (_isRotatingClockwise && RotationMismatch > 0f ||
                !_isRotatingClockwise && RotationMismatch < 0f)
            {
                RotatePlayerToTarget();
            }
        }

        private void RotatePlayerToTarget()
        {
            Quaternion targetRotationX = Quaternion.Euler(0f, _playerTargetRotation.x, 0f);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotationX, playerModelRotationSpeed * Time.deltaTime);
        }
        #endregion

        #region State Checks
        private bool IsMovingLaterally()
        {
            Vector3 lateralVelocity = new Vector3(_characterController.velocity.x, 0f, _characterController.velocity.z);

            return lateralVelocity.magnitude > movingThreshold;
        }

        private bool IsGrounded()
        {
            bool grounded = _playerState.InGroundedState() ? IsGroundedWhileGrounded() : IsGroundedWhileAirborne();

            return grounded;
        }

        private bool IsGroundedWhileGrounded()
        {
            Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - _characterController.radius, transform.position.z);

            bool grounded = Physics.CheckSphere(spherePosition, _characterController.radius, _groundLayers, QueryTriggerInteraction.Ignore);

            return grounded;
        }

        private bool IsGroundedWhileAirborne()
        {
            Vector3 normal = CharacterControllerUtils.GetNormalWithSphereCast(_characterController, _groundLayers);
            float angle = Vector3.Angle(normal, Vector3.up);
            bool validAngle = angle <= _characterController.slopeLimit;

            return _characterController.isGrounded && validAngle;
        }

        private bool CanRun()
        {
            // ����� �������� �� ��������� ��� ����� 45 �������� ��� ������ (������ �� ����� ������)
            return _playerLocomotionInput.MovementInput.y >= Mathf.Abs(_playerLocomotionInput.MovementInput.x);
        }
        #endregion
    }
}
