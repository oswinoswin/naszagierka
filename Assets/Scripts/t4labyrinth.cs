using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class t4labyrinth : MonoBehaviour {
	
	private const float ceilingHeight = 3f;
	
	
	private static char GetField(string map, int sizeX, int x, int y) {
		return map[y * sizeX + x];
	}
	
	private static void PlaceFloor(string map, int sizeX, int sizeY, float h, bool isCeiling, Texture lavaTexture) {
		for (int x = 1; x < sizeX-1; x++) {
            for (int y = 1; y < sizeY; y++) {
				char field = GetField(map, sizeX, x, y);
				if(field == '#') {
					continue;
				}
				
				GameObject plane = GameObject.CreatePrimitive(PrimitiveType.Plane);
				plane.transform.position = new Vector3(x+.5f, h, y+.5f);
				plane.transform.rotation = Quaternion.Euler(isCeiling ? 180 : 0, 0, 0);
				plane.transform.localScale = new Vector3(.1f, 1f, .1f);		
				if(field == '_') {
					plane.name = "Lava";
					Material material = plane.GetComponent<Renderer>().material;
					material.mainTexture = lavaTexture;	
					material.EnableKeyword("_EMISSION");	
					material.SetColor ("_EmissionColor", new Color(1f, .4f, .1f, 1f));
				}	
			}
		}
	}
	
	private static void PlaceSzalami(string map, int sizeX, int sizeY, float floorH, bool isCeiling, GameObject szalamiPrefab) {
		float h = isCeiling ? floorH - .4f : floorH + .4f;
		for (int x = 1; x < sizeX-1; x++) {
            for (int y = 1; y < sizeY; y++) {	
				char field = GetField(map, sizeX, x, y);
				if(field == 's') {
					GameObject szalami = Instantiate(szalamiPrefab, new Vector3(x+.5f, h, y+.5f), Quaternion.identity);
					szalami.name = "Szalami";
				}	
			}
		}
	}
	
	private static void PlaceOuterWalls(int sizeX, int sizeY, float h, Texture stoneTexture, Texture heightMap) {
		PlaceWall(0, sizeX-1, 0, 0, 0, h, stoneTexture, heightMap);
		PlaceWall(0, 0, 0, sizeY-1, 0, h, stoneTexture, heightMap);
		PlaceWall(0, sizeX-1, sizeY-1, sizeY-1, 0, h, stoneTexture, heightMap);
		PlaceWall(sizeX-1, sizeX-1, 0, sizeY-1, 0, h, stoneTexture, heightMap);
	}
	
	private static void PlaceWall(int x1, int x2, int y1, int y2, float h1, float h2, Texture stoneTexture, Texture heightMap) {
		GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
		cube.name = "Wall";
		cube.transform.position = new Vector3((x1 + x2 + 1)/2f, (h1 + h2)/2f, (y1 + y2 + 1)/2f);
		cube.transform.localScale = new Vector3((x2 - x1 + 1), (h2 - h1), (y2 - y1 + 1));
		Material material = cube.GetComponent<Renderer>().material;
		material.mainTexture = stoneTexture;
		material.mainTextureScale = new Vector2(.5f, .5f);
		
		material.EnableKeyword("_NORMALMAP");
		material.SetTexture("_BumpMap", heightMap);
		material.SetFloat("_BumpScale", 0.5f);
		
		Mesh mesh = cube.GetComponent<MeshFilter>().mesh;
		Vector2[] uvs = mesh.uv;
		uvs[1][0] = uvs[3][0] = uvs[7][0] = uvs[11][0] = (x2 - x1 + 1);
		uvs[2][1] = uvs[3][1] = uvs[6][1] = uvs[7][1] = (h2 - h1);
		uvs[18][0] = uvs[19][0] = uvs[22][0] = uvs[23][0] = (y2 - y1 + 1);
		uvs[18][1] = uvs[17][1] = uvs[21][1] = uvs[22][1] = (h2 - h1);
		mesh.uv = uvs;
	}
	
	private static void PlaceLabyrinth(string map, int sizeX, int sizeY, float h, Texture stoneTexture, Texture heightMap) {
		for (int y = 1; y < sizeY-1; y++) {
			int startWall = -1;
            for (int x = 1; x < sizeX; x++) {
				char field = GetField(map, sizeX, x, y);
				if(field == '#' && startWall == -1) {
					startWall = x;
				} else if(field != '#' && startWall == x-1) {
					startWall = -1;
				} else if((field != '#' && startWall >= 0) || x == sizeX-1) {
					PlaceWall(startWall, x-1, y, y, h, h+1, stoneTexture, heightMap);
					startWall = -1;
				}
            }
        }
		for (int x = 1; x < sizeX-1; x++) {
			int startWall = -1;
            for (int y = 1; y < sizeY; y++) {
				char field = GetField(map, sizeX, x, y);
				if(field == '#' && startWall == -1) {
					startWall = y;
				} else if((field != '#' || y == sizeY-1) && startWall >= 0) {
					PlaceWall(x, x, startWall, y-1, h, h+1, stoneTexture, heightMap);
					startWall = -1;
				}
            }
        }
	}
	
	private static void PlaceDoors(string map, int sizeX, int sizeY, float floorH, bool isCeiling) {
		float doorHeight = .8f;
		float h = isCeiling ? floorH - doorHeight/2f : floorH + doorHeight/2f;
		for (int x = 0; x < sizeX; x++) {
            for (int y = 0; y < sizeY; y++) {	
				char field = GetField(map, sizeX, x, y);
				if(field == 'D') {
					bool onVerticalWall = x == 0 || x == sizeX-1;
					GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
					cube.name = "Door";
					cube.transform.position = new Vector3(x+.5f, h, y+.5f);
					cube.transform.localScale = new Vector3((onVerticalWall ? 1.06f : .6f), doorHeight, (onVerticalWall ? .6f : 1.06f));
				}	
			}
		}
	}
	
	public static void BuildLabyrinth(int lvl, Texture stoneTexture, Texture lavaTexture, Texture heightMap, GameObject szalamiPrefab) {
		string map1 = t4maps.mapsFloor[lvl];
		string map2 = t4maps.mapsCeil[lvl];
		int sizeX = t4maps.sizeX[lvl];
		int sizeY = t4maps.sizeY[lvl];
	
		PlaceFloor(map2, sizeX, sizeY, ceilingHeight, true, lavaTexture);
		PlaceFloor(map1, sizeX, sizeY, 0f, false, lavaTexture);
		PlaceSzalami(map1, sizeX, sizeY, 0f, false, szalamiPrefab);
		PlaceOuterWalls(sizeX, sizeY, ceilingHeight, stoneTexture, heightMap);
		PlaceLabyrinth(map1, sizeX, sizeY, 0, stoneTexture, heightMap);
		PlaceLabyrinth(map2, sizeX, sizeY, ceilingHeight - 1, stoneTexture, heightMap);
		PlaceDoors(map1, sizeX, sizeY, 0, false);
	}
}
