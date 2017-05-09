using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFollower : MonoBehaviour {

	public Transform[] path;
	public Quaternion rot;
	public float speed = 1.0f;
	public float turnSpeed = 5.0f;
	public float reachDist = 0.5f;
	public int currentPoint = 0;

	//Variables for sight
	public float heightMultiplier;
	public float sightDist = 10;
	
	// Update is called once per frame
	void Update () {
		//Investigate();
		Vector3 dir = path[currentPoint].position - transform.position;

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

	/*void Investigate(){
		RaycastHit hit;
		Debug.DrawRay(transform.position + Vector3.up / heightMultiplier, transform.forward * sightDist, Color.green);
		Debug.DrawRay(transform.position + Vector3.up / heightMultiplier, (transform.forward + transform.right).normalized * sightDist, Color.green);
		Debug.DrawRay(transform.position + Vector3.up / heightMultiplier, (transform.forward - transform.right).normalized * sightDist, Color.green);
		if (Physics.Raycast(transform.position + Vector3.up / heightMultiplier, transform.forward, out hit, sightDist)){
			if (hit.collider.gameObject.tag == "Player"){
				GameManager.enemyCollision = true;
			}
		}
		if (Physics.Raycast(transform.position + Vector3.up / heightMultiplier, (transform.forward + transform.right).normalized, out hit, sightDist)){
			if (hit.collider.gameObject.tag == "Player"){
				GameManager.enemyCollision = true;
			}
		}
		if (Physics.Raycast(transform.position + Vector3.up / heightMultiplier, (transform.forward - transform.right).normalized, out hit, sightDist)){
			if (hit.collider.gameObject.tag == "Player"){
				GameManager.enemyCollision = true;
			}
		}
	}*/

	/*void OnTriggerExit (Collider other) {
		/*if (other.gameObject.tag == "Player") {
			hinge.localRotation = Quaternion.identity;
		}*/
		/*this.other = other;
		onTrigger = false;
		input = "";
		doorClosed = true;
	}*/
}
