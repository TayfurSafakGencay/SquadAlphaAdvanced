using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Weapon.Base;

namespace Weapon.DefaultData
{
	[CreateAssetMenu(fileName = "DefaultGunData", menuName = "Scriptable Objects/Gun/UnsavedData", order = 0)]
	public class DefaultGunData : ScriptableObject
	{
		[SerializeField]
		private List<DefaultGunDTO> _guns;

		private Dictionary<string, DefaultGunDTO> _gunsDictionary;

		private bool _isInitialized;

		private async void OnEnable()
		{
			_isInitialized = false;

			await Initialize();
		}

		public async Task Initialize()
		{
			if (_isInitialized) return;
			_isInitialized = true;

			_gunsDictionary = new Dictionary<string, DefaultGunDTO>();

			foreach (DefaultGunDTO gun in _guns)
			{
				_gunsDictionary.TryAdd(gun.GunConfig.GunDTO.Name, gun);
			}

			await UniTask.Delay(50);
		}

		public DefaultGunDTO GetGunData(string gunName)
		{
			if (_gunsDictionary.TryGetValue(gunName, out DefaultGunDTO data)) return data;

			Debug.LogWarning($"Gun with name {gunName} not found!");
			return new DefaultGunDTO();
		}
	}
	
	[Serializable]
	public struct DefaultGunDTO
	{
		public GunConfig GunConfig;
		
		public Sprite Icon;
		
		public Gun Gun;
		
		public Bullet Bullet;

	}
}