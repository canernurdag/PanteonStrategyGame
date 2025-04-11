using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[DefaultExecutionOrder(-10)]
public class JsonSaveSystem : MonoBehaviour, ISaveSystem
{
	[field:SerializeField] public SaveState SaveState { get; set; } = new SaveState();
	private string _filePath;


	private void Awake()
	{
		_filePath = Application.persistentDataPath + "/SaveState.json";
		Load();
	}
	public void Save()
	{
		if (_filePath == null || _filePath == "")
		{
			_filePath = Application.persistentDataPath + "/SaveState.json";
		}
		string saveData = JsonUtility.ToJson(SaveState);
		File.WriteAllText(_filePath, saveData);
	}


	public void Load()
	{
		bool isSaveStateExist = File.Exists(_filePath);
		if (isSaveStateExist)
		{
			string loadData = File.ReadAllText(_filePath);
			SaveState = JsonUtility.FromJson<SaveState>(loadData);

		}
		else if (!isSaveStateExist)
		{
			SaveState = new SaveState();
			SaveState.InitSaveState();
			Save();
		}
	}

	public void SetData()
    {
		Save();
	}

	public void ResetData()
	{
		SaveState = new SaveState();
		SaveState.InitSaveState();
		Save();
	}

}
