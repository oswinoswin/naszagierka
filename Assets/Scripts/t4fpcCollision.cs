using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class t4fpcCollision : MonoBehaviour {
	
	void OnTriggerEnter(Collider other) {
		if(other.name == "Lava") {
			t4person.PlacePlayer(new Vector3(1.5f, 1.5f, 1f), new Quaternion(0, 0, 0, 1));
		}
	}
}
