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
	
	private Vector2 currentPosition;
	
	void Start () {
		t4labyrinth.BuildLabyrinth(lvl, texture, lavaTexture, heightMap, szalamiPrefab);
	}
	
	void Update() {
	}
}
