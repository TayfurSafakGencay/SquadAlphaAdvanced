using System.IO;
using Newtonsoft.Json;
using UnityEngine;

namespace Systems.SavingSystem
{
	public static class SaveSystem
	{
		private static string SavePath => SaveManager.SavePath;
		public static void Save(SaveData saveData)
		{
			SaveDTO saveDTO = saveData.SaveDTO;

			string json = JsonConvert.SerializeObject(saveDTO, Formatting.Indented);
			File.WriteAllText(SavePath, json);
		}

		public static SaveDTO Load()
		{
			if (!File.Exists(SavePath))
			{
				Debug.LogWarning("Save file not found!");
				return default;
			}

			string json = File.ReadAllText(SavePath);

			SaveDTO dto = JsonConvert.DeserializeObject<SaveDTO>(json);
			return dto;
		}
	}
}