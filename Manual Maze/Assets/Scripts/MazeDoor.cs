using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class MazeDoor : MonoBehaviour {

	public Transform hinge;


	void OnTriggerEnter (Collider other) {
		if (other.gameObject.tag == "Player") {
			OpenDoor (other);
		}
//		if (other.gameObject.tag == "Player") {
//			Debug.Log (gameObject.transform.rotation.y);
//			if (gameObject.transform.rotation == Quaternion.identity) {
//				if (other.gameObject.transform.position.z > gameObject.transform.position.z + 0.5) {
//					hinge.localRotation = Quaternion.Euler (0f, 90f, 0f);
//				} else if (other.gameObject.transform.position.z < gameObject.transform.position.z + 0.5) {
//					hinge.localRotation = Quaternion.Euler (0f, -90f, 0f);
//				}
//			}
//			else if (gameObject.transform.rotation == Quaternion.Euler (0f, 90f, 0f)) {
//				Debug.Log ("Test");
//				if (other.gameObject.transform.position.x > gameObject.transform.position.x + 0.5) {
//					hinge.localRotation = Quaternion.Euler (0f, 90f, 0f);
//				} else if (other.gameObject.transform.position.x < gameObject.transform.position.x + 0.5) {
//					hinge.localRotation = Quaternion.Euler (0f, -90f, 0f);
//				}
//			}
//			else if (gameObject.transform.rotation == Quaternion.Euler (0f, 180f, 0f)) {
//				if (other.gameObject.transform.position.z < gameObject.transform.position.z - 0.5) {
//					hinge.localRotation = Quaternion.Euler (0f, 90f, 0f);
//				} else if (other.gameObject.transform.position.z > gameObject.transform.position.z - 0.5) {
//					hinge.localRotation = Quaternion.Euler (0f, -90f, 0f);
//				}
//			}
//			else if (gameObject.transform.rotation == Quaternion.Euler (0f, 270f, 0f)) {
//				if (other.gameObject.transform.position.x < gameObject.transform.position.x - 0.5) {
//					hinge.localRotation = Quaternion.Euler (0f, 90f, 0f);
//				} else if (other.gameObject.transform.position.x > gameObject.transform.position.x - 0.5) {
//					hinge.localRotation = Quaternion.Euler (0f, -90f, 0f);
//				}
//			}
//		}
	}

	void OnTriggerExit (Collider other) {
		if (other.gameObject.tag == "Player") {
			hinge.localRotation = Quaternion.identity;
		}
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
}
