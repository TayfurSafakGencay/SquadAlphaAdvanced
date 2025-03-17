using UnityEngine;

namespace Interfaces
{
	public interface IMovable
	{
		float Speed { get; set; }
		
		Rigidbody Rigidbody { get; }
	}
}