using Actor.Camera;
using Actor.Firing;
using UnityEngine;

namespace Actor
{
	public class Player : MonoBehaviour
	{
		public PlayerCamera PlayerCamera { get; private set; }
		
		public PlayerShooting PlayerShooting { get; private set; }

		private void Awake()
		{
			PlayerCamera = GetComponent<PlayerCamera>();
			PlayerShooting = GetComponent<PlayerShooting>();
		}
	}
}