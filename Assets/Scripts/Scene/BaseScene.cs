using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseScene : MonoBehaviour
{
	private Action OnAwakeScene = null;
	private Action OnStartScene = null;
	private Action OnEndScene = null;

	public void OnAwakeStageCallbackRegister(Action callback)
	{
		OnAwakeScene += callback;
	}

	public void OnStartStageCallbackRegister(Action callback)
	{
		OnStartScene += callback;
	}

	public void OnEndStageCallbackRegister(Action callback)
	{
		OnEndScene += callback;
	}
	public void OnAwakeStageCallbackUnegister(Action callback)
	{
		OnAwakeScene -= callback;
	}

	public void OnStartStageCallbackUnregister(Action callback)
	{
		OnStartScene -= callback;
	}

	public void OnEndStageCallbackUnregister(Action callback)
	{
		OnEndScene -= callback;
	}

	protected virtual void Awake()
	{
		OnAwakeScene?.Invoke();
	}

	protected virtual IEnumerator Start()
	{
		yield return null;
		OnStartScene?.Invoke();
	}

	public void End()
	{
		OnEndScene?.Invoke();

		OnAwakeScene = null;
		OnStartScene = null;
		OnEndScene = null;
	}
}
