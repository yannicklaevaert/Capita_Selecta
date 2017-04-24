using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	public float moveSpeed = 5.0f; 

	void Start() {
		Cursor.lockState = CursorLockMode.Locked;
	}

	// Update is called once per frame
	void FixedUpdate () {
		float moveX = Input.GetAxis ("Horizontal");
		float moveZ = Input.GetAxis ("Vertical");


		moveX *= (moveSpeed * Time.deltaTime);
		moveZ *= (moveSpeed * Time.deltaTime);
		// Move
		//Vector3 movement = new Vector3(moveX, 0f, moveZ);
		//GetComponent<Rigidbody> ().velocity = movement * moveSpeed * Time.deltaTime;
		transform.Translate(moveX, 0, moveZ);

		if (Input.GetKeyDown ("escape")) {
			Cursor.lockState = CursorLockMode.None;
		}
	}
}
