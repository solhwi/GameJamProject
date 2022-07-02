using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Singleton : MonoBehaviour
{
	// 본인이 수행
	protected abstract void OnAwakeInstance();
	protected virtual void OnStartInstance() { }

	// 게임 매니저가 대신 수행
	public virtual void OnUpdateInstance() { }
	public virtual void OnDestroyInstance() { }

	// 직접 수행
	public abstract void Initialize();
	public abstract void Clear();
}

public abstract class Singleton<T> : Singleton where T : Singleton
{
	public bool IsInitialized
	{
		get;
		private set;
	} = false;

	public static T Instance 
	{ 
		get
		{
			if(instance == null)
			{
				var g = new GameObject(typeof(T).ToString());
				DontDestroyOnLoad(g);
				instance = g.AddComponent<T>();
				instance.Initialize();
				return instance;
			}

			return instance;
		}
	}

	private static T instance;

	public override void Initialize()
	{
		if (IsInitialized)
			return;

		GameManager.RegisterManager(this);
		IsInitialized = true;
	}

	public override void Clear()
	{
		GameManager.UnregisterManager(this);
		IsInitialized = false;
	}

	protected virtual void Awake()
	{
		OnAwakeInstance();
	}

	protected virtual IEnumerator Start()
	{
		yield return null;
		OnStartInstance();
	}

	protected virtual void OnDestroy()
	{
		Clear();
	}
}