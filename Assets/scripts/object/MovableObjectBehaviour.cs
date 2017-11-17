using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Crayon;

public abstract class MovableObjectBehaviour : MonoBehaviour {

	private Camera cam;
	private float distanceZ = 0.0f;
	private Vector2 buffer;
	private Vector3 lt, rt, lb, rb;
	protected Rigidbody2D body;
	protected GameObject componentManager;

	protected virtual void Start() {
		this.componentManager = GameObject.Find ("ComponentManager");

		this.body = GetComponent<Rigidbody2D> ();
		this.cam = Camera.main;
	
		float width = this.transform.localScale.x / GetComponent<SpriteRenderer>().bounds.size.x;
		float height = this.transform.localScale.y / GetComponent<SpriteRenderer> ().bounds.size.y;
		this.buffer = new Vector2 ( width, height );


//		this.distanceZ = Mathf.Abs ( cam.transform.position.z + transform.position.z );


//		lb = this.cam.ScreenToWorldPoint (new Vector3 ( 0.0f, 0.0f, distanceZ ) );
//		rb = this.cam.ScreenToWorldPoint (new Vector3 ( Screen.width, 0.0f, distanceZ ) );
//		lt = this.cam.ScreenToWorldPoint (new Vector3 ( 0.0f, Screen.height, distanceZ) );
//		rt = this.cam.ScreenToWorldPoint (new Vector3 ( Screen.width, Screen.height, distanceZ ) );

		RectTransform tr = GameObject.Find ("Background").GetComponent<RectTransform> ();
		Vector3[] corners = new Vector3[4];
		tr.GetWorldCorners (corners);
//		for (int n = 0; n < corners.Length; n++) {
//			Debug.Log (corners [n]);
//		}
//

		lb = corners [0];
		lt = corners [1];
		rt = corners [2];
		rb = corners [3];

//		lb = new Vector2( -400.0f, -240.0f );
//		rb = new Vector2 (400.0f, -240.0f);
//		lt = new Vector2 (-400.0f, 240.0f);
//		rt = new Vector2 (400.0f, 240.0f);

		Debug.DrawLine (lb, rb, Color.red, Mathf.Infinity, false);
	}

	protected virtual void Update() {
		this.determineOverflowPos ( this.body.rotation );
	}

//	protected void DestroyGameObject() {
//		Debug.Log ("asdf");
//		PhotonNetwork.Destroy (this.gameObject);
//	}

	public bool intersect( string tag, Vector2 p1, Vector2 p2, Vector2 q1, Vector2 q2, out Vector2 pt ) {
		float a1 = p2.y - p1.y;
		float b1 = p1.x - p2.x;
		float c1 = a1 * p1.x + b1 * p1.y;

		float a2 = q2.y - q1.y;
		float b2 = q1.x - q2.x;
		float c2 = a2 * q1.x + b2 * q1.y;

		pt = Vector2.zero;
		float det = a1 * b2 - a2 * b1;
		if ( det != 0.0f ) {
			pt.x = (b2 * c1 - b1 * c2) / det;
			pt.y = (a1 * c2 - a2 * c1) / det;
			if (pt.x <= rt.x &&
				pt.x >= lt.x &&
				pt.y <= lt.y &&
				pt.y >= lb.y) {

				return true;
			}
		}

		return false;

	}
		
	protected bool checkScreenOverflowBegin() {
		if ( (transform.position.x < lb.x) ||
			(transform.position.x > rb.x) ||
			(transform.position.y > lt.y) ||
			(transform.position.y < lb.y) ) {
			return true;
		}

		return false;
	}

	protected void determineOverflowPos( float rotation ) {
		Vector2 oldPos = transform.position;
		if (transform.position.x < lb.x - buffer.x) { //left
			transform.position = new Vector2(rt.x - buffer.x, oldPos.y);
		}

		if (transform.position.x > rb.x + buffer.x) { //rigt
			transform.position = new Vector2(lt.x + buffer.x, oldPos.y);
		}

		if (transform.position.y > lt.y + buffer.y) { //top
			transform.position = new Vector2(oldPos.x, lb.y + buffer.y);
		}

		if (transform.position.y < lb.y - buffer.y) { //bottom
			transform.position = new Vector2(oldPos.x, lt.y - buffer.y);
		}

	}


//	protected void determineOverflowPos( float rotation ) {
//		float s = Vector2.Distance (lt, rb) + 100.0f;
//		rotation -= 90.0f;
//		Vector2 xy = new Vector2 (s * Mathf.Cos (Mathf.Deg2Rad * rotation), s * Mathf.Sin (Mathf.Deg2Rad * rotation));
//		Vector2 pt = Vector2.zero;
//		if (transform.position.x < lb.x - buffer.x) { //left
//			if ( intersect ("bottom", lb, rb, transform.position, xy, out pt) ||
//				intersect ("right", rb, rt, transform.position, xy, out pt) ||
//				intersect ("top", lt, rt, transform.position, xy, out pt)) {
//				transform.position = pt;
//				return;
//			}
//		}
//
//		if (transform.position.x > rb.x + buffer.x) { //rigt
//			if ( intersect ("bottom", lb, rb, transform.position, xy, out pt) ||
//				intersect ("top", lt, rt, transform.position, xy, out pt) ||
//				intersect ("left", lb, lt, transform.position, xy, out pt)) {
//				transform.position = pt;
//
//				return;
//			}
//		}
//
//		if (transform.position.y > lt.y + buffer.y) { //top
//			if (intersect ("bottom", lb, rb, transform.position, xy, out pt) ||
//				intersect ("right", rb, rt, transform.position, xy, out pt) ||
//				intersect ("left", lb, lt, transform.position, xy, out pt)) {
//				transform.position = pt;
//
//				return;
//			}
//		}
//
//		if (transform.position.y < lb.y - buffer.y) { //bottom
//			if (intersect ("right", rb, rt, transform.position, xy, out pt) ||
//				intersect ("top", lt, rt, transform.position, xy, out pt) ||
//				intersect ("left", lb, lt, transform.position, xy, out pt)) {
//				transform.position = pt;
//
//				return;
//			}
//		}
//	}
}
