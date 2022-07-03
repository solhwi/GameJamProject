using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 움직이지 않으며, 체력을 가짐
/// </summary>

public class TowerUnit : ActiveObject
{
	public int CurrHp
	{
		get
		{
			return currHp;
		}
	}

	private void Awake()
	{
		Initialize(CollisionType.Box);
	}

	public override void Initialize(CollisionType type, bool isTrigger = true)
	{
		base.Initialize(type, false);

		tagType = ENUM_TAG_TYPE.Tower;

		TowerData data = ResourceManager.Instance.GetTowerData(id);
		currHp = data.hpMax;
	}

	public void OnTriggerEnter2D(Collider2D collision)
	{
		var enemy = collision.GetComponent<EnemyUnit>();
		if (enemy != null)
		{
			UnitManager.Instance.ExecuteCommand(enemy, ObjectStatus.Hit, new HitParam() { damage = 100000 });

			if(enemy.IsBoss)
			{
				var data = ResourceManager.Instance.GetBossData(enemy.id);
				UnitManager.Instance.ExecuteCommand(enemy, ObjectStatus.Hit, new HitParam() { damage = data.damage });
			}
			else
			{
				var data = ResourceManager.Instance.GetEnemyData(enemy.id);
				UnitManager.Instance.ExecuteCommand(enemy, ObjectStatus.Hit, new HitParam() { damage = data.damage });
			}
		}
	}
}
