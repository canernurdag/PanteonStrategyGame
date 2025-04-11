using System;
using UnityEngine;

public interface IUnitMovable : IMonobehaviour
{
	void Move(Vector3 targetPosition, float duration, Action callback = null);

	void Move(Vector3[] path, float duration, Action callback = null);
}
