using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveObject : CollisionObject
{
	// 로드하지 않고 직접 세팅하는 방법 채택
	[SerializeField] Animator anim = null;
	SpriteRenderer spriteRenderer = null;

	public override void Initialize(CollisionType type)
	{
		base.Initialize(type);

		spriteRenderer = GetComponent<SpriteRenderer>();
		spriteRenderer.sortingOrder = 1;
	}

	public override void Idle(CommandParam param = null)
	{
		base.Idle(param);
	}

	public override void Move(CommandParam param)
	{
		base.Move(param);
	}

	public override void Attack(CommandParam param)
	{
		base.Attack(param);
	}

	public override void Hit(CommandParam param)
	{
		base.Hit(param);
	}

	public override void Die(CommandParam param)
	{
		base.Die(param);
	}


}
