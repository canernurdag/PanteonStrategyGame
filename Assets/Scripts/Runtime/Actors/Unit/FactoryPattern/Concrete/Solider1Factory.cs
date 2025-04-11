using Lean.Pool;
using System;
using UnityEngine;

public class Solider1Factory : UnitFactory
{
	[SerializeField] private Solider1 _solder1Prefab;
	public override Unit CreateUnit()
	{
		return LeanPool.Spawn(_solder1Prefab);
	}
}
