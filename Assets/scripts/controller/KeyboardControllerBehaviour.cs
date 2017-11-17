using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Crayon;

public class KeyboardControllerBehaviour : AbstractControllerBehaviour
{
	public override void handleInput() {
		GameObject player = GameObject.FindGameObjectWithTag (Tag.OBJECT_PLAYER.ToString());
		if (player == null) {
			return;
		}

//		Crayon.Action action = Crayon.Action.NEUTRAL;
//		if (Input.GetKey (KeyCode.LeftArrow)) {
//			action = Crayon.Action.TURN_LEFT;
//		}
//
//		if (Input.GetKey (KeyCode.RightArrow)) {
//			action = Crayon.Action.TURN_RIGHT;
//		}
//
//		if (Input.GetKey (KeyCode.UpArrow)) {
//			//action = Crayon.Action.SPEED_BOOST;
//		}
//
//		if (Input.GetKeyDown (KeyCode.Space)) {
//			//action = Crayon.Action.FIRE;
//			action = Crayon.Action.FIRE_ON;
//		}
	}
}


