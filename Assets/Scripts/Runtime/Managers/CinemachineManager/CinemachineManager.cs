using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CinemachineManager : Singleton<CinemachineManager>
{
	#region DIRECT REF
	[SerializeField]private Animator _animator;
	#endregion
	public void SetVCam(string param)
	{
		_animator.SetTrigger(param);
	}
}