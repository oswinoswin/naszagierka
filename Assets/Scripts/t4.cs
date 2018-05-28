using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class t4 : MonoBehaviour {
	
	public t4person personScript;
	public t4labyrinth labyrinthScript;
	public Text scoreText;
	public Text youDiedText;
	public Text youWonText;
	
	private const float ceilingHeight = 2.7f;
	private int lvl = 0;
	private int points = 0;
	private float lastDeath = 0f;
	
	private Vector2 currentPosition;
	
	public void NextLevel() {
		if(lvl < 3) {
			lvl++;
			LoadLevel();			
		} else {
			youWonText.gameObject.SetActive(true);
			Time.timeScale = 0;
		}
	}
	
	public void SzalamiCollected() {
		print(++points);
	}
	
	public void YouDied() {
		lastDeath = Time.realtimeSinceStartup;
	}
	
	private void LoadLevel() {
		personScript.Reset();
		labyrinthScript.BuildLabyrinth(lvl);
	}
	
	void Start () {
		LoadLevel();
		youWonText.gameObject.SetActive(false);
	}
	
	void Update() {
		scoreText.text = points.ToString();
		youDiedText.gameObject.SetActive(Time.realtimeSinceStartup - lastDeath <= 1.6f);
		if (Input.GetKey(KeyCode.Q) ){
			Application.Quit();
		}
	}
}
