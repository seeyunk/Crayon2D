using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Crayon;

public class CrayonBehaviour : MovableObjectBehaviour {

	private GameObject bulletPrefab;
	private volatile FireState fireState = FireState.OFF;
	private volatile BoostState boostState = BoostState.OFF;
	private volatile float boostTime = 0.0f;
	protected volatile float speed = 0.0f;

	protected delegate void CallbackHit();
	protected CallbackHit callbackHit;


	protected override void Start() {
		this.bulletPrefab = Resources.Load<GameObject> ("bullet");
		this.StartCoroutine (fireRoutine());
		this.StartCoroutine (boostRoutine ());
		this.speed = Variables.DEFAULT_SPEED;

		base.Start ();
	}

	protected override void Update() {
		base.Update ();
		this.draw ();
	}

	protected void setFireState( FireState fireState ) {
		this.fireState = fireState;
	}

	public float getSpeed() {
		return this.speed;
	}

	protected void setBoostState( BoostState boostState ) {
		if ( this.boostState != BoostState.OVERHEAT &&
			this.boostTime == 0.0f ) {
			this.boostState = boostState;
		}
	}

	private IEnumerator fireRoutine() {
		while (true) {
			if ( this.fireState == FireState.ON ) {
//				@@this.shootBullet ();				

				PhotonView view = PhotonView.Get( this.componentManager );
				view.RPC ("shootBullet", PhotonTargets.All, this.name, this.transform.position, this.transform.rotation, this.speed);
				this.fireState = FireState.OFF;
				yield return new WaitForSeconds (0.4f);
			}
			yield return new WaitForSeconds (Time.deltaTime);
		}
	}
		
	private IEnumerator boostRoutine() {
		while (true) {
			if (this.boostState == BoostState.OVERHEAT) {
				this.boostState = BoostState.OFF;
			} else {
				if (this.boostState == BoostState.ON) {
					if (this.speed >= Variables.DEFAULT_BOOST_SPEED) {
						this.boostTime += Time.deltaTime;
						if (boostTime >= 0.5f) {
							this.boostState = BoostState.OVERHEAT;
						}
					} else {
						this.applySpeed (true, Variables.DEFAULT_SPEED, Variables.DEFAULT_BOOST_SPEED, Variables.DEFAULT_BOOST_INC_VELOCITY);
					}
				}

				if (this.boostState == BoostState.OFF) {
					this.applySpeed (false, Variables.DEFAULT_SPEED, Variables.DEFAULT_BOOST_SPEED, Variables.DEFAULT_BOOST_DEC_VELOCITY);
					if (this.speed <= Variables.DEFAULT_SPEED) {
						this.boostTime = 0.0f;
					}
				}
			}
		
			yield return new WaitForSeconds (Time.deltaTime);
		
		}
	}

	private void draw() {		
		Vector2 position = transform.position;
		float speed = this.speed * Time.fixedDeltaTime;
		float rotation = base.body.rotation;

		float rad = rotation * Mathf.Deg2Rad;
		position.x = position.x + Mathf.Sin(rad) * speed * -1.0f;
		position.y = position.y + Mathf.Cos(rad) * speed;

		base.body.MovePosition( position );
	}

	protected void shootBullet() {
		GameObject bullet = Instantiate<GameObject> ( this.bulletPrefab, transform.position, transform.rotation );
		BulletBehaviour bulletBehaviour = bullet.GetComponent<BulletBehaviour> ();
		bulletBehaviour.setOwner (this.name, this.speed );
		Destroy (bullet, 1.0f);
	}


	protected void applySpeed( bool up, float min, float max, float velocity) {
		float speed = this.speed;
		if ( up ) {
			speed += velocity;
			speed = speed > max ? max : speed;
		} else {
			speed -= velocity;
			speed = speed < min ? min : speed;
		}
			
		this.speed = speed;
	}

	protected void applyRotation( bool left ) {
		this.applyRotation (left, Variables.DEFAULT_ROTATION_VELOCITY);
	}

	protected void applyRotation( bool left, float velocity ) {
		var angle = transform.eulerAngles;
		if (left) {
			angle.z += velocity;
		} else {
			angle.z -= velocity;
		}
			
		transform.rotation = Quaternion.Euler (angle);
	}
		
	protected void OnTriggerEnter2D( Collider2D col ) {
		GameObject obj = col.gameObject;
		string tag = obj.tag;
		if (tag.Equals (Tag.OBJECT_BULLET.ToString())) {
			BulletBehaviour bullet = obj.GetComponent<BulletBehaviour> ();
			if (!this.name.Equals (bullet.getOwner ())) {
//				Destroy (col.gameObject);
//				Destroy (this.gameObject);
				PhotonView bulletView = col.gameObject.GetComponent<PhotonView>();
				PhotonView playerView = this.gameObject.GetComponent<PhotonView> ();

				if (bulletView.isMine && playerView.isMine) {
					PhotonNetwork.Destroy (col.gameObject);
					PhotonNetwork.Destroy (this.gameObject);
				}

				if (this.callbackHit != null) {
					this.callbackHit ();
				}
			}
		}
	}

	protected void Destroy() {
		PhotonNetwork.Destroy (this.gameObject);
	}

	private void score() {
		GameObject obj = GameObject.FindGameObjectWithTag ("UI_SCORE");
		if (obj == null) {
			return;
		}

		UnityEngine.UI.Text scoreText = obj.GetComponent<UnityEngine.UI.Text>();
		string score = scoreText.text;
		string[] vals = score.Split ( ":"[0] );
		int win = int.Parse(vals [0]);
		int loose = int.Parse(vals [1]);
		string tag = this.tag;
		if ( tag.Equals( Tag.OBJECT_ENEMY.ToString() ) ) {
			win++;
		}
		else if ( tag.Equals( Tag.OBJECT_PLAYER.ToString() ) ) {
			loose++;
		}

		scoreText.text = string.Format ("{0}:{1}", win, loose);
	}
}
