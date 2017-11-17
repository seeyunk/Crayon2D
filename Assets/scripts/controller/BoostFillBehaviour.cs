using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Crayon;

public class BoostFillBehaviour : MonoBehaviour
{
	private Image filler;

	protected void Start() {
		this.filler = GetComponent<Image> ();
	}

	protected void FixedUpdate() {
		this.draw();
	}

	private void draw() {
		GameObject player = GameObject.FindGameObjectWithTag (Tag.OBJECT_PLAYER.ToString());
		if (player != null) {
			PlayerBehaviour pb = player.GetComponent<PlayerBehaviour> ();
			float speed = pb.getSpeed () - Variables.DEFAULT_SPEED;
			float max = Variables.DEFAULT_BOOST_SPEED - Variables.DEFAULT_SPEED;
			this.filler.fillAmount =  speed / max;
		}


	}
}


