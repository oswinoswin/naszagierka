using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class t1 : MonoBehaviour {
	
	Rigidbody rb;
	public Transform cube;
	const int cubeN = 15;
	Transform[] cubes = new Transform[cubeN];

	// Use this for initialization
	void Start () {
		
		for (int i = 0; i < cubeN; i++) {
            cubes[i] = (Transform) Instantiate(cube, new Vector3(Random.value*6-3, Random.value*7 + 2, Random.value*6-3), Quaternion.identity);
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
