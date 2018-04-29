using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class t4torches : MonoBehaviour {
	
	/*
	The torches will be placed on fields (x[i], y[i]) on one of the walls (n/e/s/w)
	*/
	
	public GameObject torchPrefab;
	
	private int[] xs = {1, 2, 3, 3, 1};
	private int[] ys = {1, 1, 1, 2, 1};
	private char[] orientations = {'w', 'n', 'e', 's', 'w'};
	private bool[] ceiling = {false, false, false, false, true};
	
	private bool Validate() {
		if(!(xs.Length == ys.Length && xs.Length == orientations.Length && xs.Length == ceiling.Length)) {
			Debug.LogError("Bad array dimensions!");
			return false;
		}
		return true;
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
		
		Instantiate(torchPrefab, pos, rot);
	}
	
	void Start () {
		Validate();
		
		for(int i=0; i<xs.Length; i++) {
			PlaceTorch(xs[i], ys[i], orientations[i], ceiling[i]);
		}
	}
	
	void Update () {
	}
}
