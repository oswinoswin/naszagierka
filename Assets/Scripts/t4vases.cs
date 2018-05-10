using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class t4vases : MonoBehaviour {
	
	/*
	The vases will be placed on fields (x[i], y[i]) closer to one of the walls (n/e/s/w)
	*/
	
	public GameObject vasePrefab;
	
	private List<GameObject> vases = new List<GameObject>();
	
	public void PlaceVases(int lvl) {
		t4maps.Vase[] vasesData = t4maps.levels[lvl].vases;
		
		for(int i=0; i<vasesData.Length; i++) {
			PlaceVase(vasesData[i]);
		}
	}
	
	private void PlaceVase(t4maps.Vase t) {
		
		float h = t.ceiling ? 3f : 0f;
		float dist = .2f;
		int angleRelative = 30;
		int angle = t.ceiling ? 180 - angleRelative : angleRelative;
		
		Vector3 pos = new Vector3(t.x + .5f, h, t.y + .5f);
		Quaternion rot = Quaternion.Euler(0, 45, 0);
		
		switch(t.orientation) {
			case 'n':
				pos[2] -= dist;
				break;
			case 'e':
				pos[0] += dist;
				break;
			case 's':
				pos[2] += dist;
				break;
			case 'w':
				pos[0] -= dist;
				break;
		}
		
		GameObject vase = Instantiate(vasePrefab, pos, rot);
		vases.Add(vase);
	}
	
	public void ClearAll() {
		vases.ForEach(Destroy);
		vases.Clear();
	}
}
