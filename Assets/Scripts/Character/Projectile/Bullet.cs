using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	private Rigidbody2D rigid;
	private BoxCollider2D col;

    public ENUM_TAG_TYPE tagType = ENUM_TAG_TYPE.Bullet;

	public void Shot(Vector3 rotateVec)
	{
		transform.Rotate(rotateVec);
	}

	private void Update()
	{
		rigid.velocity = Vector2.right;
	}
}
