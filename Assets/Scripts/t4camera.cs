using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class t4camera : MonoBehaviour {

	void Start () {
		transform.position = new Vector3(2.5f, .5f, 6.5f);
		transform.rotation = Quaternion.Euler(0, 180, 0);
	}
	
	void Update () {
		
	}
}
