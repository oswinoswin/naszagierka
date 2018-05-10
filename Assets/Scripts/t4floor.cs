using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class t4floor : MonoBehaviour {
	
	public Texture floorTexture;
	public Texture heightMap;
	
	private Texture2D lt;
	private Material material;
	private List<GameObject> floors = new List<GameObject>();
	private const int resolution = 512;
	
	private char GetField(string map, int sizeX, int x, int y) {
		return map[y * sizeX + x];
	}
	
	private void PlaceFloorFragment(int x1, int x2, int y1, int y2, float h, bool isLava, bool isCeiling) {
		h += (isCeiling ? 1f : -1f) * (isLava ? .2f : .1f);
		
		GameObject plane = GameObject.CreatePrimitive(PrimitiveType.Cube);
		plane.name = isLava ? "Lava" : "Floor";
		plane.transform.position = new Vector3((x1+x2+1)/2f, h, (y1+y2+1)/2f);
		plane.transform.rotation = Quaternion.Euler(isCeiling ? 180 : 0, 0, 0);
		plane.transform.localScale = new Vector3((x2-x1+1), .2f, (y2-y1+1));		
		floors.Add(plane);
		
		if(isLava) {
			material = plane.GetComponent<Renderer>().material;
			material.mainTexture = lt;	
			material.EnableKeyword("_EMISSION");	
			material.SetTexture("_EmissionMap", lt);
			material.SetColor("_EmissionColor", Color.white);
			material.EnableKeyword("_PARALLAXMAP");
			material.SetTexture("_ParallaxMap", lt);
			material.SetFloat("_Parallax", -.5f);
			material.EnableKeyword("_SPECULARHIGHLIGHTS_OFF");	
			material.SetFloat("_SpecularHighlights", 0f);
			material.EnableKeyword("_GLOSSYREFLECTIONS_OFF");	
			material.SetFloat("_GlossyReflections", 0f);
		} else {
			material = plane.GetComponent<Renderer>().material;
			material.mainTexture = floorTexture;
			material.EnableKeyword("_NORMALMAP");
			material.SetTexture("_BumpMap", heightMap);
			material.SetFloat("_BumpScale", 0.5f);
			material.EnableKeyword("_GLOSSYREFLECTIONS_OFF");	
			material.SetFloat("_GlossyReflections", 0f);
					
			Mesh mesh = plane.GetComponent<MeshFilter>().mesh;
			for(int i=0;i<24;i++) print(i + ": " + mesh.uv[i][0] + "" + mesh.uv[i][1] + " ");
			Vector2[] uvs = mesh.uv;
			uvs[5][0] = uvs[9][0] = (x2 - x1 + 1);
			uvs[4][1] = uvs[5][1] = (y2 - y1 + 1);
			mesh.uv = uvs;
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

		blt(sizeX, sizeY);
		PlaceFloorFragment(1, sizeX-2, 1, sizeY-2, h, true, isCeiling);

		if(fullWidthColStart < sizeY-1) {
			PlaceFloorFragment(1, sizeX-2, fullWidthColStart, sizeY-2, h, false, isCeiling);
		}
	}
	
	public void ClearAll() {
		floors.ForEach(Destroy);
		floors.Clear();
	}
	
	private Texture2D blt(int sizeX, int sizeY) {
		Texture2D texture = new Texture2D(resolution, resolution, TextureFormat.RGB24, true);
		FillTexture(texture, sizeX, sizeY);
		lt = texture;
		return texture;
	}

	private void FillTexture (Texture2D texture, int sizeX, int sizeY) {
		float scaleX = 20f;
		float scaleY = scaleX * (float)sizeY / (float)sizeX;
		
		for (int y2 = 0; y2 < resolution; y2++) {
			for (int x = 0; x < resolution; x++) {
				int y = (int)(y2 + 5f*Mathf.Sin((float)y2/25f))%resolution;
				float noise1 = Mathf.PerlinNoise((float)x * scaleX / resolution, (float)y * scaleY / resolution);
				float noise2 = Mathf.PerlinNoise((float)x * scaleX * 2f / resolution, (float)y * scaleY * 2f / resolution) * 2f;
				float noise3 = Mathf.PerlinNoise((float)x * scaleX * 2f / resolution, (float)y * scaleY * 2f / resolution) * 2f;
				float noise = Mathf.Min(noise1, noise2);
				if(noise > .5f) noise = 1-noise;
				
				noise = noise*3f-.8f;
				if(noise>1) noise = 1f;
				if(noise<0) noise = 0f;
				if(noise<.2f) noise = 0f;
				
				float noiseG = noise;
				if(noiseG > .6f) noiseG = 1f;
				noiseG /= 2f;
				
				if(noise > .2f) noise += (noise-.2f) * .1f;
				if(noise3 > .45f && noise3 < .55f && noise == 0f) noise = 1f - Mathf.Abs(noise3 - .5f) * 15; 
				
				float gtor = Random.Range(0f, .3f);
				if(noiseG > .1f) {
					noise += gtor;
					noiseG -= gtor;
				}
					
				texture.SetPixel(x, y2, new Color(noise, noiseG, 0));
			}
		}
		texture.Apply();
	}
}
