using UnityEngine;
using System.Collections;

public class MazeDoorSlideLocked : MonoBehaviour {

	Animator animator;
	public string curPassword = "123456";
	public static string neededColor = "green";
	public static string neededType = "A";
	public string input;
	bool doorLocked;
	public bool onTrigger;
	public string message = "Authorized personnel only.";
	public string key = "You need a " + neededColor + " key of type " + neededType;
	public AudioClip slide;

	void Start()
	{
		doorLocked = true;
		animator = GetComponent<Animator>();
	}

	void OnTriggerEnter (Collider other) {
		onTrigger = true;
		if (!doorLocked){
			DoorControl("Open");
		}
	}

	void OnTriggerExit (Collider other) {
		onTrigger = false;
		input = "";
		if (!doorLocked){
			DoorControl("Close");
		}
	}
		

	void DoorControl(string direction)
	{
		AudioSource.PlayClipAtPoint(slide, transform.position);
		animator.SetTrigger(direction);
	}


	void OnGUI() 	{
		if(doorLocked)
		{
				if (onTrigger){

				//GUI.Box(new Rect(0, 0, 220, 330), "");
				GUI.Box(new Rect(100, 100, 200, 25), message);
				GUI.Box(new Rect(100, 125, 200, 25), key);
				GUI.Box(new Rect(100, 150, 200, 25), "Enter code to open door:");
				GUI.Box(new Rect(100, 175, 200, 25), input);
				if(Event.current.Equals(Event.KeyboardEvent(KeyCode.Keypad0.ToString()))){
					message = "Authorized personnel only.";
					input = input + "0";
				}

				if(Event.current.Equals(Event.KeyboardEvent(KeyCode.Keypad1.ToString()))){
					message = "Authorized personnel only.";
					input = input + "1";
				}

				if(Event.current.Equals(Event.KeyboardEvent(KeyCode.Keypad2.ToString()))){
					message = "Authorized personnel only.";
					input = input + "2";
				}

				if(Event.current.Equals(Event.KeyboardEvent(KeyCode.Keypad3.ToString())))
				{
					message = "Authorized personnel only.";
					input = input + "3";
				}

				if(Event.current.Equals(Event.KeyboardEvent(KeyCode.Keypad4.ToString())))
				{
					message = "Authorized personnel only.";
					input = input + "4";
				}

				if(Event.current.Equals(Event.KeyboardEvent(KeyCode.Keypad5.ToString())))
				{
					message = "Authorized personnel only.";
					input = input + "5";
				}

				if(Event.current.Equals(Event.KeyboardEvent(KeyCode.Keypad6.ToString())))
				{
					message = "Authorized personnel only.";
					input = input + "6";
				}

				if(Event.current.Equals(Event.KeyboardEvent(KeyCode.Keypad7.ToString())))
				{
					message = "Authorized personnel only.";
					input = input + "7";
				}

				if(Event.current.Equals(Event.KeyboardEvent(KeyCode.Keypad8.ToString())))
				{
					message = "Authorized personnel only.";
					input = input + "8";
				}

				if(Event.current.Equals(Event.KeyboardEvent(KeyCode.Keypad9.ToString())))
				{
					message = "Authorized personnel only.";
					input = input + "9";
				}

				if(Event.current.Equals(Event.KeyboardEvent(KeyCode.Backspace.ToString())))
				{
					message = "Authorized personnel only.";
					input = input.Substring(0, input.Length - 1);
				}
			}
			if (input.Length == 6){
				if(input == curPassword){
					doorLocked = false;
					DoorControl ("Open");
				} else {
					message = "Wrong code, please try again.";
					input = "";
				}
			}
		}
	}
}