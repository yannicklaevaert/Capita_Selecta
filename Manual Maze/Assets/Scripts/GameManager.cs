using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System.IO;
using System;

public class GameManager : MonoBehaviour {

	public Text timeLeft;

	public Text beatsPerMinute;

	public static float time;

	public GameObject escape;

	public static bool alive;

	public float heartBeat;

	public static bool enemyCollision = false;

	public AudioClip slow;
	public AudioClip fast;
	public AudioClip intro;
	public AudioClip gameover;
	public AudioClip winning;
	
	public float runningTime;

	// Use this for initialization
	void Start () { 
		AudioSource.PlayClipAtPoint(intro, transform.position);
		time = 200.0f;
		runningTime = 0.0f;
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
		if (last == 0){
			return 100;
		}
		else{
			return last;
		}	
  }


	// Update is called once per frame
	void Update () {


		int intLast = ReadLine();
		float floatLast = (float) intLast;
		UpdateRunningTime();
		float newHeartBeat = floatLast + runningTime;
		// if (newHeartBeat > heartBeat){
		// 	if (!GetComponent<AudioSource>().isPlaying){
		// 		audio.clip = fast;
		// 		audio.Play();
		// 	}
			
		// }
		// else{
		// 	if (!audio.isPlaying){
		// 		audio.clip = slow;
		// 		audio.Play();
		// 	}
			
		// }
		heartBeat = newHeartBeat;
		float factor = heartBeat/60;
		time -= factor * Time.deltaTime;
	    if((time < 0) || enemyCollision){
	    	Application.LoadLevel("GameOver");
	    }
	    else{
	    	UpdateUI();
	    }


	}

	public void UpdateUI() {
		timeLeft.text = "Time: " + Mathf.Round(time);
		beatsPerMinute.text = "BPM: " + Mathf.Round(heartBeat);
	}

	public void UpdateRunningTime(){
		float moveX = Input.GetAxis ("Horizontal");
		float moveZ = Input.GetAxis ("Vertical");
		if (moveX ==0 && moveZ == 0){
			runningTime -= 0.01f;
		}
		else{
			runningTime += 0.02f;
		}
	}
}
