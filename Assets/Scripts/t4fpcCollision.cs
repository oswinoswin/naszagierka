using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class t4fpcCollision : MonoBehaviour {
	
	public t4 main;
	
	void OnTriggerEnter(Collider other) {
		print("Collision with: " + other.name);
		
		if(other.name == "Lava" || other.name == "Spikes") {
			t4person.PlacePlayer(new Vector3(1.5f, 1.5f, 1f), new Quaternion(0, 0, 0, 1));
		} else if(other.name == "Door") {
			main.NextLevel();
		} else if(other.name == "Szalami") {
			main.SzalamiCollected();
			Destroy(other.gameObject);
		}
	}
}
