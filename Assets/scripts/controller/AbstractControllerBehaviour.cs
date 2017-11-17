using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Crayon;

public abstract class AbstractControllerBehaviour : MonoBehaviour, IInputHandler
{
	protected delegate void InputDelegator();
	protected InputDelegator input;
	private Vector2 oldPos;
	private GameObject controller;

	protected void Start() {
		this.input += handleInput;
		this.controller = GameObject.FindGameObjectWithTag (Tag.UI_CONTROLLER_MOVE.ToString());
	}

	protected virtual void Update ()
	{
		this.input();
	}

	public abstract void handleInput();

	protected Collider2D examine( Vector2 pos ) {
		int layerMask = 1 << LayerMask.NameToLayer (Layer.UI.ToString ());
		RaycastHit2D hit = Physics2D.Raycast (pos, Vector2.up, layerMask );
		return hit.collider;
	}

	protected void gesture(int fingerId, Vector2 pos, TouchPhase phase) {
		Collider2D collider = this.examine (pos);
		if (collider == null) {
			return;
		}

		string tag = collider.tag;
		if ( tag.Equals( Tag.UI_CONTROLLER_MOVE.ToString() ) ) {
			this.control (pos, phase);
		}

		if (tag.Equals (Tag.UI_CONTROLLER_FIRE.ToString ())) {
			this.fire (pos, phase);
		} 

		if (tag.Equals (Tag.UI_CONTROLLER_BOOST.ToString ())) {
			this.boost (pos, phase);
		} 
	}
//
//	private void control( Vector2 pos, TouchPhase phase ) {
//		if (!phase.Equals (TouchPhase.Canceled) &&
//			!phase.Equals( TouchPhase.Ended ) ) {
//
//			GameObject player = GameObject.FindGameObjectWithTag (Tag.OBJECT_PLAYER.ToString());
//			if (player == null) {
//				return;
//			}
//
//			Vector2 center = this.controller.transform.position;
//			Vector2 a = new Vector2 (center.x, 200.0f) - center;
//			Vector2 b = pos - center;
//			float angle = Vector2.SignedAngle (b, a);
//
//			if (angle > 30.0f) {
//				ExecuteEvents.Execute<ICrayonEventHandler> (player, null, (x, y) => x.turnRight ());
//			}
//			else if (angle < -30.0f) {
//				ExecuteEvents.Execute<ICrayonEventHandler> (player, null, (x, y) => x.turnLeft ());
//			}
//		}
//	}
//
	private void control( Vector2 pos, TouchPhase phase ) {
		if (!phase.Equals (TouchPhase.Canceled) &&
			!phase.Equals( TouchPhase.Ended ) ) {

			GameObject player = GameObject.FindGameObjectWithTag (Tag.OBJECT_PLAYER.ToString());
			if (player == null) {
				return;
			}

			Vector2 center = this.controller.transform.position;
			Vector2 a = pos - center;
			Vector2 b = this.oldPos - center;
			float angle = Vector2.SignedAngle (a, b);
			if (angle > 0.0f) {
				ExecuteEvents.Execute<ICrayonEventHandler> (player, null, (x, y) => x.turnRight ());
			}
			else if (angle < 0.0f) {
				ExecuteEvents.Execute<ICrayonEventHandler> (player, null, (x, y) => x.turnLeft ());
			}

			this.oldPos = pos;
		}
	}
		
	private void fire( Vector2 pos, TouchPhase phase ) {
		FireState fireState = FireState.OFF;
		if (!phase.Equals( TouchPhase.Ended ) ) {
			fireState = FireState.ON;
		}

		GameObject player = GameObject.FindGameObjectWithTag (Tag.OBJECT_PLAYER.ToString());
		if (player != null) {
			ExecuteEvents.Execute<ICrayonEventHandler> (player, null, (x,y) => x.fire( fireState ) );
		}
	}
		
	private void boost( Vector2 pos, TouchPhase phase ) {
		BoostState boostState = BoostState.OFF;
		if ( !phase.Equals( TouchPhase.Ended ) ) {
			boostState = BoostState.ON;
		}
			
		GameObject player = GameObject.FindGameObjectWithTag (Tag.OBJECT_PLAYER.ToString());
		if (player != null) {
			ExecuteEvents.Execute<ICrayonEventHandler> (player, null, (x, y) => x.boost (boostState));
		}
	}
}

