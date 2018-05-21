using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class t4 : MonoBehaviour {
	
	public t4person personScript;
	public t4labyrinth labyrinthScript;
	public Text scoreText;
	
	private const float ceilingHeight = 3f;
	private int lvl = 2;
	private int points = 0;
	
	private Vector2 currentPosition;
	
	public void NextLevel() {
		lvl++;
		print("Going to the next level");
		LoadLevel();
	}
	
	public void SzalamiCollected() {
		print(++points);
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
	}
}
