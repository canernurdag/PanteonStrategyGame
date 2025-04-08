using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class GameManager : Singleton<GameManager>
{
	public State CurrentState { get; private set; }

	protected override void Awake()
	{
		base.Awake();
		Application.targetFrameRate = 90;
		QualitySettings.vSyncCount = 0;

	}

	public void SetCurrentState(State state)
	{
		CurrentState = state;
		EventManager.Instance.GetEvent<OnGameStateChanged>().Execute(state);

	}

	public enum State
	{
		Menu,
		Gameplay,
		GameEnding
	}
}