
using Lean.Pool;
using UnityEngine;

public class BarracksFactory : BuildingFactory
{
	#region DIRECT REF
	[SerializeField] private Barrack _barrackPrefab;
	#endregion
	public override Building CreateBuilding()
	{
		
		return LeanPool.Spawn(_barrackPrefab, transform);
	}
}
