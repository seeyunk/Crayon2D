using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Crayon;

public class BulletBehaviour : MovableObjectBehaviour {
	public static float DEFAULT_BULLET_VELOCITY = 350.0f;
	private string owner;
	private float speed;

	protected override void Start() {
		base.Start();
		base.tag = Tag.OBJECT_BULLET.ToString();
	}

	protected override void Update () {
		base.Update ();
		this.draw ();
	}
		
	private void draw() {		
		Vector2 position = base.body.position;
		float speed = ( this.speed + DEFAULT_BULLET_VELOCITY ) * Time.fixedDeltaTime;
		float rad = base.body.rotation * Mathf.Deg2Rad;
		position.x = position.x + Mathf.Sin(rad) * speed * -1.0f;
		position.y = position.y + Mathf.Cos(rad) * speed;

		base.body.MovePosition( position );
	}

	public void setOwner( string owner, float speed ) {
		this.owner = owner;
		this.speed = speed;
	}

	public string getOwner() {
		return this.owner;
	}
}
