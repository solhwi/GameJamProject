using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionObject : MonoBehaviour
{
	public enum CollisionType
	{
		Box = 0,
		Capsule = 1,
		Polygon = 2,
	}

	// 리지드 바디? 캐릭터 컨트롤러?

	private Collider2D col = null;

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
	}

	public void OnCollisionEnter2D(Collision2D collision)
	{

	}

	public void OnCollisionStay2D(Collision2D collision)
	{
		
	}

	public void OnCollisionExit2D(Collision2D collision)
	{

	}
}
