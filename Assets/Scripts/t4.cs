using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class t4 : MonoBehaviour {
	
	public Texture texture;
	
	private string map = "" +
		"### #" +
		"# # #" +
		"#   #" +
		"### #" +
		"  #  " +
		"#   #";
	
	private int sizeX = 5;
	private int sizeY = 6;
	
	private char GetField(string map, int sizeX, int x, int y) {
		return map[y * sizeX + x];
	}
	
	private void PlacePlane(int sizeX, int sizeY, float h, bool isCeiling) {
		GameObject plane = GameObject.CreatePrimitive(PrimitiveType.Plane);
		plane.transform.position = new Vector3(sizeX / 2f, h, sizeY / 2f);
		plane.transform.rotation = Quaternion.Euler(isCeiling ? 180 : 0, 0, 0);
		plane.transform.localScale = new Vector3(sizeX / 10f, 1f, sizeY / 10f);
	}

	void Start () {
		PlacePlane(sizeX, sizeY, 1f, true);
		PlacePlane(sizeX, sizeY, 0f, false);
		
        for (int y = 0; y < sizeY; y++) {
            for (int x = 0; x < sizeX; x++) {
				if(GetField(map, sizeX, x, y) == '#') {
					GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
					cube.AddComponent<Rigidbody>();
					cube.transform.position = new Vector3(x + .5f, .5f, y + .5f);
					//cube.transform.localScale = new Vector3(Random.value, Random.value, Random.value);
					cube.GetComponent<Renderer>().material.mainTexture = texture;
				}
            }
        }
	}
	
	void Update () {
		
	}
}
