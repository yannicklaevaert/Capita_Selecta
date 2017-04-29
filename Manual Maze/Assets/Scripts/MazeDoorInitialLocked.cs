using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MazeDoorInitialLocked : MazeDoor {

	public int code;

	public int givenCode;

	private bool isOpen = false;

	void OnTriggerEnter (Collider other) {
		if (other.gameObject.tag == "Player") {
			if (isOpen) {
				OpenDoor (other);
			}
			else {
					if (givenCode == code) {
						isOpen = true;
						OpenDoor (other);
					} else {
						//Code not correct
					}
			}
		}
	}

	void OnTriggerExit (Collider other) {
		if (other.gameObject.tag == "Player") {
			if (isOpen) {
				hinge.localRotation = Quaternion.identity;
			}
		}
	}


}
