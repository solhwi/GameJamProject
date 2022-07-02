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

	private Queue<Action> enemyActionQueue = new Queue<Action>();

	public override void Initialize()
	{
		base.Initialize();

		// 여기서 리소스 딕셔너리를 가져와서 거기 데이터에 맞춰 적을 스폰해준다. 
	}

	public override void OnUpdateInstance()
	{
		while(enemyActionQueue.Count > 0)
		{
			var action = enemyActionQueue.Dequeue();
			if (action == null)
				continue;

			action.Invoke();

			var obj = action.Target as CollisionObject;
			if (obj == null)
				continue;

			if (obj is EnemyUnit)
			{
				var enemy = obj as EnemyUnit;
				
				if(enemy.IsBoss)
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
			else if(obj is FriendlyUnit)
			{
				ExecuteCommand(obj, CollisionObject.ObjectStatus.Idle);
			}
		}
	}

	public void ExecuteCommand(CollisionObject receiver, CollisionObject.ObjectStatus status, CommandParam param = null)
	{
		if (receiver == null)
			return;

		switch(status)
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
