using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �쿡�� �·� �����̴� ����, �Ʊ� ������ �������� ������, Ÿ���� ���� ������
/// </summary>

public abstract class EnemyUnit : ActiveObject
{
	public bool IsBoss = false;
	public override void Init(CollisionType type)
	{
		base.Init(type);

		tagType = ENUM_TAG_TYPE.Enemy;

		var data = ResourceManager.Instance.GetEnemyData(id);
		if(data != null)
			currHp = data.hpMax;

	}

	public void OnTriggerEnter2D(Collider2D collision)
	{
		var obj = collision.GetComponent<CollisionObject>();

		if (obj != null)
		{
			// Enemy Unit�� Ÿ���� ���� �� ����
			if (obj.tagType == ENUM_TAG_TYPE.Tower)
			{
				var data = ResourceManager.Instance.GetEnemyData(id);
				var hitParam = new HitParam() { damage = data.damage };

				UnitManager.Instance.ExecuteCommand(obj, ObjectStatus.Hit, hitParam);
				obj.Hit(hitParam);
			}
			// Enemy Unit�� �Ѿ˿��� ���� �� ����
			else if(obj.tagType == ENUM_TAG_TYPE.Friendly)
			{
				var data = ResourceManager.Instance.GetEnemyData(obj.id);
				var hitParam = new HitParam() { damage = data.damage };

				UnitManager.Instance.ExecuteCommand(this, ObjectStatus.Hit, hitParam);
			}
		}
	}
}
