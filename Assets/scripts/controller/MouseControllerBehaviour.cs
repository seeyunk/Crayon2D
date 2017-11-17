using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Crayon;

public class MouseControllerBehaviour : AbstractControllerBehaviour
{
	public override void handleInput() {
		if (Input.touchCount == 0) {
			if (Input.GetMouseButtonDown(0) ) {
				gesture(10, Camera.main.ScreenToWorldPoint(Input.mousePosition), TouchPhase.Began);
			}
			if (Input.GetMouseButton(0) ) {
				gesture(10, Camera.main.ScreenToWorldPoint(Input.mousePosition), TouchPhase.Moved);
			}
			if (Input.GetMouseButtonUp(0) ) {
				gesture(10, Camera.main.ScreenToWorldPoint(Input.mousePosition), TouchPhase.Ended);
			}
		}
	}
}




