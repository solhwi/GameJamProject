using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class EnemyData
{
	public GameObject prefab;
	public string code; // ��θ� ã�� ���� �ڵ� ����
	public string name;
	public int hpMax;
	public int damage;
	public float moveSpeed;
	public int stage;
}

public class BossData
{
	public GameObject prefab;
	public string code; // ��θ� ã�� ���� �ڵ� ����
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
	public string code;
	public int hpMax;
	public int tower_limit;
}

public class FriendlyData
{
	public int damage;
	public float attackSpeed;
	public int bulletCount;
}

public class StageWeightData
{
	public string code;
	public int damage;
	public float attack_speed;
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

		// ��Ʈ 1. �� ������

		UnityWebRequest request = UnityWebRequest.Get(URL + enemyDataCode);
		yield return request.SendWebRequest();

		DownloadHandler dataHandler = request.downloadHandler;

		var sheetList = CSVReader.Read(dataHandler.text);

		foreach(var column in sheetList)
		{
			EnemyData _data = new EnemyData()
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
				enemyDictionary.Add((ObjectID)id, _data);
		}

		// ��Ʈ 2. ���� ������

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
				moveSpeed = (float)column["moveSpeed"],
				attackSpeed = (float)column["attack_speed"],
				spawnTime = (float)column["boss_spawn_timing"]
			};

			int id = (int)column["id"];

			if (!bossDictionary.ContainsKey((ObjectID)id))
				bossDictionary.Add((ObjectID)id, _data);
		}

		// ��Ʈ 3. Ÿ�� ������
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

		// ��Ʈ 4. �Ʊ� ������
		request = UnityWebRequest.Get(URL + friendlyDataCode);
		yield return request.SendWebRequest();

		dataHandler = request.downloadHandler;
		sheetList = CSVReader.Read(dataHandler.text);

		foreach (var column in sheetList)
		{
			FriendlyData _data = new FriendlyData()
			{
				damage = (int)column["damage"],
				attackSpeed = (float)column["attack_speed"],
				bulletCount = (int)column["bullet"],
			};

			int id = (int)column["id"];

			if (!friendlyDictionaryByLevel.ContainsKey(id))
				friendlyDictionaryByLevel.Add(id, _data);
		}

		// ��Ʈ 5. �Ʊ� ������ ����ġ ������
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

		// ��Ʈ 6
		request = UnityWebRequest.Get(URL + stage1TimeCode);
		yield return request.SendWebRequest();

		dataHandler = request.downloadHandler;
		sheetList = CSVReader.Read(dataHandler.text);

		StageData data = new StageData();

		foreach (var column in sheetList)
		{
			float timing = (float)column["timing"];
			int id = (int)column["id"];

			data.spawnTimeList.Add(new KeyValuePair<float, int>(timing, id));
		}

		if (!stageTimingDictionary.ContainsKey(ENUM_STAGE.stage1))
			stageTimingDictionary.Add(ENUM_STAGE.stage1, data);

		// ��Ʈ 7
		request = UnityWebRequest.Get(URL + stage2TimeCode);
		yield return request.SendWebRequest();

		dataHandler = request.downloadHandler;
		sheetList = CSVReader.Read(dataHandler.text);

		data = new StageData();

		foreach (var column in sheetList)
		{
			float timing = (float)column["timing"];
			int id = (int)column["id"];

			data.spawnTimeList.Add(new KeyValuePair<float, int>(timing, id));
		}

		if (!stageTimingDictionary.ContainsKey(ENUM_STAGE.stage2))
			stageTimingDictionary.Add(ENUM_STAGE.stage2, data);

		// ��Ʈ 8
		request = UnityWebRequest.Get(URL + stage3TimeCode);
		yield return request.SendWebRequest();

		dataHandler = request.downloadHandler;
		sheetList = CSVReader.Read(dataHandler.text);

		data = new StageData();

		foreach (var column in sheetList)
		{
			float timing = (float)column["timing"];
			int id = (int)column["id"];

			data.spawnTimeList.Add(new KeyValuePair<float, int>(timing, id));
		}

		if (!stageTimingDictionary.ContainsKey(ENUM_STAGE.stage3))
			stageTimingDictionary.Add(ENUM_STAGE.stage3, data);

		// ��Ʈ 9
		request = UnityWebRequest.Get(URL + stage4TimeCode);
		yield return request.SendWebRequest();

		dataHandler = request.downloadHandler;
		sheetList = CSVReader.Read(dataHandler.text);

		data = new StageData();

		foreach (var column in sheetList)
		{
			float timing = (float)column["timing"];
			int id = (int)column["id"];

			data.spawnTimeList.Add(new KeyValuePair<float, int>(timing, id));
		}

		if (!stageTimingDictionary.ContainsKey(ENUM_STAGE.stage4))
			stageTimingDictionary.Add(ENUM_STAGE.stage4, data);

		// ��Ʈ 10
		request = UnityWebRequest.Get(URL + stage5TimeCode);
		yield return request.SendWebRequest();

		dataHandler = request.downloadHandler;
		sheetList = CSVReader.Read(dataHandler.text);

		data = new StageData();

		foreach (var column in sheetList)
		{
			float timing = (float)column["timing"];
			int id = (int)column["id"];

			data.spawnTimeList.Add(new KeyValuePair<float, int>(timing, id));
		}

		if (!stageTimingDictionary.ContainsKey(ENUM_STAGE.stage5))
			stageTimingDictionary.Add(ENUM_STAGE.stage5, data);

		// code�� ���� ���ҽ� �ε�

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
