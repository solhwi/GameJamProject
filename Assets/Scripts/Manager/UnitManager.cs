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
			action?.Invoke();
		}
	}

	public void ExecuteCommand(CollisionObject receiver, ActiveObject.ObjectStatus status, CommandParam param = null)
	{
		if (receiver == null)
			return;

		switch(status)
		{
			case ActiveObject.ObjectStatus.Idle:
				enemyActionQueue.Enqueue(() => { receiver.Idle(param); });
				break;

			case ActiveObject.ObjectStatus.Move:
				enemyActionQueue.Enqueue(() => { receiver.Move(param); });
				break;

			case ActiveObject.ObjectStatus.Attack:
				enemyActionQueue.Enqueue(() => { receiver.Attack(param); });
				break;

			case ActiveObject.ObjectStatus.Hit:
				enemyActionQueue.Enqueue(() => { receiver.Hit(param); });
				break;

			case ActiveObject.ObjectStatus.Die:
				enemyActionQueue.Enqueue(() => { receiver.Die(param); });
				break;
		}
	}
}
