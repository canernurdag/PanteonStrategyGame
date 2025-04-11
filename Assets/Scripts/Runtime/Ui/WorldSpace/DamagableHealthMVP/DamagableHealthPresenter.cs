using DG.Tweening;
using Lean.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagableHealthPresenter : MonoBehaviour, IDamagable
{
	#region DIRECT REF
	[SerializeField] private DamgableHealthView _damagableHealthView;
	[SerializeField] private DamagableHealthModel _damagableHealthModel;
	[SerializeField] private InterfaceReference<IPlaceable> _placeable;
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

		float ratio = (float)_damagableHealthModel.CurrentHealth / (float)_damagableHealthModel.InitHealth;
		_damagableHealthView.HealthBar.SetBarImageFillAmount(ratio);

		if(_damagableHealthModel.CurrentHealth == 0)
		{
			//DESTROY HERE
			_placeable.Value.Deplace();
			LeanPool.Despawn(_placeable.Value.Transform);
		}
	}

	public void Damage(float damageAmount)
	{
		var newHealth = _damagableHealthModel.CurrentHealth - damageAmount;
		if (newHealth < 0) newHealth = 0;
		_damagableHealthModel.SetCurrentHealth(newHealth);
	}

	public void SetInitHealth(float health)
	{
		_damagableHealthModel.SetInitHealth(health);
	}

	public void SetCurrentHealth(float health)
	{
		_damagableHealthModel.SetCurrentHealth(health);
	}
}
