using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
	private List<Singleton> managerList = new List<Singleton>();

	private Action onTriggerGameStart = null;
	private Action onTriggerGameStop = null;

	private bool isStarted = false;

	public ENUM_STAGE currStage = ENUM_STAGE.stage1;

	public static int CurrScore
	{
		get;
		private set;
	} = 0;

	public static int CurrGold
	{
		get;
		private set;
	} = 0;

	public float StageTime
	{
		get;
		private set;
	} = 0.0f;

	private void Update()
	{
		OnUpdateInstance();

		if(isStarted)
			StageTime += Time.deltaTime;
	}

	protected override void OnDestroy()
	{
		OnDestroyInstance();
	}

	public void AddScore(int score)
	{
		CurrScore += score;
	}

	public void AddGold(int gold)
	{
		CurrGold += gold;
	}

	public override void OnUpdateInstance()
	{
		foreach (var manager in managerList)
		{
			if(manager != null)
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

		Instance.StageTime = 0.0f;
		Instance.isStarted = false;
		CurrGold = 0;
		CurrScore = 0;
	}

	public static void GameStartTrigger()
	{
		Instance.onTriggerGameStart?.Invoke();
		Instance.onTriggerGameStart = null;

		Instance.isStarted = true;
		Instance.StageTime = 0.0f;
		CurrGold = 0;
		CurrScore = 0;
		Time.timeScale = 1.0f;
	}

	public static void PrevGame()
	{
		GameStopTrigger();

		SceneManager.Instance.ChangeScene(SceneManager.ENUM_SCENE.Main);
	}

	public static void NextGame()
	{
		GameStopTrigger();

		if(Instance.currStage == ENUM_STAGE.stage1)
		{
			Instance.currStage = ENUM_STAGE.stage2;
		}
		else if(Instance.currStage == ENUM_STAGE.stage2)
		{
			Instance.currStage = ENUM_STAGE.stage3;
		}
		else if(Instance.currStage == ENUM_STAGE.stage3)
		{
			Instance.currStage = ENUM_STAGE.stage4;
		}
		else if(Instance.currStage == ENUM_STAGE.stage4)
		{
			Instance.currStage = ENUM_STAGE.stage5;
		}

		SceneManager.Instance.ChangeScene(SceneManager.ENUM_SCENE.Stage);
		GameStartTrigger();
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
