using Systems.SavingSystem;
using UnityEditor;
using UnityEngine;

namespace Editor.SaveSystem
{
	[CustomEditor(typeof(SaveManager))]
	public class SaveManagerEditor : UnityEditor.Editor
	{
		public override void OnInspectorGUI()
		{
			DrawDefaultInspector();
 
			EditorGUILayout.Space();
			EditorGUILayout.LabelField("Save Management", EditorStyles.boldLabel);
 
			if (GUILayout.Button("Delete Save Data"))
			{
				DeleteSaveData();
			}
		}
 
		private void DeleteSaveData()
		{
			string fullPath = SaveManager.SavePath;
 
			if (System.IO.File.Exists(fullPath))
			{
				System.IO.File.Delete(fullPath);
				Debug.Log("Save data deleted successfully.");
			}
			else
			{
				Debug.LogWarning("No save data found to delete.");
			}
		}
	}
}