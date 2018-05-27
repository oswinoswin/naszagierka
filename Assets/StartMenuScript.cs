using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuScript : MonoBehaviour {

	public void PlayGame(){
		SceneManager.LoadScene(1);
	}
	
	public void QuitGame(){
		Debug.Log("Quit");
		Application.Quit();
	}
	void Update(){
		if (Input.GetKey(KeyCode.Q) ){
			Application.Quit();
		}
	}
}
