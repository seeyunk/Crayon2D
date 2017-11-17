using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Crayon;
using UnityEngine.EventSystems;

public class ParachuteBehaviour : MovableObjectBehaviour
{

	// Use this for initialization
	private float wait = 15.0f;
	private GameObject labelObj;
	private Coroutine countdownRoutine;

	protected override void Start ()
	{
		base.Start ();
		base.tag = Tag.OBJECT_PLAYER_PARACHUTE.ToString ();	
		this.labelObj = GameObject.FindGameObjectWithTag ("UI_LABEL");

		this.countdownRoutine = this.StartCoroutine (countdown ());
	}

	protected override void Update() {
		base.Update ();
		this.draw ();

		GameObject obj = GameObject.FindGameObjectWithTag (Tag.OBJECT_PLAYER_PARACHUTE.ToString ());
		if (obj == null) {
			this.StopCoroutine( this.countdownRoutine );
		}
	}

	protected void OnTriggerEnter2D( Collider2D col ) {
		GameObject obj = col.gameObject;
		string tag = obj.tag;
		if (tag.Equals (Tag.OBJECT_BULLET.ToString())) {
			BulletBehaviour bullet = obj.GetComponent<BulletBehaviour> ();
			if (!this.name.Equals (bullet.getOwner ())) {
				Destroy (col.gameObject);
				Destroy (this.gameObject);
				UnityEngine.UI.Text label = this.labelObj.GetComponent<UnityEngine.UI.Text>();
				label.text = string.Format ("You died");
			}
		}
	}

	private void draw() {		
		Vector2 position = base.body.position;
		float speed = Variables.DEFAULT_PARACHUTE_VELOCITY * Time.fixedDeltaTime;
		Vector2 pos = new Vector2 (Random.Range (-10.0f, 10.0f), -1.0f);

		base.body.AddForce (pos * speed, ForceMode2D.Impulse);
	}

	private IEnumerator countdown() {
		while (true) {
			UnityEngine.UI.Text label = this.labelObj.GetComponent<UnityEngine.UI.Text>();
			label.text = string.Format ("{0} seconds left to revive", this.wait);
			this.wait -= 1.0f;
			if (this.wait < 0.0f) {
//				ObjectManager manager = GameObject.Find ("ObjectManager").GetComponent<ObjectManager> ();
//				manager.SendMessage ("spawnPlayer", transform.position );
//				Destroy (this.gameObject);

				PhotonNetwork.Destroy (this.gameObject);

				Quaternion rotation = Quaternion.Euler (new Vector3 (0.0f, 0.0f, Random.Range( -180.0f, 180.0f )));;
				GameObject player = PhotonNetwork.Instantiate ("player", transform.position, rotation, 0);
				player.name = PhotonNetwork.AuthValues.UserId; 
			}
			yield return new WaitForSeconds (1.0f);
		}	
	}
}

