using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : Singleton<UnitManager>
{
	public bool IsSpawnEnded
	{
		get;
		private set;
	} = false;


	public int MyTowerHp
	{
		get
		{
			if (towerUnit == null)
				return 0;

			return towerUnit.CurrHp;
		}
	}

	public int MyTowerMaxHp
    {
		get
        {
			if (towerUnit == null)
				return 1;

			var data = ResourceManager.Instance.GetTowerData(towerUnit.id);

			if(data == null)
			{
				return 1;
			}
			return data.hpMax;
        }
    }

	private Queue<Action> enemyActionQueue = new Queue<Action>();
	private List<CollisionObject> objectList = new List<CollisionObject>();

	private Vector2 spawnPos = new Vector2(7.0f, -1.5f);
	private List<KeyValuePair<int, int>> spawnSequence = new List<KeyValuePair<int, int>>();

	private TowerUnit towerUnit = null;
	private FriendlyUnit friendlyUnit = null;

	public void Spawn()
	{
		Initialize();

		towerUnit = null;
		objectList = new List<CollisionObject>();
		enemyActionQueue = new Queue<Action>();
		spawnSequence = new List<KeyValuePair<int, int>>();

		var stageData = ResourceManager.Instance.GetStageData(GameManager.Instance.currStage);
		spawnSequence.AddRange(stageData.spawnTimeList);

		towerUnit = FindObjectOfType<TowerUnit>();
		friendlyUnit = FindObjectOfType<FriendlyUnit>();
		ExecuteCommand(friendlyUnit, CollisionObject.ObjectStatus.Attack);

		GameManager.RegisterStartTrigger(() => { Instance.Initialize(); });
		GameManager.RegisterStopTrigger(() => { DestroyImmediate(gameObject); });
	}

	public FriendlyUnit GetFriendlyUnit()
	{
		return friendlyUnit;
	}

	public override void OnUpdateInstance()
	{
		for(int i = 0; i < spawnSequence.Count; i++)
		{
			float time = spawnSequence[i].Key;
			int id = spawnSequence[i].Value;

			if (time <= GameManager.Instance.StageTime)
			{
				var data = ResourceManager.Instance.GetEnemyData((ObjectID)id);
				var g = Instantiate(data.prefab, spawnPos, new Quaternion());
				var unit = g.GetComponent<EnemyUnit>();
				unit.Initialize(CollisionObject.CollisionType.Box);
				spawnSequence.RemoveAt(i--);
			}
		}

		while (enemyActionQueue.Count > 0)
		{
			var action = enemyActionQueue.Dequeue();
			if (action == null)
				continue;

			action.Invoke();
		}

		foreach(var obj in objectList)
		{
			if (obj == null)
				continue;

			if (obj is EnemyUnit)
			{
				var enemy = obj as EnemyUnit;

				if (enemy.IsBoss)
				{
					var data = ResourceManager.Instance.GetBossData(obj.id);
					ExecuteCommand(obj, CollisionObject.ObjectStatus.Move, new MoveParam() { speed = data.moveSpeed });
				}
				else
				{
					var data = ResourceManager.Instance.GetEnemyData(obj.id);
					ExecuteCommand(obj, CollisionObject.ObjectStatus.Move, new MoveParam() { speed = data.moveSpeed });
				}
			}
			else if (obj is FriendlyUnit)
			{
				var friendly = obj as FriendlyUnit;

				ExecuteCommand(obj, CollisionObject.ObjectStatus.Attack);
			}
		}

		if (towerUnit == null)
			return;

		if(towerUnit.CurrHp <= 0)
		{
			GameManager.PrevGame();
		}
	}

	public void ExecuteCommand(CollisionObject receiver, CollisionObject.ObjectStatus status, CommandParam param = null)
	{
		if (receiver == null)
			return;

		if(!objectList.Contains(receiver))
			objectList.Add(receiver);

		switch (status)
		{
			case CollisionObject.ObjectStatus.Idle:
				enemyActionQueue.Enqueue(() => { receiver.Idle(param); });
				break;

			case CollisionObject.ObjectStatus.Move:
				enemyActionQueue.Enqueue(() => { receiver.Move(param); });
				break;

			case CollisionObject.ObjectStatus.Attack:
				enemyActionQueue.Enqueue(() => { receiver.Attack(param); });
				break;

			case CollisionObject.ObjectStatus.Hit:
				enemyActionQueue.Enqueue(() => { receiver.Hit(param); });
				break;

			case CollisionObject.ObjectStatus.Die:
				enemyActionQueue.Enqueue(() => { receiver.Die(param); });
				break;
		}
	}
}
