using System;
using UnityEngine;

public interface IBullet :IMonobehaviour
{
	void Throw(Vector3 initPost, Vector3 targetPos, float speed, Action callback);
}
