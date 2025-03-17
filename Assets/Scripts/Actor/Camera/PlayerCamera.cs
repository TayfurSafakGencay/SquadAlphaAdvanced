using UnityEngine;

namespace Actor.Camera
{
	public class PlayerCamera : MonoBehaviour
	{
		[SerializeField]
		private UnityEngine.Camera _camera;
		
		public UnityEngine.Camera Camera => _camera;
	}
}