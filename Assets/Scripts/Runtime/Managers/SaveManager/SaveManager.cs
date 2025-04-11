using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-10)]
public class SaveManager : Singleton<SaveManager>
{
	public ISaveSystem SaveSystem { get; private set; }

	protected override void Awake()
	{
		base.Awake();
		SaveSystem = GetComponent<ISaveSystem>();
	}

	private void OnApplicationFocus(bool focus)
	{
		if (!focus) SaveSystem.Save();
	}

	private void OnApplicationPause(bool pause)
	{
		if (pause) SaveSystem.Save();

	}

	private void OnApplicationQuit()
	{
		SaveSystem.Save();
	}
}
