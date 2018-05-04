using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class t4floor : MonoBehaviour {
	
	public Texture lavaTexture;
	
	private List<GameObject> floors = new List<GameObject>();
	
	private char GetField(string map, int sizeX, int x, int y) {
		return map[y * sizeX + x];
	}
	
	private void PlaceFloorFragment(int x1, int x2, int y1, int y2, float h, bool isLava, bool isCeiling) {
		if(!isLava) print("" + x1 + " " + x2 + " " + y1 + " " + y2);
		
		GameObject plane = GameObject.CreatePrimitive(PrimitiveType.Plane);
		plane.name = "Floor";
		plane.transform.position = new Vector3((x1+x2+1)/2f, h, (y1+y2+1)/2f);
		plane.transform.rotation = Quaternion.Euler(isCeiling ? 180 : 0, 0, 0);
		plane.transform.localScale = new Vector3((x2-x1+1)/10f, 1f, (y2-y1+1)/10f);		
		floors.Add(plane);
		
		if(isLava) {
			plane.name = "Lava";
			Material material = plane.GetComponent<Renderer>().material;
			material.mainTexture = lavaTexture;	
			material.EnableKeyword("_EMISSION");	
			material.SetColor ("_EmissionColor", new Color(1f, .4f, .1f, 1f));
		}	
	}
	
	public void PlaceFloor(string map, int sizeX, int sizeY, float h, bool isCeiling) {
		int fullWidthColStart = 1;
		
		for (int y = 1; y < sizeY-1; y++) {
			int spanStart = 1;
			
            for (int x = 1; x < sizeX-1; x++) {
				char field = GetField(map, sizeX, x, y);
				
				if(field == '_') {
					if(x > spanStart) {
						PlaceFloorFragment(spanStart, x-1, y, y, h, false, isCeiling);
					}
					PlaceFloorFragment(x, x, y, y, h, true, isCeiling);
					spanStart = x+1;
				}
			}
			
			if(spanStart > 1) {			
				if(spanStart < sizeX-1) {
					PlaceFloorFragment(spanStart, sizeX-2, y, y, h, false, isCeiling);
				}
			
				if(y > fullWidthColStart) {
					PlaceFloorFragment(1, sizeX-2, fullWidthColStart, y-1, h, false, isCeiling);
				}
				fullWidthColStart = y+1;
			}
		}

		if(fullWidthColStart < sizeY-1) {
			PlaceFloorFragment(1, sizeX-2, fullWidthColStart, sizeY-2, h, false, isCeiling);
		}
	}
	
	public void ClearAll() {
		floors.ForEach(Destroy);
		floors.Clear();
	}
}
