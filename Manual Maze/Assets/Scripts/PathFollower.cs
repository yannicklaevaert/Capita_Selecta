using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class PathFollower : MonoBehaviour {

	public Transform[] path;
	public Quaternion rot;
	public float speed = 1.0f;
	public float turnSpeed = 5.0f;
	public float reachDist = 0.5f;
	public int currentPoint = 0;
	public Vector3 dir;

	//Variables for sight
	public float heightMultiplier;
	public float sightDist = 10;
	
	// Update is called once per frame
	void Update () {
		if (path.Length > 0){
		dir = path[currentPoint].position - transform.position;

		transform.position += dir * Time.deltaTime * speed;	

		rot = Quaternion.LookRotation(dir);
   		// Slerp to it over time:
   		transform.rotation = Quaternion.Slerp(transform.rotation, rot, turnSpeed * Time.deltaTime);	

		if (dir.magnitude <= reachDist){
			currentPoint++;
		}

		if (currentPoint >= path.Length){
			currentPoint = 0;
		}
	}
	}

	void OnDrawGizmos(){
		if (path.Length > 0)
		for(int i = 0; i<path.Length; i++){
			if(path[i] != null){
				Gizmos.DrawSphere(path[i].position, reachDist);
			}
		}
	}

	void OnTriggerEnter (Collider other) {
		if (other.gameObject.tag == "Player") {
			GameManager.enemyCollision = true;
		}
	}
}
