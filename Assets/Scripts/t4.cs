using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class t4 : MonoBehaviour {
	
	public Texture texture;
	
	private string map = "" +
		"###      #" +
		"# #      #" +
		"#        #" +
		"###      #" +
		"  #      #" +
		"#    #####";
	private int sizeX = 5;
	private int sizeY = 6;
	private float[] lightX = {1.5f};
	private float[] lightY = {4.5f};
	private Vector3 startingPosition = new Vector3(2.5f ,0.5f ,4.5f);
	private Quaternion startingRotation = new Quaternion(0,0,0, 1);
	private string playerName = "FPSController";
	private GameObject player;
	private float ceilingHeight = 3f;
	private bool toggleGravity = false;
    private static Vector3 down = new Vector3(0.0f, -9.8f, 0f);
    private static Vector3 up = new Vector3(0f, 9.8f, 0f);
	
	
	private char GetField(string map, int sizeX, int x, int y) {
		return map[y * sizeX + x];
	}
	
	private void PlacePlane(int sizeX, int sizeY, float h, bool isCeiling) {
		GameObject plane = GameObject.CreatePrimitive(PrimitiveType.Plane);
		plane.transform.position = new Vector3(sizeX / 2f, h, sizeY / 2f);
		plane.transform.rotation = Quaternion.Euler(isCeiling ? 180 : 0, 0, 0);
		plane.transform.localScale = new Vector3(sizeX / 10f, 1f, sizeY / 10f);
	}
	
	private void PlaceTorches() {
		
		for(int i=0; i<lightX.Length; i++) {
			GameObject lightGameObject = new GameObject("The Light");
			Light lightComp = lightGameObject.AddComponent<Light>();
			lightComp.color = new Color(.8f, .6f, 0f, 1f);
			lightGameObject.transform.position = new Vector3(lightX[i], .5f, lightY[i]);
		}
	}
	
	private void PlacePlayer(Vector3 position, Quaternion rotation){
		player = GameObject.Find(playerName);
		player.transform.position = position;
	}
	
	private void ToggleGravity(){
		if (Input.GetKey(KeyCode.G)){
            toggleGravity = !toggleGravity;
            print(Physics.gravity);
            if (toggleGravity){
                Physics.gravity = up;
            }
            else {
                Physics.gravity = down;
            }
        } 
	}
	
	void Start () {
		PlacePlane(sizeX, sizeY, ceilingHeight, true);
		PlacePlane(sizeX, sizeY, 0f, false);
		PlaceTorches();
				
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
		
		PlacePlayer(startingPosition, startingRotation);
	}
	
	void Update () {
		ToggleGravity();
	}
}
