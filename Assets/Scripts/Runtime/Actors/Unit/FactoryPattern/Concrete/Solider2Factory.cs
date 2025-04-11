
using Lean.Pool;
using UnityEngine;

public class Solider2Factory : UnitFactory
{
	[SerializeField] private Solider2 _solder2Prefab;
	public override Unit CreateUnit()
	{
		return LeanPool.Spawn(_solder2Prefab);
	}
}
