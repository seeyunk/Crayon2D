using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Crayon;

public class TouchControllerBehaviour : AbstractControllerBehaviour
{
	public override void handleInput() {
		for (int i = 0; i < Input.touchCount; i++) {
			Touch touch = Input.touches [i];
			gesture( touch.fingerId, Camera.main.ScreenToWorldPoint(touch.position), touch.phase);
		}
	}


}


