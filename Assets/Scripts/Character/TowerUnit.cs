using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �������� ������, ü���� ����
/// </summary>

public class TowerUnit : ActiveObject
{
	public override void Init(CollisionType type)
	{
		base.Init(type);

		tagType = ENUM_TAG_TYPE.Tower;
	}
}
