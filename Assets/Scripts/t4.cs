using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class t4 : MonoBehaviour {
	
	public t4person personScript;
	public t4labyrinth labyrinthScript;
	public Text scoreText;
	public Text youDiedText;
	
	private const float ceilingHeight = 3f;
	private int lvl = 2;
	private int points = 0;
	private float lastDeath = 0f;
	
	private Vector2 currentPosition;
	
	public void NextLevel() {
		lvl++;
		print("Going to the next level");
		LoadLevel();
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
	}
	
	void Update() {
		scoreText.text = points.ToString();
		youDiedText.gameObject.SetActive(Time.realtimeSinceStartup - lastDeath <= 1.6f);
	}
}
