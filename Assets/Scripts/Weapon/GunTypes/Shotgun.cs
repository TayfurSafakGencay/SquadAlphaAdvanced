using UnityEngine;
using Weapon.Base;

namespace Weapon.GunTypes
{
	[CreateAssetMenu(fileName = "Shotgun-", menuName = "Scriptable Objects/Gun/Create a Shotgun", order = 0)]
	public class Shotgun : GunConfig
	{
		public float SpreadAngle;
	}
}