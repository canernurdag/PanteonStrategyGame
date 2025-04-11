using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagableHealthModel : MonoBehaviour
{
	#region REF
	private OnDamagableHealthChanged _onDamagableHealthChanged;
	#endregion


	#region INTERNAL VAR
	public float InitHealth { get; private set; }
    public float CurrentHealth { get; private set; }
	#endregion


	private void Start()
	{
		_onDamagableHealthChanged = EventManager.Instance.GetEvent<OnDamagableHealthChanged>();
	}

	public void SetInitHealth(float initHealth)
    {
        InitHealth = initHealth;
    }

    public void SetCurrentHealth(float currentHealth)
    {
		CurrentHealth = currentHealth;
		_onDamagableHealthChanged.Execute(this);
    }
}
