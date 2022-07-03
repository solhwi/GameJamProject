using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	public Rigidbody2D rigid;
	public BoxCollider2D col;

	public int damage = 0;

    public ENUM_TAG_TYPE tagType = ENUM_TAG_TYPE.Bullet;

	public void Shot(int damage)
	{
		this.damage = damage;
		rigid.velocity = Vector2.right * 12.5f;
	}

	public void TrippleShot(int damage)
	{
		StartCoroutine(TrippleCoroutine(damage));
	}

	private IEnumerator TrippleCoroutine(int damage)
	{
		Shot(damage);

		yield return null;

		Shot(damage);

		yield return null;

		Shot(damage);
	}

	public void OnTriggerEnter2D(Collider2D collision)
	{
		var obj = collision.GetComponent<CollisionObject>();

		if(obj != null && obj.tagType == ENUM_TAG_TYPE.Enemy)
		{
			UnitManager.Instance.ExecuteCommand(obj, CollisionObject.ObjectStatus.Hit, new HitParam() { damage = this.damage });
		}
	}

	public void OnCollisionEnter2D(Collision2D collision)
	{
		//if (gameObject)
		//	Destroy(gameObject);
	}
}
