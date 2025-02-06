using UnityEngine;

namespace Singleton
{
	public abstract class Singleton<T> : MonoBehaviour where T : Component
	{
		public static T Instance;

		private void Awake()
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