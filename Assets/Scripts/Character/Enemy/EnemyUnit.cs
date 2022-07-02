using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 우에서 좌로 움직이는 유닛, 아군 유닛을 공격하지 않으며, 타워를 향해 움직임
/// </summary>

public abstract class EnemyUnit : ActiveObject
{
	public override void Init(CollisionType type)
	{
		base.Init(type);

		tagType = ENUM_TAG_TYPE.Enemy;

		var data = ResourceManager.Instance.GetEnemyData(id);
		if(data != null)
			currHp = data.hpMax;

	}

	public override void Move(CommandParam param)
	{
		base.Move(param);
	}

	public void OnTriggerEnter2D(Collider2D collision)
	{
		var obj = collision.GetComponent<CollisionObject>();

		if (obj != null)
		{
			// Enemy Unit은 타워만 때릴 수 있음
			if (obj.tagType == ENUM_TAG_TYPE.Tower)
			{
				var data = ResourceManager.Instance.GetEnemyData(id);
				var hitParam = new HitParam() { damage = data.damage };

				UnitManager.Instance.ExecuteCommand(obj, ObjectStatus.Hit, hitParam);
				obj.Hit(hitParam);
			}
			// Enemy Unit은 총알에게 맞을 수 있음
			else if(obj.tagType == ENUM_TAG_TYPE.Bullet)
			{
				var data = ResourceManager.Instance.GetEnemyData(obj.id);
				var hitParam = new HitParam() { damage = data.damage };

				UnitManager.Instance.ExecuteCommand(this, ObjectStatus.Hit, hitParam);
			}
		}
	}
}
