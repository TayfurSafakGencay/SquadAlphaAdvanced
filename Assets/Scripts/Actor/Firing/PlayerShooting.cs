using System;
using System.Collections.Generic;
using Systems.Event_Bus;
using Systems.RuntimeData;
using UnityEngine;
using UnityEngine.InputSystem;
using Weapon.Base;
using Weapon.DefaultData;

namespace Actor.Firing
{
	public class PlayerShooting : MonoBehaviour
	{
		public Player Player { get; private set; }
		public Gun ActiveGun { get; private set; }

		public List<GunConfig> Guns;
		
		private InputManager _inputManager;
		
		private async void Awake()
		{
			Player = GetComponent<Player>();
    
			DefaultGunData defaultGunData = await RuntimeDataManager.Instance.LoadAsync<DefaultGunData>(AddressableKey.GunUnsavedData);

			List<Gun> instantiatedGuns = new();

			foreach (GunConfig gunConfig in Guns)
			{
				DefaultGunDTO dto = defaultGunData.GetGunData(gunConfig.Name);
        
				gunConfig.Initialize(Player);
				gunConfig.SetObjects(dto.Gun, dto.Bullet);

				Gun gunInstance = Instantiate(gunConfig.Gun.gameObject, transform).GetComponent<Gun>();
				gunInstance.Init(this, gunConfig);
				gunInstance.transform.localPosition = new Vector3(0, 0, 1);
				gunInstance.transform.localRotation = Quaternion.identity;
				gunInstance.gameObject.SetActive(false); 
				
				instantiatedGuns.Add(gunInstance);

				gunConfig.Gun = gunInstance;
			}

			ActiveGun = instantiatedGuns[0];
			ActiveGun.gameObject.SetActive(true);

			EventBus<InGameScreenEvent>.Publish(new InGameScreenEvent
			{
				Guns = Guns
			});
			
			InitializeInput();
		}
		
		private void InitializeInput()
		{
			_inputManager = new InputManager();
			_inputManager.Enable(); 

			_gun1Action = _ => ChangeGun(0);
			_gun2Action = _ => ChangeGun(1);
			_gun3Action = _ => ChangeGun(2);
			_gun4Action = _ => ChangeGun(3);

			_inputManager.Player.Gun_1.performed += _gun1Action;
			_inputManager.Player.Gun_2.performed += _gun2Action;
			_inputManager.Player.Gun_3.performed += _gun3Action;
			_inputManager.Player.Gun_4.performed += _gun4Action;
		}
		
		private void Update()
		{
			if (ActiveGun == null) return;
			
			ActiveGun.Fire();
		}
		
		private Action<InputAction.CallbackContext> _gun1Action;
		private Action<InputAction.CallbackContext> _gun2Action;
		private Action<InputAction.CallbackContext> _gun3Action;
		private Action<InputAction.CallbackContext> _gun4Action;
		
		private void ChangeGun(int index)
		{
			if (ActiveGun != null)
			{
				ActiveGun.IsFiring = false;
				ActiveGun.gameObject.SetActive(false);
			}
			
			if (index < 0 || index >= Guns.Count) return;

			ActiveGun = Guns[index].Gun;
			ActiveGun.gameObject.SetActive(true);
			ActiveGun.ChangeGun();
			
			EventBus<InGameScreenSelectGunEvent>.Publish(new InGameScreenSelectGunEvent
			{
				GunIndex = index
			});
		}

		public void ActivateFire()
		{
			ActiveGun.IsFiring = true;
		}
		
		public void DeactivateFire()
		{
			ActiveGun.IsFiring = false;
		}

		private void OnDisable()
		{
			_inputManager.Player.Gun_1.performed -= _gun1Action;
			_inputManager.Player.Gun_2.performed -= _gun2Action;
			_inputManager.Player.Gun_3.performed -= _gun3Action;
			_inputManager.Player.Gun_4.performed -= _gun4Action;

			foreach (GunConfig gunConfig in Guns)
			{
				gunConfig.Gun.gameObject.SetActive(false);
			}
		}
	}
}