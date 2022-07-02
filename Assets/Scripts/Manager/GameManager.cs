using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
	private List<Singleton> managerList = new List<Singleton>();

	private void Update()
	{
		OnUpdateInstance();
	}

	protected override void OnDestroy()
	{
		OnDestroyInstance();
	}

	protected override void OnAwakeInstance()
	{
		
	}

	protected override void OnStartInstance()
	{

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
}
