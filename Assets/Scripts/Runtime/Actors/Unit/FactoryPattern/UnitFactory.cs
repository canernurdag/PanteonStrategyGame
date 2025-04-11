using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UnitFactory : MonoBehaviour
{
	[field:SerializeField] public UnitType UnitType { get; protected set; }
    public abstract Unit CreateUnit();

}
