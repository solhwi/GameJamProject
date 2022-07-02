using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
	private List<Singleton> managerList = new List<Singleton>();

	private Action onTriggerGameStart = null;
	private Action onTriggerGameStop = null;

	public ENUM_STAGE currStage = ENUM_STAGE.stage1;

	public float StageTime
	{
		get;
		private set;
	} = 0.0f;

	private void Update()
	{
		OnUpdateInstance();

		StageTime += Time.deltaTime;
	}

	protected override void OnDestroy()
	{
		OnDestroyInstance();
	}

	public override void OnUpdateInstance()
	{
		foreach (var manager in managerList)
		{
			manager?.OnUpdateInstance();
		}
	}

	public override void OnDestroyInstance()
	{
		foreach (var manager in managerList)
		{
			if (manager != null)
			{
				manager.OnDestroyInstance();
			}
		}
	}

	public static void RegisterManager(Singleton manager)
	{
		if (manager.Equals(Instance))
			return;

		if (!Instance.managerList.Contains(manager))
		{
			Instance.managerList.Add(manager);
		}	
	}

	public static void UnregisterManager(Singleton manager)
	{
		if (Instance.managerList.Contains(manager))
		{
			Instance.managerList.Remove(manager);
		}
	}

	public static void GameStopTrigger()
	{
		Instance.onTriggerGameStop?.Invoke();
		Instance.onTriggerGameStop = null;

		Time.timeScale = 0.125f;
	}

	public static void GameStartTrigger()
	{
		Instance.onTriggerGameStart?.Invoke();
		Instance.onTriggerGameStart = null;

		Time.timeScale = 1.0f;
	}

	public static void RegisterStartTrigger(Action callback)
	{
		Instance.onTriggerGameStart += callback;
	}

	public static void RegisterStopTrigger(Action callback)
	{
		Instance.onTriggerGameStop += callback;
	}
}
