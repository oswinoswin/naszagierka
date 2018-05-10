﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

	
public class t4spikes : MonoBehaviour {
	public GameObject spikesPrefab;
	
	private float ceilingHeight = 3f;
	private List<GameObject> spikes = new List<GameObject>();
	private List<GameObject> spikesBases = new List<GameObject>();
	private Dictionary<int, GameObject> coordsSpikes = new Dictionary<int, GameObject>();
	
	public void PlaceSpikes(int x, int y, float floorH, bool isCeiling, int id) {
		float oppositeH = ceilingHeight - floorH;
		
		GameObject spike = Instantiate(spikesPrefab, new Vector3(x+.5f, floorH, y+.5f), Quaternion.Euler(isCeiling ? 90 : -90, 0, 90));
		spike.name = "Spikes";
		coordsSpikes[id] = spike;	
		spikes.Add(spike);
		print(coordsSpikes[id]);
		
		GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
		cube.name = "SpikesBase";
		cube.transform.position = new Vector3(x+.5f, floorH, y+.5f);
		cube.transform.localScale = new Vector3(.8f, .1f, .8f);
		spikesBases.Add(cube);
	}
	
	private float animateSpikesF(float t) {
		return Mathf.Sin(Time.realtimeSinceStartup) + 1f + ceilingHeight;
	}
	
	private void animateSpikes() {
		foreach(GameObject spike in spikes) {
			Vector3 pos = spike.transform.position;
			spike.transform.position = new Vector3(pos[0], animateSpikesF(Time.realtimeSinceStartup), pos[2]);
		}
	}
	
	public void ClearAll() {
		spikesBases.ForEach(Destroy);
		spikesBases.Clear();
	}
	
	void Update() {
		animateSpikes();
	}
}