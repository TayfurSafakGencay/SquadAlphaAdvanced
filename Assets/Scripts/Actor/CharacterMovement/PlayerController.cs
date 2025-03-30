using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Actor.CharacterMovement.States;
using Interfaces;

namespace Actor.CharacterMovement
{
    public class PlayerController : MonoBehaviour, IMovable
    {
        private Player _player;
        public CharacterStateMachine StateMachine { get; private set; }
        public Rigidbody Rigidbody { get; private set; }
        public float Speed { get; set; } = 5f;

        public InputManager InputManager { get; private set; }

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
            InputManager.Player.Attack.performed += _ => Firing(true);
            InputManager.Player.Attack.canceled += _ => Firing(false);
        }

        private void Update()
        {
            StateMachine.UpdateState();
            
            RotateTowardsMouse();
        }

        #region Movement
        
        public bool IsMovingInput()
        {
            Vector2 moveInput = InputManager.Player.Move.ReadValue<Vector2>();
            return moveInput != Vector2.zero;
        }
        
        public void MoveCharacter()
        {
            Vector2 input = InputManager.Player.Move.ReadValue<Vector2>();
            input = input.normalized;
            
            OnMoveCharacter(input);
        }

        private void OnMoveCharacter(Vector2 input)
        {
            Vector3 moveDirection = transform.right * input.x + transform.forward * input.y;
            moveDirection.y = 0; 
            Rigidbody.linearVelocity = moveDirection.normalized * Speed;
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

        private void Firing(bool isFiring) 
        {
            if (isFiring)
            {
                _player.PlayerShooting.ActivateFire();
            }
            else
            {
                _player.PlayerShooting.DeactivateFire();
            }
        }

        #endregion

        #region Jump

        private InputAction _jumpAction;


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
