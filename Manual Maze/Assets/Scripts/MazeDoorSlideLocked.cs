using UnityEngine;
using System.Collections;

public class MazeDoorSlideLocked : MonoBehaviour {

	Animator animator;
	bool doorOpen;
	public string curPassword = "123456";
	public static string neededColor = "green";
	public static string neededType = "A";
	public string input;
	bool onTrigger;
	bool doorLocked;
	bool doorClosed;
	Collider other;
	public string message = "Authorized personnel only.";
	public string key = "You need a " + neededColor + " key of type " + neededType;

	void Start()
	{
		doorLocked = true;
		animator = GetComponent<Animator>();
	}
		
	void OnTriggerEnter (Collider other) {
		this.other = other;
		onTrigger = true;
		doorClosed = false;
		doorOpen = true;
	}

	void OnTriggerExit (Collider other) {
		/*if (other.gameObject.tag == "Player") {
			hinge.localRotation = Quaternion.identity;
		}*/
		this.other = other;
		onTrigger = false;
		input = "";
		doorClosed = true;
		doorOpen = false;
	}


	void DoorControl(string direction)
	{
		animator.SetTrigger(direction);
	}


	void Update() {
//		/*if(input == curPassword){
//			doorOpen = true;
//		}*/

		if(!doorLocked && doorOpen){
			DoorControl ("Open");
			
		}

		if (doorClosed){
			if (other.gameObject.tag == "Player") {
				doorClosed = false;
				DoorControl ("Close");
			}
		}
	}

	void OnGUI() 	{
		if(doorLocked)
		{
			if(onTrigger)
			{

				//GUI.Box(new Rect(0, 0, 220, 330), "");
				GUI.Box(new Rect(100, 100, 200, 25), message);
				GUI.Box(new Rect(100, 125, 200, 25), "Enter code to open door:");
				GUI.Box(new Rect(100, 150, 200, 25), input);
				GUI.Box(new Rect(100, 175, 200, 25), key);

				if(Event.current.Equals(Event.KeyboardEvent(KeyCode.Keypad1.ToString()))){
					message = "Authorized personnel only. You need a " + neededType + " key of type " + neededType ;
					input = input + "1";
				}

				if(Event.current.Equals(Event.KeyboardEvent(KeyCode.Keypad2.ToString()))){
					message = "Authorized personnel only. You need a " + neededType + " key of type " + neededType ;
					input = input + "2";
				}

				if(Event.current.Equals(Event.KeyboardEvent(KeyCode.Keypad3.ToString())))
				{
					message = "Authorized personnel only. You need a " + neededType + " key of type " + neededType ;
					input = input + "3";
				}

				if(Event.current.Equals(Event.KeyboardEvent(KeyCode.Keypad4.ToString())))
				{
					message = "Authorized personnel only. You need a " + neededType + " key of type " + neededType ;
					input = input + "4";
				}

				if(Event.current.Equals(Event.KeyboardEvent(KeyCode.Keypad5.ToString())))
				{
					message = "Authorized personnel only. You need a " + neededType + " key of type " + neededType ;
					input = input + "5";
				}

				if(Event.current.Equals(Event.KeyboardEvent(KeyCode.Keypad6.ToString())))
				{
					message = "Authorized personnel only. You need a " + neededType + " key of type " + neededType ;
					input = input + "6";
				}

				if(Event.current.Equals(Event.KeyboardEvent(KeyCode.Keypad7.ToString())))
				{
					message = "Authorized personnel only. You need a " + neededType + " key of type " + neededType ;
					input = input + "7";
				}

				if(Event.current.Equals(Event.KeyboardEvent(KeyCode.Keypad8.ToString())))
				{
					message = "Authorized personnel only. You need a " + neededType + " key of type " + neededType ;
					input = input + "8";
				}

				if(Event.current.Equals(Event.KeyboardEvent(KeyCode.Keypad9.ToString())))
				{
					message = "Authorized personnel only. You need a " + neededType + " key of type " + neededType ;
					input = input + "9";
				}

				if(Input.GetKeyDown(KeyCode.Keypad0))
				{
					message = "Authorized personnel only. You need a " + neededType + " key of type " + neededType ;
					input = input + "0";
				}

				if(Event.current.Equals(Event.KeyboardEvent(KeyCode.Backspace.ToString())))
				{
					message = "Authorized personnel only. You need a " + neededType + " key of type " + neededType ;
					input = input.Substring(0, input.Length - 1);
				}
			}
			if (input.Length == 6){
				if(input == curPassword){
					doorLocked = false;
					doorOpen = true;
				} else {
					message = "Wrong code, please try again.";
					input = "";
				}
			}
		}
	}
}