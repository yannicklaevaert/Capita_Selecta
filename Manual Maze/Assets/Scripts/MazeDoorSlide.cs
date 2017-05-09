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
			AudioSource.PlayClipAtPoint(slide, transform.position);
		}
	}
		

	void OnTriggerExit(Collider col)
	{
		if(doorOpen)
		{
			doorOpen = false;
			
		}
	}

	void DoorControl(string direction)
	{
		animator.SetTrigger(direction);
	}

}
