﻿using UnityEngine;

namespace Systems.Singleton
{
	public abstract class Singleton<T> : MonoBehaviour where T : Component
	{
		public static T Instance;

		protected virtual void Awake()
		{
			if (Instance == null)
			{
				Instance = this as T;
			}
			else
			{
				Destroy(gameObject);
			}
		}
	}
}