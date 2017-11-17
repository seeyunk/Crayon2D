using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Crayon;

public class ObjectManager : MonoBehaviour {

	private delegate void SpawnDelegator( Vector2 pos, float rotation, float speed );
	private class Player
	{
		public GameObject obj;

		public void spawnPlayer( Vector3 position) {
			this.obj = Instantiate<GameObject> ( Resources.Load<GameObject>("player") );	
			this.obj.transform.position = position;
			this.obj.transform.rotation = Quaternion.Euler (new Vector3 (0.0f, 0.0f, Random.Range( -180.0f, 180.0f )));
		}

		public void spawnParachute( Vector3 position ) {
			this.obj = Instantiate<GameObject> ( Resources.Load<GameObject>("player_p") );	
			this.obj.transform.position = position;
		}
	}

	private Player player = new Player();

	private void spawnEnemy() {
		GameObject enemy = Instantiate<GameObject> ( Resources.Load<GameObject>("enemy") );
		enemy.transform.position = new Vector2 (Random.Range (-400.0f, 400.0f), Random.Range (-200.0f, 200.0f));
		enemy.transform.rotation = Quaternion.Euler (new Vector3 (0.0f, 0.0f, Random.Range( -180.0f, 180.0f )));

	}

	public void spawnPlayer( Vector3 position) {
		this.player.spawnPlayer ( position );
	}

	public void spawnParachute( Vector3 position ) {
		this.player.spawnParachute (position);
	}

	private IEnumerator countdown( float begin, float velocity ) {
		float f = begin;
		while( true ) {
			f -= velocity;
			yield return new WaitForSeconds (velocity);
			if (f < 0.0f) {
				spawnEnemy ();
				f = begin;
			}
		}			
	}

	protected void Start () {
		this.StartCoroutine (countdown(5.0f,1.0f));
		this.player.spawnPlayer ( new Vector3 (Random.Range (-400.0f, 400.0f), Random.Range (-200.0f, 200.0f), 0.0f) );
	}

}
