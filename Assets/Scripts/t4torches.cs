using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class t4torches : MonoBehaviour {
	
	/*
	The torches will be placed on fields (x[i], y[i]) on one of the walls (n/e/s/w)
	*/
	
	public GameObject torchPrefab;
	
	private int[] xs = {1, 2, 3, 3};
	private int[] ys = {1, 1, 1, 2};
	private char[] orientations = {'w', 'n', 'e', 's'};
	private const float h = .2f;
	
	void Start () {
		for(int i=0; i<xs.Length; i++) {
			Vector3 pos = new Vector3(xs[i] + .5f, h, ys[i] + .5f);
			Quaternion rot = Quaternion.identity;
			
			switch(orientations[i]) {
				case 'n':
					pos[2] -= .6f;
					rot = Quaternion.Euler(30, 0, 0);
					break;
				case 'e':
					pos[0] += .6f;
					rot = Quaternion.Euler(0, 0, 30);
					break;
				case 's':
					pos[2] += .6f;
					rot = Quaternion.Euler(-30, 0, 0);
					break;
				case 'w':
					pos[0] -= .6f;
					rot = Quaternion.Euler(0, 0, -30);
					break;
			}
			
			Instantiate(torchPrefab, pos, rot);
		}
	}
	
	void Update () {
	}
}
