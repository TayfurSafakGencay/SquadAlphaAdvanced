using System.Collections.Generic;
using System.Linq;
using Systems.Singleton;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Manager
{
    public class InputManager : Singleton<InputManager>
    {
        [SerializeField] private InputActionAsset inputActions;
        private readonly Dictionary<PlayerState, InputActionMap> actionMapDictionary = new();
        private readonly List<PlayerState> activeStates = new();

        public enum PlayerState
        {
            Movement,
            UI
        }

        protected override void Awake()
        {
            base.Awake();
            
            InitializeActionMaps();
        }
        private void Start()
        {
            Init();
        }
        public void Init()
        {
            DisableAllActionMaps();

            AddState(PlayerState.Movement);

        }
        private void InitializeActionMaps()
        {
            // Clone the input actions asset to avoid sharing references

            foreach (PlayerState state in System.Enum.GetValues(typeof(PlayerState)))
            {
                InputActionMap actionMap = inputActions.FindActionMap(state.ToString());
                if (actionMap != null)
                {
                    actionMapDictionary[state] = actionMap;
                }
                else
                {
                    Debug.LogWarning($"Action map not found for state: {state}");
                }
            }
        }

        public void DisableAllActionMaps()
        {
            foreach (InputActionMap actionMap in actionMapDictionary.Values)
            {
                foreach (InputAction action in actionMap.actions)
                {
                    action.Disable();
                }
                actionMap.Disable();
            }
            
            activeStates.Clear();
            Debug.Log("All action maps disabled");
        }

        public void AddState(PlayerState state)
        {
            if (activeStates.Contains(state) || !actionMapDictionary.TryGetValue(state, out InputActionMap actionMap)) return;
            
            activeStates.Add(state);

            foreach (InputAction action in actionMap.actions)
            {
                action.Enable();
            }

            actionMap.Enable();

            Debug.Log($"Enabled action map: {state}");

            AddState(PlayerState.UI);
        }

        public void RemoveState(PlayerState state)
        {
            if (!activeStates.Contains(state) || !actionMapDictionary.TryGetValue(state, out InputActionMap actionMap)) return;
            
            activeStates.Remove(state);
            actionMap.Disable();
            Debug.Log($"Disabled action map: {state}");
        }


        public void EnableAllInput()
        {
            // Reactivate previously active states
            foreach (var state in activeStates.ToList())
            {
                AddState(state);
            }
        }

        public InputAction GetAction(PlayerState state, string actionName)
        {
            if (actionMapDictionary.TryGetValue(state, out InputActionMap actionMap))
            {
                return actionMap.FindAction(actionName);
            }
            
            Debug.LogWarning($"Action map not found for state: {state}");
            return null;
        }

        public bool IsStateActive(PlayerState state)
        {
            return activeStates.Contains(state);
        }
    }
}