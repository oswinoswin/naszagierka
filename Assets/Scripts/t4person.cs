using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class t4person : MonoBehaviour {
	
	private Quaternion startingRotation = new Quaternion(0,0,0,1);
	private static string playerName = "FPSController";
	private static GameObject player;
	public static bool directionUp = false;
	private static bool shuldChangeHeight = false;
    private static Vector3 down = new Vector3(0.0f, -9.8f, 0f);
    private static Vector3 up = new Vector3(0f, 9.8f, 0f);
	public static float heightUp = 2.2f;
	public static float heightDown = 0.5f;
	DateTime lastAction;
	DateTime lastPress;
	TimeSpan waitTime = new TimeSpan(0,0,0,0,300);
	Vector3 tmp = new Vector3(0,0,0);
	
	
	private Vector3 GetCoords(int x, int y, float h) {
		//print(new Vector3(x + .5f, h, y + .5f));
		return new Vector3(x + .5f, h, y + .5f);
	}
	
	public static void PlacePlayer(Vector3 position, Quaternion rotation){
		player = GameObject.Find(playerName);
		player.transform.position = position;
	}
	
	private void MoveUp(){
		print("MoveUp");
		
		
		tmp = player.transform.position;
		Vector3 newPosition = new Vector3(tmp[0], heightUp, tmp[2]);
		player.transform.position = newPosition;
		Physics.gravity = up;
		print(player.transform.position);
	}
	
		private void MoveDown(){
		print("MoveDown");
		
		tmp = player.transform.position;
		Vector3 newPosition = new Vector3(tmp[0], heightDown, tmp[2]);
		player.transform.position = newPosition;
		Physics.gravity = down;
		print(player.transform.position);
	}
	
	private void ToggleGravity(){
		if (directionUp){               
				MoveUp();
            }
        else{           
			MoveDown();
        }
	}
	private void CheckForGravityChange(){
		lastPress = DateTime.Now;
		TimeSpan delta = lastPress - lastAction;
		if(delta < waitTime){
			return;
		}
		if (Input.GetKey(KeyCode.G) ){
			directionUp = !directionUp;
			lastAction = lastPress;
			shuldChangeHeight = true;
		}
	}
	
	public void Reset() {
		PlacePlayer(GetCoords(1, 1, 1f), startingRotation);
		MoveDown();
	}
	
	void Start () {
		PlacePlayer(GetCoords(1, 1, 1f), startingRotation);
		lastAction = DateTime.Now;
		lastPress = DateTime.Now;
	}
	
	void Update () {	
		CheckForGravityChange();
	}
	
	void FixedUpdate(){
		if(shuldChangeHeight){
			ToggleGravity();
			shuldChangeHeight = false;
		}
		Vector3 currentPosition = player.transform.position;
		print("DEBUG" + currentPosition);
	}
}
