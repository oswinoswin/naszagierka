using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class t4torches : MonoBehaviour {
	
	/*
	The torches will be placed on fields (x[i], y[i]) on one of the walls (n/e/s/w)
	*/
	
	public GameObject torchPrefab;
	
	private List<GameObject> torches = new List<GameObject>();
	
	public void PlaceTorches() {
		ClearAll();
		
		int[] xs = t4maps.xs;
		int[] ys = t4maps.ys;
		char[] orientations = t4maps.orientations;
		bool[] ceiling = t4maps.ceiling;
		
		for(int i=0; i<xs.Length; i++) {
			PlaceTorch(xs[i], ys[i], orientations[i], ceiling[i]);
		}
	}
	
	private void PlaceTorch(int x, int y, char orientation, bool ceil) {
		float hRelative = .2f;
		float h = ceil ? 3f - hRelative : hRelative;
		int angleRelative = 30;
		int angle = ceil ? 180 - angleRelative : angleRelative;
		
		Vector3 pos = new Vector3(x + .5f, h, y + .5f);
		Quaternion rot = Quaternion.identity;
		
		switch(orientation) {
			case 'n':
				pos[2] -= .6f;
				rot = Quaternion.Euler(angle, 0, 0);
				break;
			case 'e':
				pos[0] += .6f;
				rot = Quaternion.Euler(0, 0, angle);
				break;
			case 's':
				pos[2] += .6f;
				rot = Quaternion.Euler(-angle, 0, 0);
				break;
			case 'w':
				pos[0] -= .6f;
				rot = Quaternion.Euler(0, 0, -angle);
				break;
		}
		
		GameObject torch = Instantiate(torchPrefab, pos, rot);
		torches.Add(torch);
	}
	
	private void ClearAll() {
		torches.ForEach(Destroy);
		torches.Clear();
	}
	
	void Start() {
		PlaceTorches();
	}
}
