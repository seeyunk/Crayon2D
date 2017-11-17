using System;
using UnityEngine;

public class MathUtil
{
	public static float determineLR( Vector2 v1, Vector2 v2, Vector2 pt ) {
		return (v2.x - v1.x) * (pt.y - v1.y) - (pt.x - v1.x) * (v2.y - v1.y);
	}

	public static bool intersect( string tag, Vector2 p1, Vector2 p2, Vector2 q1, Vector2 q2, out Vector2 pt ) {
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
			return true;
		}

		return false;
	}
}


