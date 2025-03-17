using Systems.Singleton;
using UnityEngine;

namespace Systems.SavingSystem
{
	public class SaveManager : Singleton<SaveManager>
	{
		[SerializeField]
		private SaveData _saveData;
		
		public static string SavePath => Application.persistentDataPath + "/save.json";

		protected override void Awake()
		{
			base.Awake();
			
			Load();
		}

		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.S))
			{
				Save();
			}
		}

		public void Load()
		{
			_saveData.SaveDTO = SaveSystem.Load();
		}
		
		public void Save()
		{
			SaveSystem.Save(_saveData);
		}
	}
}