using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 움직이지 않으며, 주기적으로 탄을 발사
/// </summary>

public class FriendlyUnit : ActiveObject
{
	public override void Init(CollisionType type)
	{
		base.Init(type);

		tagType = ENUM_TAG_TYPE.Friendly;
	}

	public override void Idle(CommandParam param = null)
	{
		base.Idle(param);
	}

	public override void Attack(CommandParam param)
	{
		base.Attack(param);
	}
}
