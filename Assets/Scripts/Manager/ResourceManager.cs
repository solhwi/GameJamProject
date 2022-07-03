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
	public int moveSpeed;
	public int stage;
	public int score;
	public int gold;
}

public class BossData
{
	public GameObject prefab;
	public string code; // 경로를 찾기 위한 코드 네임
	public int hpMax;
	public int damage;
	public int moveSpeed;
	public int attackSpeed;
	public int spawnTime;
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
	public List<KeyValuePair<int, int>> spawnTimeList = new List<KeyValuePair<int, int>>();
}

public class TowerData
{
	public string code;
	public int hpMax;
	public int tower_limit;
}

public class FriendlyData
{
	public int damage;
	public int attackSpeed;
	public int bulletCount;
}

public class StageWeightData
{
	public string code;
	public int damage;
	public int attack_speed;
}

public class ResourceManager : Singleton<ResourceManager>
{
	public bool IsReadyResource
	{
		get;
		private set;
	} = false;

	private readonly string URL = "https://docs.google.com/spreadsheets/d/1-BwkH4X4BDXGfYuwwJkeAZcCouOF_QwxiSrbzbVjbcs/export?format=csv&gid=";
	
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
			var code = (string)column["code"];
			var damage = (int)column["damage"];
			var hpMax = (int)column["hpMax"];
			var moveSpeed = (int)column["moveSpeed"];
			var name = (string)column["name"];
			var stage = (int)column["stage"];
			var score = (int)column["score"];
			var gold = (int)column["gold"];

			var obj = Resources.Load<GameObject>($"Prefabs/{code}");

			EnemyData _data = new EnemyData()
			{
				prefab = obj,
				code = code,
				damage = damage,
				hpMax = hpMax,
				moveSpeed = moveSpeed,
				name = name,
				stage = stage,
				score = score,
				gold = gold
			};

			int id = (int)column["id"];
			
			if(!enemyDictionary.ContainsKey((ObjectID)id))
				enemyDictionary.Add((ObjectID)id, _data);
		}

		// 시트 2. 보스 데이터

		request = UnityWebRequest.Get(URL + bossDataCode);
		yield return request.SendWebRequest();

		dataHandler = request.downloadHandler;
		sheetList = CSVReader.Read(dataHandler.text);

		foreach (var column in sheetList)
		{
			BossData _data = new BossData()
			{
				code = (string)column["code"],
				damage = (int)column["damage"],
				hpMax = (int)column["hpMax"],
				moveSpeed = (int)column["moveSpeed"],
				attackSpeed = (int)column["attack_speed"],
				spawnTime = (int)column["boss_spawn_timing"]
			};

			int id = (int)column["id"];

			if (!bossDictionary.ContainsKey((ObjectID)id))
				bossDictionary.Add((ObjectID)id, _data);
		}

		// 시트 3. 타워 데이터
		request = UnityWebRequest.Get(URL + towerDataCode);
		yield return request.SendWebRequest();

		dataHandler = request.downloadHandler;
		sheetList = CSVReader.Read(dataHandler.text);

		foreach (var column in sheetList)
		{
			TowerData _data = new TowerData()
			{
				code = (string)column["code"],
				hpMax = (int)column["hpMax"],
				tower_limit= (int)column["tower_limit"],
			};

			int id = (int)column["id"];

			if (!towerDictionary.ContainsKey((ObjectID)id))
				towerDictionary.Add((ObjectID)id, _data);
		}

		// 시트 4. 아군 데이터
		request = UnityWebRequest.Get(URL + friendlyDataCode);
		yield return request.SendWebRequest();

		dataHandler = request.downloadHandler;
		sheetList = CSVReader.Read(dataHandler.text);

		foreach (var column in sheetList)
		{
			FriendlyData _data = new FriendlyData()
			{
				damage = (int)column["damage"],
				attackSpeed = (int)column["attack_speed"],
				bulletCount = (int)column["bullet"],
			};

			int id = (int)column["id"];

			if (!friendlyDictionaryByLevel.ContainsKey(id))
				friendlyDictionaryByLevel.Add(id, _data);
		}

		// 시트 5. 아군 레벨업 가중치 데이터
		request = UnityWebRequest.Get(URL + stageWeightCode);
		yield return request.SendWebRequest();

		dataHandler = request.downloadHandler;
		sheetList = CSVReader.Read(dataHandler.text);

		foreach (var column in sheetList)
		{
			StageWeightData _data = new StageWeightData()
			{
				code = (string)column["code"],
				damage = (int)column["damage"],
				attack_speed = (int)column["attack_speed"],
			};

			int id = (int)column["id"];

			if (!stageWeightDictionary.ContainsKey((ENUM_STAGE)id))
				stageWeightDictionary.Add((ENUM_STAGE)id, _data);
		}

		// 시트 6
		request = UnityWebRequest.Get(URL + stage1TimeCode);
		yield return request.SendWebRequest();

		dataHandler = request.downloadHandler;
		sheetList = CSVReader.Read(dataHandler.text);

		StageData data = new StageData();

		foreach (var column in sheetList)
		{
			int timing = (int)column["timing"];
			int id = (int)column["id"];

			data.spawnTimeList.Add(new KeyValuePair<int, int>(timing, id));
		}

		if (!stageTimingDictionary.ContainsKey(ENUM_STAGE.stage1))
			stageTimingDictionary.Add(ENUM_STAGE.stage1, data);

		// 시트 7
		request = UnityWebRequest.Get(URL + stage2TimeCode);
		yield return request.SendWebRequest();

		dataHandler = request.downloadHandler;
		sheetList = CSVReader.Read(dataHandler.text);

		data = new StageData();

		foreach (var column in sheetList)
		{
			int timing = (int)column["timing"];
			int id = (int)column["id"];

			data.spawnTimeList.Add(new KeyValuePair<int, int>(timing, id));
		}

		if (!stageTimingDictionary.ContainsKey(ENUM_STAGE.stage2))
			stageTimingDictionary.Add(ENUM_STAGE.stage2, data);

		// 시트 8
		request = UnityWebRequest.Get(URL + stage3TimeCode);
		yield return request.SendWebRequest();

		dataHandler = request.downloadHandler;
		sheetList = CSVReader.Read(dataHandler.text);

		data = new StageData();

		foreach (var column in sheetList)
		{
			int timing = (int)column["timing"];
			int id = (int)column["id"];

			data.spawnTimeList.Add(new KeyValuePair<int, int>(timing, id));
		}

		if (!stageTimingDictionary.ContainsKey(ENUM_STAGE.stage3))
			stageTimingDictionary.Add(ENUM_STAGE.stage3, data);

		// 시트 9
		request = UnityWebRequest.Get(URL + stage4TimeCode);
		yield return request.SendWebRequest();

		dataHandler = request.downloadHandler;
		sheetList = CSVReader.Read(dataHandler.text);

		data = new StageData();

		foreach (var column in sheetList)
		{
			int timing = (int)column["timing"];
			int id = (int)column["id"];

			data.spawnTimeList.Add(new KeyValuePair<int, int>(timing, id));
		}

		if (!stageTimingDictionary.ContainsKey(ENUM_STAGE.stage4))
			stageTimingDictionary.Add(ENUM_STAGE.stage4, data);

		// 시트 10
		request = UnityWebRequest.Get(URL + stage5TimeCode);
		yield return request.SendWebRequest();

		dataHandler = request.downloadHandler;
		sheetList = CSVReader.Read(dataHandler.text);

		data = new StageData();

		foreach (var column in sheetList)
		{
			int timing = (int)column["timing"];
			int id = (int)column["id"];

			data.spawnTimeList.Add(new KeyValuePair<int, int>(timing, id));
		}

		if (!stageTimingDictionary.ContainsKey(ENUM_STAGE.stage5))
			stageTimingDictionary.Add(ENUM_STAGE.stage5, data);

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

	public FriendlyData GetFriendlyDataByLevel(int level)
	{
		if(friendlyDictionaryByLevel.ContainsKey(level))
		{
			return friendlyDictionaryByLevel[level];
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
