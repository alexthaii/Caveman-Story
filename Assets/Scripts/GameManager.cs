using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	private static GameManager gameInstance = null;
	public static GameManager GetGameManager(){
		if(gameInstance == null){
			gameInstance = (GameManager)FindObjectOfType(typeof(GameManager));
		}
		return gameInstance;
	}

	private GameObject player;
	private GameObject police;

	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");	
		police = GameObject.FindGameObjectWithTag("Police");
	}

	void Update(){
		if(Input.GetKeyDown(KeyCode.Return)){
			Application.LoadLevel(Application.loadedLevel);
		}
	}

	public void GameOver(){
		player.GetComponent<tk2dSpriteAnimator>().Stop();
		player.GetComponent<PlayerController>().enabled = false;
		police.GetComponent<tk2dSpriteAnimator>().Stop();
		police.GetComponent<Police>().enabled = false;
	}
}
