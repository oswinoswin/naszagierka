using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class t4person : MonoBehaviour {
	
	private Quaternion startingRotation = new Quaternion(0,0,0,1);
	public Text warningText;
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
	
	IEnumerator JumpToCeiling ()
    {
		Physics.gravity = up;
		Vector3 tmp = player.transform.position;
		player.transform.position = new Vector3(tmp[0], heightUp, tmp[2]);
        yield return new WaitForSeconds((float)UnityEngine.Random.Range(3, 6));
		warningText.gameObject.SetActive(true);
		Physics.gravity = down;
        yield return new WaitForSeconds(4f);
		warningText.gameObject.SetActive(false);
    }
	
	private void ToggleGravity(){
		if (Physics.gravity[1] < 0){       
			StartCoroutine(JumpToCeiling());
        }
	}
	
	public void Reset() {
		PlacePlayer(GetCoords(1, 1, 1f), startingRotation);
	}
	
	void Start () {
		PlacePlayer(GetCoords(1, 1, 1f), startingRotation);
		lastAction = DateTime.Now;
		lastPress = DateTime.Now;
		warningText.gameObject.SetActive(false);
	}
	
	void Update () {
	}
	
	void FixedUpdate(){
		if (Input.GetKey(KeyCode.G) ){
			ToggleGravity();
		}
	}
}
