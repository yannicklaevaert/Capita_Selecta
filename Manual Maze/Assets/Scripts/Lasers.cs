using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Lasers : MonoBehaviour {

	public float sinlengte;
	public float coslengte;
	public float lengte;
	public bool change = false;
	//public GameObject go = this;

	public static bool rotating = false;

	void OnTriggerEnter (Collider other) {
		if (other.gameObject.tag == "Player") {
			GameManager.enemyCollision = true;
		} /*else if (other.gameObject.tag == "Wall"){
			Debug.Log("Wall");
			Debug.Log(other.gameObject.transform.position);
			if (Math.Abs(other.gameObject.transform.position.z-transform.position.z) <= (float) (transform.localScale.y*0.766)){
				Debug.Log("kleiner");
				lengte = (float) (transform.localScale.y*0.766) - (Math.Abs(other.gameObject.transform.position.z-transform.position.z));
				sinlengte = (float) (0.64 * lengte);
				coslengte = (float) (0.766 * lengte);
				transform.localScale = new Vector3(transform.localScale.x,(float) (transform.localScale.y-lengte/0.766),transform.localScale.z);
				Debug.Log(transform.position.y);
				transform.position = new Vector3(transform.position.x,(transform.position.y + sinlengte),transform.position.z - coslengte);
				change = true;
			}
		}
		 else {
			Debug.Log("not player or wall");
		}*/
	}

	void OnTriggerExit (Collider other) {
		/*Debug.Log("OnTriggerExit");
		if ((other.gameObject.tag == "Wall") && change){
			transform.localScale = new Vector3(transform.localScale.x,(float) (transform.localScale.y+lengte/0.766),transform.localScale.z);
			transform.position = new Vector3(transform.position.x,(transform.position.y - sinlengte),transform.position.z + coslengte);
			change = false;
		}*/
	}

	void OnCollisionEnter(Collision collision)
    {
        Debug.Log("collision");
    }
}