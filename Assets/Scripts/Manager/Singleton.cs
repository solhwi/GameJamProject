using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Singleton : MonoBehaviour
{
	// ������ ����
	protected abstract void OnAwakeInstance();
	protected virtual void OnStartInstance() { }

	// ���� �Ŵ����� ��� ����
	public virtual void OnUpdateInstance() { }
	public virtual void OnDestroyInstance() { }

	// ���� ����
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