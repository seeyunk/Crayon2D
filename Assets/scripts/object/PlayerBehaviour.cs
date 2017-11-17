using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Crayon;
using UnityEngine.EventSystems;

public class PlayerBehaviour : CrayonBehaviour, ICrayonEventHandler {

	private float time;
	private Coroutine countdownRoutine;
	private GameObject labelObj;

	protected override void Start () {
		base.Start ();

		base.tag = Tag.OBJECT_PLAYER.ToString();
		base.callbackHit += onHit;

		this.labelObj = GameObject.FindGameObjectWithTag ("UI_LABEL");
		//this.countdownRoutine = StartCoroutine (this.countdown ());
	}
		
	public void fire( FireState fireState ) {
		base.setFireState( fireState );
	}

	public void turnLeft() {
		base.applyRotation (true);
	}

	public void turnRight() {
		base.applyRotation (false);
	}

	public void boost( BoostState boostState ) {
		base.setBoostState (boostState);
	}

	public void onHit() {
//		this.StopCoroutine (this.countdownRoutine);
//		ObjectManager manager = GameObject.Find ("ComponentManager").GetComponent<ObjectManager> ();
//		manager.SendMessage ("spawnParachute", transform.position );
		PhotonView view = PhotonView.Get( base.componentManager );
		view.RPC ("spawnParachute", PhotonTargets.All, this.name, this.transform.position );
	}

	private IEnumerator countdown() {
		while (true) {
			UnityEngine.UI.Text label = this.labelObj.GetComponent<UnityEngine.UI.Text>();
			label.text = string.Format ("You have survived for {0} seconds", this.time);
			this.time += 1.0f;
			yield return new WaitForSeconds (1.0f);
		}	
	}
}
