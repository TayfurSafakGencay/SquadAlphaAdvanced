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

        private InputManager _inputManager;
        private InputAction _jumpAction;

        private void Awake()
        {
            _player = GetComponent<Player>();
            Rigidbody = GetComponent<Rigidbody>();
            StateMachine = new CharacterStateMachine();
            _inputManager = new InputManager();
            
            _inputManager.Player.Move.Enable();
            
            StateMachine.ChangeState(new MovingState(this));
        }

        private void OnEnable()
        {
            RegisterInputListeners();
        }

        private void OnDisable()
        {
            RemoveInputListeners();
        }

        private void RegisterInputListeners()
        {
            // _jumpAction.performed += OnJump;
        }

        private void RemoveInputListeners()
        {
            // _jumpAction.performed -= OnJump;
        }

        private void Update()
        {
            StateMachine.UpdateState();
            
            RotateTowardsMouse();
        }

        private void OnJump(InputAction.CallbackContext context)
        {
            // StateMachine.ChangeState(new JumpState(this));
        }
        
        public void MoveCharacter()
        {
            Vector2 input = _inputManager.Player.Move.ReadValue<Vector2>();
            input = input.normalized;
            
            OnMoveCharacter(input);
            print(input);
        }

        private void OnMoveCharacter(Vector2 input)
        {
            Vector3 moveDirection = transform.right * input.x + transform.forward * input.y;
            moveDirection.y = 0; 
            Rigidbody.linearVelocity = moveDirection.normalized * Speed;
        }
        
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
    }
}
