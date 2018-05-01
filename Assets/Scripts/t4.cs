using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class t4 : MonoBehaviour {
	
	public Texture texture;
	public Texture lavaTexture;
	public Texture heightMap;
	public GameObject player;
	public GameObject szalamiPrefab;
	
	private string map1 = t4maps.map1;
	private string map2 = t4maps.map2;
	private int sizeX = t4maps.sizeX;
	private int sizeY = t4maps.sizeY;
	private const float ceilingHeight = 3f;
	
	private Vector2 currentPosition;
	
	
	private char GetField(string map, int sizeX, int x, int y) {
		return map[y * sizeX + x];
	}
	
	/*
	private Vector2 CurrentPosition() {
		Transform t = player.transform;
		return new Vector2((int)t.position[0], (int)t.position[2]);
	}
	
	private void PositionChanged(Vector2 newPosition) {
		string currentMap = t4person.toggleGravity ? map2 : map1;
		char field = GetField(currentMap, sizeX, (int)newPosition[0], (int)newPosition[1]);
		if(field == '_') {
			t4person.PlacePlayer(new Vector3(1.5f, 1.5f, 1f), new Quaternion(0, 0, 0, 1));
			currentPosition = new Vector2(1, 1);
		} else {
			currentPosition = newPosition;
		}
	}
	*/
	
	private void PlaceFloor(string map, int sizeX, int sizeY, float h, bool isCeiling) {
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
					plane.GetComponent<Renderer>().material.mainTexture = lavaTexture;	
					plane.GetComponent<Renderer>().material.EnableKeyword("_EMISSION");	
					plane.GetComponent<Renderer>().material.SetColor ("_EmissionColor", new Color(1f, .4f, .1f, 1f));
				}	
			}
		}
	}
	
	private void PlaceSzalami(string map, int sizeX, int sizeY, float floorH, bool isCeiling) {
		float h = isCeiling ? floorH - .4f : floorH + .4f;
		for (int x = 1; x < sizeX-1; x++) {
            for (int y = 1; y < sizeY; y++) {	
				char field = GetField(map, sizeX, x, y);
				if(field == 's') {
					GameObject szalami = Instantiate(szalamiPrefab, new Vector3(x+.5f, h, y+.5f), Quaternion.identity);
				}	
			}
		}
	}
	
	private void PlaceOuterWalls(int sizeX, int sizeY, float h) {
		PlaceWall(0, sizeX-1, 0, 0, 0, h);
		PlaceWall(0, 0, 0, sizeY-1, 0, h);
		PlaceWall(0, sizeX-1, sizeY-1, sizeY-1, 0, h);
		PlaceWall(sizeX-1, sizeX-1, 0, sizeY-1, 0, h);
	}
	
	private void PlaceWall(int x1, int x2, int y1, int y2, float h1, float h2) {
		GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
		cube.transform.position = new Vector3((x1 + x2 + 1)/2f, (h1 + h2)/2f, (y1 + y2 + 1)/2f);
		cube.transform.localScale = new Vector3((x2 - x1 + 1), (h2 - h1), (y2 - y1 + 1));
		cube.GetComponent<Renderer>().material.mainTexture = texture;
		cube.GetComponent<Renderer>().material.mainTextureScale = new Vector2(.5f, .5f);
		
		//cube.GetComponent<Renderer>().material.SetTexture("_ParallaxMap", heightMap);
		//cube.GetComponent<Renderer>().material.SetFloat("_Parallax", 0.08f);
		cube.GetComponent<Renderer>().material.EnableKeyword("_NORMALMAP");
		cube.GetComponent<Renderer>().material.SetTexture("_BumpMap", heightMap);
		cube.GetComponent<Renderer>().material.SetFloat("_BumpScale", 0.5f);
		
		Mesh mesh = cube.GetComponent<MeshFilter>().mesh;
		Vector2[] uvs = mesh.uv;
		uvs[1][0] = uvs[3][0] = uvs[7][0] = uvs[11][0] = (x2 - x1 + 1);
		uvs[2][1] = uvs[3][1] = uvs[6][1] = uvs[7][1] = (h2 - h1);
		uvs[18][0] = uvs[19][0] = uvs[22][0] = uvs[23][0] = (y2 - y1 + 1);
		uvs[18][1] = uvs[17][1] = uvs[21][1] = uvs[22][1] = (h2 - h1);
		mesh.uv = uvs;
	}
	
	private void PlaceLabyrinth(string map, int sizeX, int sizeY, float h) {
		for (int y = 1; y < sizeY-1; y++) {
			int startWall = -1;
            for (int x = 1; x < sizeX; x++) {
				char field = GetField(map, sizeX, x, y);
				if(field == '#' && startWall == -1) {
					startWall = x;
				} else if(startWall == x-1) {
					startWall = -1;
				} else if((field != '#' && startWall >= 0) || x == sizeX-1) {
					PlaceWall(startWall, x-1, y, y, h, h+1);
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
				} else if((field != '#' && startWall >= 0) || y == sizeY-1) {
					PlaceWall(x, x, startWall, y-1, h, h+1);
					startWall = -1;
				}
            }
        }
	}
	
	void Start () {
		PlaceFloor(map2, sizeX, sizeY, ceilingHeight, true);
		PlaceFloor(map1, sizeX, sizeY, 0f, false);
		PlaceSzalami(map1, sizeX, sizeY, 0f, false);
		PlaceOuterWalls(sizeX, sizeY, ceilingHeight);
		PlaceLabyrinth(map1, sizeX, sizeY, 0);
		PlaceLabyrinth(map2, sizeX, sizeY, ceilingHeight - 1);
	}
	
	void Update() {
		/*
		Vector2 newPosition = CurrentPosition();
		if(newPosition != currentPosition) {
			PositionChanged(newPosition);
		}
		*/
	}
}
