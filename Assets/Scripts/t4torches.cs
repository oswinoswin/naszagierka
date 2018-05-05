using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class t4torches : MonoBehaviour {
	
	/*
	The torches will be placed on fields (x[i], y[i]) on one of the walls (n/e/s/w)
	*/
	
	public GameObject torchPrefab;
	
	private List<GameObject> torches = new List<GameObject>();
	
	public void PlaceTorches(int lvl) {
		t4maps.Torch[] torchesData = t4maps.levels[lvl].torches;
		
		for(int i=0; i<torchesData.Length; i++) {
			PlaceTorch(torchesData[i]);
		}
	}
	
	private void PlaceTorch(t4maps.Torch t) {
		
		float hRelative = .2f;
		float h = t.ceiling ? 3f - hRelative : hRelative;
		int angleRelative = 30;
		int angle = t.ceiling ? 180 - angleRelative : angleRelative;
		
		Vector3 pos = new Vector3(t.x + .5f, h, t.y + .5f);
		Quaternion rot = Quaternion.identity;
		
		switch(t.orientation) {
			case 'n':
				pos[2] -= .6f;
				rot = Quaternion.Euler(angle, 0, 0);
				break;
			case 'e':
				pos[0] += .6f;
				rot = Quaternion.Euler(0, 0, angle);
				break;
			case 's':
				pos[2] += .6f;
				rot = Quaternion.Euler(-angle, 0, 0);
				break;
			case 'w':
				pos[0] -= .6f;
				rot = Quaternion.Euler(0, 0, -angle);
				break;
		}
		
		GameObject torch = Instantiate(torchPrefab, pos, rot);
		torches.Add(torch);
	}
	
	public void ClearAll() {
		torches.ForEach(Destroy);
		torches.Clear();
	}
}
