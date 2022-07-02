using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 움직이지 않으며, 체력을 가짐
/// </summary>

public class TowerUnit : ActiveObject
{
	public override void Init(CollisionType type)
	{
		base.Init(type);

		tagType = ENUM_TAG_TYPE.Tower;
	}
}
