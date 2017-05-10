using UnityEngine;
using System.Collections;

public class MazeDoorSlide : MonoBehaviour {

	Animator animator;
	bool doorOpen;
	public AudioClip slide;

	void Start()
	{
		doorOpen = false;
		animator = GetComponent<Animator>();
	}

	void OnTriggerEnter(Collider col)
	{
		if(col.gameObject.tag == "Player")
		{
			doorOpen = true;
			DoorControl("Open");
		}
	}
		

	void OnTriggerExit(Collider col)
	{
		if(doorOpen)
		{
			doorOpen = false;
			DoorControl("Close");
		}
	}

	void DoorControl(string direction)
	{
		AudioSource.PlayClipAtPoint(slide, transform.position);
		animator.SetTrigger(direction);
	}

}
