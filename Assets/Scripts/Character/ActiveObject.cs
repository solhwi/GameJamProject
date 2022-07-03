using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveObject : CollisionObject
{
	// 로드하지 않고 직접 세팅하는 방법 채택
	SpriteRenderer spriteRenderer = null;

	public override void Initialize(CollisionType type, bool isTrigger = true)
	{
		base.Initialize(type, isTrigger);

		spriteRenderer = GetComponent<SpriteRenderer>();
		spriteRenderer.sortingOrder = 1;
	}

	public override void Idle(CommandParam param = null)
	{
		base.Idle(param);

		spriteRenderer.color = Color.white;
	}

	public override void Move(CommandParam param)
	{
		base.Move(param);

		spriteRenderer.color = Color.white;
	}

	public override void Attack(CommandParam param)
	{
		base.Attack(param);

		spriteRenderer.color = Color.white;
	}

	public override void Hit(CommandParam param)
	{
		base.Hit(param);

		spriteRenderer.color = Color.red;
	}

	public override void Die(CommandParam param)
	{
		base.Die(param);

		spriteRenderer.color = Color.white;
	}


}
