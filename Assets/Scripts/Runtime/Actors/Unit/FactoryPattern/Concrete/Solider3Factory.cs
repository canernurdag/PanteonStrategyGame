
using Lean.Pool;
using UnityEngine;

public class Solider3Factory : UnitFactory
{
	[SerializeField] private Solider3 _solder3Prefab;
	public override Unit CreateUnit()
	{
		return LeanPool.Spawn(_solder3Prefab);
	}
}
