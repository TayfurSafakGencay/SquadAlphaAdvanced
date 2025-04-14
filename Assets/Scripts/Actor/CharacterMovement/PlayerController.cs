using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Actor.CharacterMovement.States;
using Actor.CharacterMovement.Stats;
using Interfaces;

namespace Actor.CharacterMovement
{
    public class PlayerController : MonoBehaviour, IMovable
    {
        private Player _player;
        public CharacterStateMachine StateMachine { get; private set; }
        public Rigidbody Rigidbody { get; private set; }
        public InputManager InputManager { get; private set; }

        public PlayerMovementStats MovementStats => _playerMovementStats;

        [SerializeField] private PlayerMovementStats _playerMovementStats;
        
        private void Awake()
        {
            _player = GetComponent<Player>();
            Rigidbody = GetComponent<Rigidbody>();
            StateMachine = new CharacterStateMachine();
            InputManager = new InputManager();
            
            StateMachine.ChangeState(new IdleState(this));
            
            InputBindings();
        }

        private void InputBindings()
        {
            InputManager.Player.Attack.performed += Firing;
            InputManager.Player.Attack.canceled += StopFiring;
            
            InputManager.Player.Jump.performed += _ => Jump();
        }

        private void Update()
        {
            Debug.DrawRay(_groundCheck.position, Vector3.down * 0.2f, CheckIfGrounded() ? Color.green : Color.red);

            StateMachine.UpdateState();
            
            RotateTowardsMouse();
        }

        #region Movement

        public bool IsSprinting()
        {
            InputManager.Player.Sprint.ReadValue<float>();
            
            return InputManager.Player.Sprint.ReadValue<float>() > 0;
        }
        
        public bool IsMovingInput()
        {
            Vector2 moveInput = InputManager.Player.Move.ReadValue<Vector2>();
            return moveInput != Vector2.zero;
        }
        
        public void MoveCharacter(float speed)
        {
            Vector2 input = InputManager.Player.Move.ReadValue<Vector2>();
            input = input.normalized;
            
            OnMoveCharacter(input, speed);
        }

        private void OnMoveCharacter(Vector2 input, float forwardSpeed)
        {
            Vector3 moveDirection = transform.right * input.x + transform.forward * input.y;
            moveDirection.y = 0;

            float speed = 0f;

            if (input.y > 0) 
            {
                speed = forwardSpeed;
            }
            else if (input.y < 0)
            {
                speed = MovementStats.BackwardSpeed;
            }
            else if (input.x != 0)
            {
                speed = MovementStats.StrafeSpeed;
            }

            Rigidbody.linearVelocity = moveDirection.normalized * speed;
        }
        
        public void StopMovement()
        {
            Rigidbody.linearVelocity = Vector3.zero;
        }

        #endregion

        #region Rotation

        private void RotateTowardsMouse()
        {
            Vector3 mousePosition = GetMouseWorldPosition();
            Vector3 lookDirection = mousePosition - transform.position;
    
            Vector3 flatDirection = new(lookDirection.x, 0, lookDirection.z);
            Quaternion flatRotation = Quaternion.LookRotation(flatDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, flatRotation, Time.deltaTime * 10f);
        }

        private Vector3 GetMouseWorldPosition()
        {
            Ray ray = _player.PlayerCamera.Camera.ScreenPointToRay(Mouse.current.position.ReadValue());
            return Physics.Raycast(ray, out RaycastHit hitInfo) ? hitInfo.point : transform.position;
        }

        #endregion

        #region Fire

        public bool IsFiring { get; private set; }
        public void Firing(InputAction.CallbackContext callbackContext = default)
        {
            IsFiring = true;
            
            _player.PlayerShooting.ActivateFire();
        }
        
        public void StopFiring(InputAction.CallbackContext callbackContext = default)
        {
            IsFiring = false;
            
            _player.PlayerShooting.DeactivateFire();
        }

        #endregion

        #region Jump

        private void Jump()
        {
            if (!CheckIfGrounded()) return;
            
            StateMachine.ChangeState(new JumpState(this));
        }

        [Header("Ground Check")]
        
        [SerializeField] private LayerMask _groundedLayers;
        
        [SerializeField] private Transform _groundCheck;
        public bool CheckIfGrounded()
        {
            return Physics.Raycast(_groundCheck.position, Vector3.down, 0.2f, _groundedLayers);
        }
        
        #endregion

        #region Input

        private readonly List<InputAction> _inputActions = new();
        
        public void NewInputs(params InputAction[] inputActions)
        {
            _inputActions.Clear();
            
            foreach (InputAction inputAction in inputActions)
            {
                _inputActions.Add(inputAction);
            }
            
            InputSetter(_inputActions);
        }

        public void InputSetter(List<InputAction> inputActions)
        {
            foreach (InputAction inputAction in InputManager.Player.Get())
            {
                if (!inputActions.Contains(inputAction)) 
                    inputAction.Disable();
            }
    
            foreach (InputAction inputAction in inputActions)
            {
                if (!inputAction.enabled) 
                    inputAction.Enable();
            }
        }

        #endregion
    }
}
