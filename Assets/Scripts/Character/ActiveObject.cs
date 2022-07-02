using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CommandParam
{
	
}

public class AttackParam : CommandParam
{

}

public class MoveParam : CommandParam
{
	public float speed;
}

public class IdleParam : CommandParam
{

}

public class HitParam : CommandParam
{
	public int damage;
}

public class DieParam : CommandParam
{

}

public enum ObjectID
{
	punc = 1,
	pacman = 2,
	ghost = 3,
	dino = 4,
	tetris = 5,
	redmush = 6,
	star = 7,
	sonic = 8,
	kirby = 9,
	mstrball = 10,
	orgmush = 11,
	slime = 12,
	goblin = 13,
	zergling = 14,
	spaceship = 15,
	grnmush = 16,
	wrecking = 17,
	among = 18,
	boss1 = 19,
	boss2 = 20,
	boss3 = 21,
	boss4 = 22,
	boss5 = 23,
}

public class ActiveObject : CollisionObject
{
	// 로드하지 않고 직접 세팅하는 방법 채택
	[SerializeField] Animator anim = null;

	public override void Init(CollisionType type)
	{
		base.Init(type);
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
