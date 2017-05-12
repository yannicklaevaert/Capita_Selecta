using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Lasers : MonoBehaviour {

	void OnTriggerEnter (Collider other) {
		if (other.gameObject.tag == "Player") {
			GameManager.enemyCollision = true;
		}
	}
}