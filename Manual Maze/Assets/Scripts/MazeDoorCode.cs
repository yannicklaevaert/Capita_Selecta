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
	public Collider other;
	public Transform hinge;


	void OnTriggerEnter (Collider other) {
		this.other = other;
		onTrigger = true;
		/*if (other.gameObject.tag == "Player") {
			OpenDoor (other);
		}*/
	}

	void OnTriggerExit (Collider other) {
		/*if (other.gameObject.tag == "Player") {
			hinge.localRotation = Quaternion.identity;
		}*/
		this.other = other;
		onTrigger = false;
		keypadScreen = false;
		input = "";
	}

	protected void OpenDoor() {
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
		if(input == curPassword){
			doorOpen = true;
		}

		if(doorOpen){
			/*var newRot = Quaternion.RotateTowards(hinge.rotation, Quaternion.Euler(0.0f, -90.0f, 0.0f), Time.deltaTime * 250);
			hinge.rotation = newRot;*/
			OpenDoor();
		}
	}

	void OnGUI() 	{
		if(!doorOpen)
		{
			if(onTrigger)
			{
				
				//GUI.Box(new Rect(0, 0, 220, 330), "");
				GUI.Box(new Rect(100, 100, 200, 25), "Enter code to open door:");
				GUI.Box(new Rect(100, 125, 200, 25), input);

				if(Event.current.Equals(Event.KeyboardEvent(KeyCode.Keypad1.ToString()))){
					input = input + "1";
				}

				if(Event.current.Equals(Event.KeyboardEvent(KeyCode.Keypad2.ToString()))){
					input = input + "2";
				}

				if(Event.current.Equals(Event.KeyboardEvent(KeyCode.Keypad3.ToString())))
				{
					input = input + "3";
				}

				if(Event.current.Equals(Event.KeyboardEvent(KeyCode.Keypad4.ToString())))
				{
					input = input + "4";
				}

				if(Event.current.Equals(Event.KeyboardEvent(KeyCode.Keypad5.ToString())))
				{
					input = input + "5";
				}

				if(Event.current.Equals(Event.KeyboardEvent(KeyCode.Keypad6.ToString())))
				{
					input = input + "6";
				}

				if(Event.current.Equals(Event.KeyboardEvent(KeyCode.Keypad7.ToString())))
				{
					input = input + "7";
				}

				if(Event.current.Equals(Event.KeyboardEvent(KeyCode.Keypad8.ToString())))
				{
					input = input + "8";
				}

				if(Event.current.Equals(Event.KeyboardEvent(KeyCode.Keypad9.ToString())))
				{
					input = input + "9";
				}

				if(Input.GetKeyDown(KeyCode.Keypad0))
				{
					input = input + "0";
				}

				if(Event.current.Equals(Event.KeyboardEvent(KeyCode.Backspace.ToString())))
				{
					input = input.Substring(0, input.Length - 1);
				}
			}
		}
	}
}
