using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 움직이지 않으며, 주기적으로 탄을 발사
/// </summary>

public class FriendlyUnit : ActiveObject
{
	public float cooltime = 0.0f;
	public int attackSpeed = 0;
	public int damage = 0;

	public override void Initialize(CollisionType type, bool isTrigger = true)
	{
		base.Initialize(type, isTrigger);

		tagType = ENUM_TAG_TYPE.Friendly;

		Upgrade(0);
	}

	public override void Idle(CommandParam param = null)
	{
		base.Idle(param);
	}

	public override void Attack(CommandParam param)
	{
		if(cooltime > 0.0f)
			return;

		base.Attack(param);
		cooltime = 2.0f;

		UnitManager.Instance.ExecuteCommand(this, ObjectStatus.Idle);
	}

	protected void Update()
	{
		cooltime -= Time.deltaTime * attackSpeed;
	}

	public void Upgrade(int level)
	{
		var data = ResourceManager.Instance.GetFriendlyDataByLevel(level);

		attackSpeed = data.attackSpeed;
		damage = data.damage;
	}
}
