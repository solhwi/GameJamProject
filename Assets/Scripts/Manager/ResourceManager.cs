using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MonsterData
{
	public string name;
	public int hpMax;
	public int damage;
	public float moveSpeed;
	public bool isAnimation;
	public int stage;
}

public class BossData
{
	public string name;
	public int hpMax;
	public int damage;
	public float moveSpeed;
	public float attackSpeed;
	public float spawnTime;
}

public class StageData
{
	public List<float> spawnTimeList = new List<float>();
}

public class ResourceManager : Singleton<ResourceManager>
{
	public bool IsReadyResource
	{
		get;
		private set;
	} = false;

	private readonly string URL = "";
	private DownloadHandler dataHandler = null;

	protected override IEnumerator Start()
	{
		yield return base.Start();

		UnityWebRequest request = UnityWebRequest.Get(URL);
		yield return request.SendWebRequest();

		dataHandler = request.downloadHandler;
	}

	public T Load<T>(string path) where T : Object
	{
		return Resources.Load<T>(path);
	}

	public void Unload(Object obj)
	{
		Resources.UnloadAsset(obj);
	}
}
