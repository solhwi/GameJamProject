using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �������� ������, ü���� ����
/// </summary>

public class TowerUnit : ActiveObject
{
	public override void Initialize(CollisionType type)
	{
		base.Initialize(type);

		tagType = ENUM_TAG_TYPE.Tower;
	}
}
