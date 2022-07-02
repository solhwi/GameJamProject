using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �������� ������, �ֱ������� ź�� �߻�
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
