using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossUnit : EnemyUnit
{
	public override void Initialize(CollisionType type, bool isTrigger = true)
	{
		base.Initialize(type, isTrigger);

		IsBoss = true;
	}
}
