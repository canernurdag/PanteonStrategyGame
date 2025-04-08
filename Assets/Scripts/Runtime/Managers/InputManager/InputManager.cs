using UnityEngine;

public sealed class InputManager : Singleton<InputManager>
{
	#region DIRECT REF
	[SerializeField] private Camera _mainCamera;
	#endregion


	#region VARIABLES
	private bool _preventInput;
	private OnLeftClickInputGiven _onLeftClickInputGiven;
	private OnRightClickInputGiven _onRightClickInputGiven;
	private OnMouseWorldPositionGiven _onMouseWorldPositionGiven;
	private OnMouseScreenPositionGive _onMouseScreenPositionGive;
	#endregion

	protected override void Awake()
	{
		base.Awake();
		if(!_mainCamera) _mainCamera = Camera.main;
	}

	private void Start()
	{
		_onLeftClickInputGiven = EventManager.Instance.GetEvent<OnLeftClickInputGiven>();
		_onRightClickInputGiven = EventManager.Instance.GetEvent<OnRightClickInputGiven>();
		_onMouseWorldPositionGiven = EventManager.Instance.GetEvent<OnMouseWorldPositionGiven>();
		_onMouseScreenPositionGive = EventManager.Instance.GetEvent<OnMouseScreenPositionGive>();
	}
	private void Update()
	{
		if (_preventInput) return;

		_onMouseScreenPositionGive.Execute(Input.mousePosition);

		var inputPos = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
		inputPos.z = 0;
		_onMouseWorldPositionGiven.Execute(inputPos);

		if (Input.GetMouseButtonDown(0))
		{
			_onLeftClickInputGiven.Execute(inputPos);
		}

		if (Input.GetMouseButtonDown(1))
		{
			_onRightClickInputGiven.Execute(inputPos);
		}

	}

	public void SetPreventInput(bool preventInput)
	{
		_preventInput = preventInput;
	}
}