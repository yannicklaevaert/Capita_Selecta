using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	public void LoadScene(){
		SceneManager.LoadScene("MainMenu");
	}

	public void LoadGame(){
		GameManager.time = 200.0f;
		GameManager.alive = true;
		GameManager.enemyCollision = false;
		SceneManager.LoadScene("Level3");
	}
}
