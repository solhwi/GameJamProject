using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 움직이지 않으며, EnemyUnit를 인식하여 방어하는 유닛
/// </summary>

public class FriendlyUnit : ActiveObject
{
	public override void Init(CollisionType type)
	{
		base.Init(type);
	}
}
