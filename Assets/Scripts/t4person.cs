using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class t4person : MonoBehaviour {
	
	private Quaternion startingRotation = new Quaternion(0,0,0,1);
	private static string playerName = "FPSController";
	private static GameObject player;
	public static bool toggleGravity = false;
    private static Vector3 down = new Vector3(0.0f, -9.8f, 0f);
    private static Vector3 up = new Vector3(0f, 9.8f, 0f);
	public static float heightUp = 2.5f;
	public static float heightDown = 0.5f;
	
	private Vector3 GetCoords(int x, int y, float h) {
		print(new Vector3(x + .5f, h, y + .5f));
		return new Vector3(x + .5f, h, y + .5f);
	}
	
	public static void PlacePlayer(Vector3 position, Quaternion rotation){
		player = GameObject.Find(playerName);
		player.transform.position = position;
	}
	
	private void MoveUp(){
		Vector3 position = player.transform.position;
		Vector3 newPosition = new Vector3(position[0], heightUp, position[2]);
		player.transform.position = newPosition;
	}
	
		private void MoveDown(){
		Vector3 position = player.transform.position;
		Vector3 newPosition = new Vector3(position[0], heightUp, position[2]);
		player.transform.position = newPosition;
	}
	
	private void ToggleGravity(){
		if (Input.GetKey(KeyCode.G)){
            toggleGravity = !toggleGravity;
            //print(Physics.gravity);
            if (toggleGravity){
                Physics.gravity = up;
				MoveUp();
            }
            else {
                Physics.gravity = down;
				MoveDown();
            }
        } 
	}
	
	void Start () {
		PlacePlayer(GetCoords(1, 1, 1f), startingRotation);
	}
	
	void Update () {
		ToggleGravity();
		Vector3 position = player.transform.position;
		print(position);
	}
}
