﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class t4torches : MonoBehaviour {
	
	public GameObject torchPrefab;
	
	private int[] xs = {1};
	private int[] ys = {1};
	private char[] orientations = {'w'};
	private const float h = .2f;
	
	void Start () {
		for(int i=0; i<xs.Length; i++) {
			Vector3 pos = new Vector3(xs[i] + .5f, h, ys[i] + .5f);
			Quaternion rot = Quaternion.Euler(30, 0, 0);
			
			switch(orientations[i]) {
				case 'w':
					pos[2] -= .6f;
					break;
			}
			
			Instantiate(torchPrefab, pos, rot);
		}
	}
	
	void Update () {
	}
}