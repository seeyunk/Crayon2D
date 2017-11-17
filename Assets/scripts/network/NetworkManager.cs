using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon;

public class NetworkManager : Photon.PunBehaviour {

	// Use this for initialization
	void Start () {
		PhotonNetwork.ConnectUsingSettings ("1.0");
	}

	public override void OnPhotonJoinRoomFailed( object[] result ) {
	}

	public override void OnJoinedRoom() {
		PhotonView view = PhotonView.Get (this);
		Vector3 pos = new Vector3 (Random.Range (-400.0f, 400.0f), Random.Range (-200.0f, 200.0f), 0.0f);
		view.RPC ("spawnPlayer", PhotonTargets.All, PhotonNetwork.AuthValues.UserId, pos);
	}

	public override void OnJoinedLobby() {
		RoomOptions options = new RoomOptions ();
		options.CleanupCacheOnLeave = true;
		options.IsOpen = true;
		options.IsVisible = true;
		PhotonNetwork.JoinOrCreateRoom ( "crayon#1", options, TypedLobby.Default );
	}

	[PunRPC]
	protected void spawnPlayer( string name, Vector3 position ) {
		Quaternion rotation = Quaternion.Euler (new Vector3 (0.0f, 0.0f, Random.Range( -180.0f, 180.0f )));;
		GameObject player = PhotonNetwork.Instantiate ("player", position, rotation, 0);
		player.name = name;
	}

	[PunRPC]
	protected void spawnParachute( string name, Vector3 position ) {
		GameObject parachute = PhotonNetwork.Instantiate( "player_p",  position, Quaternion.identity, 0 );
		parachute.name = name;
	}

	[PunRPC]
	protected void shootBullet( string owner, Vector3 position, Quaternion rotation, float speed) {
		GameObject bullet = PhotonNetwork.Instantiate( "bullet", position, rotation, 0 );
		BulletBehaviour bulletBehaviour = bullet.GetComponent<BulletBehaviour> ();
		bulletBehaviour.setOwner (owner, speed );
		StartCoroutine (destroyBullet( bullet ));
	}

	private IEnumerator destroyBullet( GameObject bullet ) {
		yield return new WaitForSeconds (1.0f);
		PhotonView view = bullet.GetComponent<PhotonView> ();
		if ( view.isMine ) {
			PhotonNetwork.Destroy (bullet);
		}
	}
}
