using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossUnit : EnemyUnit
{
	public override void Initialize(CollisionType type)
	{
		base.Initialize(type);

		IsBoss = true;
	}
}
