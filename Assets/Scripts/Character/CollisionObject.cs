using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 태그 타입, Unity의 태그는 사용하지 않을 예정
/// </summary>
/// 
[Serializable]
public enum ENUM_TAG_TYPE
{
	Untagged = 0,
	Interactable = 1,
	Friendly = 2,
	Enemy = 3,
	Tower = 4,
}

public class CollisionObject : MonoBehaviour
{
	public ObjectID id;
	public ENUM_TAG_TYPE tagType = ENUM_TAG_TYPE.Untagged;

	public enum ObjectStatus
	{
		Idle = 0,
		Move = 1,
		Attack = 2,
		Hit = 3,
		Die = 4,
	}

	ObjectStatus currActiveStatus = ObjectStatus.Idle;

	public enum CollisionType
	{
		Box = 0,
		Capsule = 1,
		Polygon = 2,
	}

	// 리지드 바디? 캐릭터 컨트롤러?

	private Collider2D col = null;

	protected int currHp = 0;

	public virtual void Init(CollisionType type)
	{
		if (col != null)
			return;

		switch(type)
		{
			case CollisionType.Box:
				col = gameObject.AddComponent<BoxCollider2D>();
				break;

			case CollisionType.Capsule:
				col = gameObject.AddComponent<CapsuleCollider2D>();
				break;

			case CollisionType.Polygon:
				col = gameObject.AddComponent<PolygonCollider2D>();
				break;
		}

		Idle(); // 시작엔 Idle로 세팅
	}

	public virtual void Idle(CommandParam param = null)
	{

	}

	public virtual void Move(CommandParam param)
	{

	}

	public virtual void Attack(CommandParam param)
	{

	}

	public virtual void Hit(CommandParam param)
	{
		var _param = param as HitParam;
		if (_param == null)
			return;

		currHp -= _param.damage;
		if (currHp < 0)
			currHp = 0;

		UnitManager.Instance.ExecuteCommand(this, ActiveObject.ObjectStatus.Die, new DieParam());
 	}

	public virtual void Die(CommandParam param)
	{

	}
}
