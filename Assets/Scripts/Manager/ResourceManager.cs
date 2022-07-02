using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MonsterData
{
	public GameObject prefab;
	public string code; // 경로를 찾기 위한 코드 네임
	public string name;
	public int hpMax;
	public int damage;
	public float moveSpeed;
	public bool isAnimation;
	public int stage;
}

public class BossData
{
	public GameObject prefab;
	public string code; // 경로를 찾기 위한 코드 네임
	public int hpMax;
	public int damage;
	public float moveSpeed;
	public float attackSpeed;
	public float spawnTime;
}

public enum ENUM_STAGE
{
	stage1 = 0,
	stage2 = 1,
	stage3 = 2,
	stage4 = 3,
	stage5 = 4,
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

	private Dictionary<ActiveObjectID, MonsterData> monsterDictionary = new Dictionary<ActiveObjectID, MonsterData>();
	private Dictionary<ActiveObjectID, BossData> bossDictionary = new Dictionary<ActiveObjectID, BossData>();
	private Dictionary<ENUM_STAGE, StageData> stageDictionary = new Dictionary<ENUM_STAGE, StageData>();

	protected override IEnumerator Start()
	{
		yield return base.Start();

		UnityWebRequest request = UnityWebRequest.Get(URL);
		yield return request.SendWebRequest();

		dataHandler = request.downloadHandler;

		// 딕셔너리 제작

		// code를 통해 리소스 로드

		IsReadyResource = true;
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
