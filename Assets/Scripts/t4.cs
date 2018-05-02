using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class t4 : MonoBehaviour {
	
	public Texture texture;
	public Texture lavaTexture;
	public Texture heightMap;
	public GameObject szalamiPrefab;
	
	private const float ceilingHeight = 3f;
	private int lvl = 0;
	private int points = 0;
	
	private Vector2 currentPosition;
	
	public void NextLevel() {
		lvl++;
		print("Going to the next level");
		//LoadLevel();
	}
	
	public void SzalamiCollected() {
		print(++points);
	}
	
	private void LoadLevel() {
		t4labyrinth.BuildLabyrinth(lvl, texture, lavaTexture, heightMap, szalamiPrefab);
	}
	
	void Start () {
		LoadLevel();
	}
	
	void Update() {
	}
}
