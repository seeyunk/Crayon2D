using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LobbyBehaviour : MonoBehaviour {

	// Use this for initialization
	private Button single;
	private Button multi;

	void Start () {
		this.single = GameObject.Find ("BTN_SINGLE").GetComponent<Button>();
		this.multi = (Button)GameObject.Find ("BTN_MULTI").GetComponent<Button>();

		this.single.onClick.AddListener (onClickSingle);
		this.multi.onClick.AddListener (onClickMulti);
	}


	private void onClickSingle() {
		SceneManager.LoadScene ("SinglePlay", LoadSceneMode.Single);
	}

	private void onClickMulti() {
		SceneManager.LoadScene ("MultiPlay", LoadSceneMode.Single);
	}
		
}
