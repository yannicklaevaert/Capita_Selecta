using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public Text timeLeft;

	public float time;

	public GameObject escape;

	public bool alive;

	public static bool enemyCollision = false;


	// Use this for initialization
	void Start () {
		time = 20.0f;
		alive = true;
		UpdateUI();
		Update();
	}
	
	// Update is called once per frame
	void Update () {
		time -= Time.deltaTime;
	    if((time < 0) || enemyCollision){
	        alive = false;
	        GameOver();
	    }
	    else{
	    	UpdateUI();	
	    }

	    
	}

	public void UpdateUI() {
		timeLeft.text = "Time: " + Mathf.Round(time);
	}

	public void GameOver() {
		timeLeft.text = "Game Over";
	}
}
