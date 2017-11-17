using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Crayon;

public class AiEnemyBehaviour : CrayonBehaviour {

	private Transform target;

	// Use this for initialization
	protected override void Start () {
		base.Start();
		base.tag = Tag.OBJECT_ENEMY.ToString();
		this.StartCoroutine (action(0.02f));


	}
		
	private IEnumerator action( float delay ) {
		
		while (true) {
			yield return new WaitForSeconds (delay);

			if (this.target != null) {
				float lr = this.detectTarget ();
				base.setFireState (FireState.OFF);
				if (lr > 50.0f) {
					base.applyRotation (true);
				} else if (lr < -50.0f) {
					base.applyRotation (false);
				} else {
					float cross = Vector3.Cross (this.target.position, this.body.position).z;
					if (cross < 0) {
						base.setFireState (FireState.ON);
					}
				}
			}
		}
	}

	private float detectTarget() {
		float s = 2000.0f;
		float rotation = base.body.rotation - 270.0f;
		Vector2 xy = new Vector2 (s * Mathf.Cos (Mathf.Deg2Rad * rotation), s * Mathf.Sin (Mathf.Deg2Rad * rotation));

		return MathUtil.determineLR (base.body.position, xy, this.target.position) / s;
	}

	protected override void Update () {
		base.Update ();
		GameObject player = GameObject.FindGameObjectWithTag (Tag.OBJECT_PLAYER.ToString());
		if (player != null) {
			this.target = player.transform;
		}

		GameObject player_p = GameObject.FindGameObjectWithTag (Tag.OBJECT_PLAYER_PARACHUTE.ToString ());
		if (player_p != null) {
			this.target = player_p.transform;
		}
	}
}
