using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public Text timeLeft;

	public int time;

	public GameObject escape;



	// Use this for initialization
	void Start () {
		time = 200;
		UpdateUI ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void UpdateUI() {
		timeLeft.text = "Time: " + time.ToString ("D");
	}
}
