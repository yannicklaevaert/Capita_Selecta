using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System.IO;
using System;

public class GameManager : MonoBehaviour {

	public Text timeLeft;

	public Text beatsPerMinute;

	public float time;

	public GameObject escape;

	public bool alive;

	public int heartBeat;

	public static bool enemyCollision = false;



	// Use this for initialization
	void Start () {
		time = 200.0f;
		alive = true;
		UpdateUI();
		Update();
	}

	int ReadLine()
	{
		string filename = "cms50d/foo.txt";
		FileInfo theSourceFile = new FileInfo (filename);
    	StreamReader reader = theSourceFile.OpenText();
		string input = reader.ReadLine();
		reader.Close();
		int last = int.Parse(input);
		return last;
  }


	// Update is called once per frame
	void Update () {

		int intLast = ReadLine();
		if (intLast > heartBeat){
			time -= 10;
		}
		heartBeat = intLast;
		float last = (float)intLast;
		float factor = last/60;
		time -= factor * Time.deltaTime;
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
		beatsPerMinute.text = "BPM: " + heartBeat;
	}

	public void GameOver() {
		timeLeft.text = "Game Over";
		Time.timeScale = 0f;
	}
}
