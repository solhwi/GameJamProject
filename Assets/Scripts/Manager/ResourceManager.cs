using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class EnemyData
{
	public GameObject prefab;
	public string code; // 경로를 찾기 위한 코드 네임
	public string name;
	public int hpMax;
	public int damage;
	public float moveSpeed;
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
	public List<KeyValuePair<float, int>> spawnTimeList = new List<KeyValuePair<float, int>>();
}

public class TowerData
{
	public int hpMax;
	public int maxSlotCount;
}

public class FriendlyData
{
	public int damage;
	public float attackSpeed;
	public int bulletCount;
}

public class StageWeightData
{

}

public class ResourceManager : Singleton<ResourceManager>
{
	public bool IsReadyResource
	{
		get;
		private set;
	} = false;

	private readonly string URL = "https://docs.google.com/spreadsheets/d/1-BwkH4X4BDXGfYuwwJkeAZcCouOF_QwxiSrbzbVjbcs/Export?format=csv&gid=";
	
	private readonly uint enemyDataCode = 0;
	private readonly uint bossDataCode = 283066001;
	private readonly uint towerDataCode = 2026227721;
	private readonly uint friendlyDataCode = 666204270;
	private readonly uint stageWeightCode = 1521888164;
	private readonly uint stage1TimeCode = 1045025113;
	private readonly uint stage2TimeCode = 1631108869;
	private readonly uint stage3TimeCode = 1930370419;
	private readonly uint stage4TimeCode = 1707643071;
	private readonly uint stage5TimeCode = 1797237857;

	private Dictionary<ObjectID, EnemyData> enemyDictionary = new Dictionary<ObjectID, EnemyData>();
	private Dictionary<ObjectID, BossData> bossDictionary = new Dictionary<ObjectID, BossData>();
	private Dictionary<ENUM_STAGE, StageData> stageTimingDictionary = new Dictionary<ENUM_STAGE, StageData>();
	private Dictionary<ObjectID, TowerData> towerDictionary = new Dictionary<ObjectID, TowerData>();
	private Dictionary<ENUM_STAGE, StageWeightData> stageWeightDictionary = new Dictionary<ENUM_STAGE, StageWeightData>();
	private Dictionary<int, FriendlyData> friendlyDictionaryByLevel = new Dictionary<int, FriendlyData>();

	protected override IEnumerator Start()
	{
		yield return base.Start();

		// 시트 1. 적 데이터

		UnityWebRequest request = UnityWebRequest.Get(URL + enemyDataCode);
		yield return request.SendWebRequest();

		DownloadHandler dataHandler = request.downloadHandler;

		var sheetList = CSVReader.Read(dataHandler.text);

		foreach(var column in sheetList)
		{
			EnemyData data = new EnemyData()
			{
				code = (string)column["code"],
				damage = (int)column["damage"],
				hpMax = (int)column["hpMax"],
				moveSpeed = (float)column["moveSpeed"],
				name = (string)column["name"],
				stage = (int)column["stage"]
			};

			int id = (int)column["id"];
			
			if(!enemyDictionary.ContainsKey((ObjectID)id))
				enemyDictionary.Add((ObjectID)id, data);
		}

		// 시트 2. 보스 데이터

		request = UnityWebRequest.Get(URL + bossDataCode);
		yield return request.SendWebRequest();

		dataHandler = request.downloadHandler;
		sheetList = CSVReader.Read(dataHandler.text);

		foreach (var column in sheetList)
		{
			BossData data = new BossData()
			{
				code = (string)column["code"],
				damage = (int)column["damage"],
				hpMax = (int)column["hpMax"],
				moveSpeed = (float)column["moveSpeed"],
				attackSpeed = (float)column["attack_speed"],
				spawnTime = (float)column["boss_spawn_timing"]
			};

			int id = (int)column["id"];

			if (!bossDictionary.ContainsKey((ObjectID)id))
				bossDictionary.Add((ObjectID)id, data);
		}

		// 시트 3. 타워 데이터
		// 시트 4. 아군 데이터
		// 시트 5. 아군 레벨업 가중치 데이터
		// 시트 6 ~ 10. 스테이지 1 ~ 5 데이터

		// code를 통해 리소스 로드

		IsReadyResource = true;
	}

	public EnemyData GetEnemyData(ObjectID id)
	{
		EnemyData data;

		if(enemyDictionary.TryGetValue(id, out data))
		{
			return data;
		}

		return null;
	}

	public BossData GetBossData(ObjectID id)
	{
		BossData data;

		if(bossDictionary.TryGetValue(id, out data))
		{
			return data;
		}

		return null;
	}

	public StageData GetStageData(ENUM_STAGE stage)
	{
		StageData data;

		if(stageTimingDictionary.TryGetValue(stage, out data))
		{
			return data;
		}

		return null;
	}

	public TowerData GetTowerData(ObjectID id)
	{
		TowerData data;

		if(towerDictionary.TryGetValue(id, out data))
		{
			return data;
		}

		return null;
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
