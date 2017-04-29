using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class MazeDoorCode : MonoBehaviour {

	public string curPassword = "12345";
	public string input;
	public bool onTrigger;
	public bool doorOpen;
	public bool keypadScreen;
	public Transform hinge;


	void OnTriggerEnter (Collider other) {
		onTrigger = true;
		/*if (other.gameObject.tag == "Player") {
			OpenDoor (other);
		}*/
	}

	void OnTriggerExit (Collider other) {
		/*if (other.gameObject.tag == "Player") {
			hinge.localRotation = Quaternion.identity;
		}*/
		onTrigger = false;
		keypadScreen = false;
		input = "";
	}

	protected void OpenDoor(Collider other) {
		if (this.transform.rotation == Quaternion.identity) {
			if (other.gameObject.transform.position.z > this.transform.position.z + 0.5) {
				hinge.localRotation = Quaternion.Euler (0f, 90f, 0f);
			} else if (other.gameObject.transform.position.z < this.transform.position.z + 0.5) {
				hinge.localRotation = Quaternion.Euler (0f, -90f, 0f);
			}
		}
		else if (this.transform.rotation == Quaternion.Euler (0f, 90f, 0f)) {
			Debug.Log ("Test");
			if (other.gameObject.transform.position.x > this.transform.position.x + 0.5) {
				hinge.localRotation = Quaternion.Euler (0f, 90f, 0f);
			} else if (other.gameObject.transform.position.x < this.transform.position.x + 0.5) {
				hinge.localRotation = Quaternion.Euler (0f, -90f, 0f);
			}
		}
		else if (this.transform.rotation == Quaternion.Euler (0f, 180f, 0f)) {
			if (other.gameObject.transform.position.z < this.transform.position.z - 0.5) {
				hinge.localRotation = Quaternion.Euler (0f, 90f, 0f);
			} else if (other.gameObject.transform.position.z > this.transform.position.z - 0.5) {
				hinge.localRotation = Quaternion.Euler (0f, -90f, 0f);
			}
		}
		else if (this.transform.rotation == Quaternion.Euler (0f, 270f, 0f)) {
			if (other.gameObject.transform.position.x < this.transform.position.x - 0.5) {
				hinge.localRotation = Quaternion.Euler (0f, 90f, 0f);
			} else if (other.gameObject.transform.position.x > this.transform.position.x - 0.5) {
				hinge.localRotation = Quaternion.Euler (0f, -90f, 0f);
			}
		}
	}

	void Update() {
		if(input == curPassword)
		{
			doorOpen = true;
		}

		if(doorOpen)
		{
			var newRot = Quaternion.RotateTowards(hinge.rotation, Quaternion.Euler(0.0f, -90.0f, 0.0f), Time.deltaTime * 250);
			hinge.rotation = newRot;
		}
	}

	void OnGUI() 	{
		if(!doorOpen)
		{
			if(onTrigger)
			{
				GUI.Box(new Rect(0, 0, 200, 25), "Press 'E' to open keypad");

				if(Input.GetKeyDown(KeyCode.E))
				{
					keypadScreen = true;
					onTrigger = false;
				}
			}

			if(keypadScreen)
			{
				GUI.Box(new Rect(0, 0, 220, 330), "");
				GUI.Box(new Rect(5, 5, 210, 25), input);

				if(GUI.Button(new Rect(5, 35, 60, 60), "1"))
				{
					input = input + "1";
				}

				if(GUI.Button(new Rect(80, 35, 60, 60), "2"))
				{
					input = input + "2";
				}

				if(GUI.Button(new Rect(155, 35, 60, 60), "3"))
				{
					input = input + "3";
				}

				if(GUI.Button(new Rect(5, 110, 60, 60), "4"))
				{
					input = input + "4";
				}

				if(GUI.Button(new Rect(80, 110, 60, 60), "5"))
				{
					input = input + "5";
				}

				if(GUI.Button(new Rect(155, 110, 60, 60), "6"))
				{
					input = input + "6";
				}

				if(GUI.Button(new Rect(5, 185, 60, 60), "7"))
				{
					input = input + "7";
				}

				if(GUI.Button(new Rect(80, 185, 60, 60), "8"))
				{
					input = input + "8";
				}

				if(GUI.Button(new Rect(155, 185, 60, 60), "9"))
				{
					input = input + "9";
				}

				if(GUI.Button(new Rect(5, 260, 60, 60), "0"))
				{
					input = input + "0";
				}

				if(GUI.Button(new Rect(155, 260, 60, 60), "Clear"))
				{
					input = "";
				}
			}
		}
	}
}
