using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagableHealthPresenter : MonoBehaviour, IDamagable
{
	#region DIRECT REF
	[SerializeField] private DamgableHealthView _damagableHealthView;
	[SerializeField] private DamagableHealthModel _damagableHealthModel;
	#endregion

	#region REF
	private OnDamagableHealthChanged _onDamagableHealthChanged;
	public Transform Transform => transform;
	#endregion

	private void Start()
	{
		_onDamagableHealthChanged = EventManager.Instance.GetEvent<OnDamagableHealthChanged>();
		_onDamagableHealthChanged.AddListener(HandleBuildingHealthChange);
	}

	private void OnDestroy()
	{
		_onDamagableHealthChanged.RemoveListener(HandleBuildingHealthChange);
	}

	public void HandleBuildingHealthChange(DamagableHealthModel model)
	{
		if (model != _damagableHealthModel) return;

		//TODO
	}
}
