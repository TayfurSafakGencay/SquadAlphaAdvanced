using System;

namespace Systems.SavingSystem
{
	[UnityEngine.CreateAssetMenu(fileName = "SaveData", menuName = "ScriptableObjects/Save/SaveData", order = 0)]
	public class SaveData : UnityEngine.ScriptableObject
	{
		public SaveDTO SaveDTO { get; set; }
	}
	
	[Serializable]
	public class SaveDTO
	{
	}
}