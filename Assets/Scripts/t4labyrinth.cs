using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class t4labyrinth : MonoBehaviour {
	
	public Texture stoneTexture;
	public Texture heightMap;
	public Texture doorTexture;
	public Texture doorHeightMap;
	public GameObject szalamiPrefab;
	
	public t4torches torchesScript;
	public t4vases vasesScript;
	public t4spikes spikesScript;
	public t4floor floorScript;
	
	private const float ceilingHeight = 2.7f;
	private List<GameObject> walls = new List<GameObject>();
	private List<GameObject> szalamis = new List<GameObject>();
	private List<GameObject> flames = new List<GameObject>();
	private List<GameObject> doors = new List<GameObject>();
	
	
	private char GetField(string map, int sizeX, int x, int y) {
		return map[y * sizeX + x];
	}
	
	private void PlaceSzalami(int x, int y, float floorH, bool isCeiling) {
		float h = isCeiling ? floorH - .4f : floorH + .4f;
		GameObject szalami = Instantiate(szalamiPrefab, new Vector3(x+.5f, h, y+.5f), Quaternion.identity);
		szalami.name = "Szalami";
		szalamis.Add(szalami);	
	}
	
	private void PlaceOuterWalls(int sizeX, int sizeY, float h) {
		PlaceWall(0, sizeX-1, 0, 0, -.2f, h);
		PlaceWall(0, 0, 0, sizeY-1, -.2f, h);
		PlaceWall(0, sizeX-1, sizeY-1, sizeY-1, -.2f, h);
		PlaceWall(sizeX-1, sizeX-1, 0, sizeY-1, -.2f, h);
	}
	
	private void PlaceWall(int x1, int x2, int y1, int y2, float h1, float h2) {
		GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
		cube.name = "Wall";
		cube.transform.position = new Vector3((x1 + x2 + 1)/2f, (h1 + h2)/2f, (y1 + y2 + 1)/2f);
		cube.transform.localScale = new Vector3(
			(x2 - x1 + 1) - (x1==x2 ? 0 : .05f), 
			(h2 - h1) * Random.Range(1f, 1.2f), 
			(y2 - y1 + 1) - (y1==y2 ? 0 : .05f));
		walls.Add(cube);
		
		Material material = cube.GetComponent<Renderer>().material;
		material.mainTexture = stoneTexture;
		material.mainTextureScale = new Vector2(.3f, .3f);
		material.EnableKeyword("_NORMALMAP");
		material.SetTexture("_BumpMap", heightMap);
		material.SetFloat("_BumpScale", 0.5f);
		material.EnableKeyword("_EMISSION");	
		material.SetTexture("_EmissionMap", stoneTexture);
		material.SetColor("_EmissionColor", new Color(.05f, .05f, .05f, .05f));
		
		Mesh mesh = cube.GetComponent<MeshFilter>().mesh;
		Vector2[] uvs = mesh.uv;
		print(uvs);
		uvs[1][0] = uvs[3][0] = uvs[7][0] = uvs[11][0] = uvs[5][0] = uvs[9][0] = (x2 - x1 + 1);
		uvs[2][1] = uvs[3][1] = uvs[6][1] = uvs[7][1] = (h2 - h1);
		uvs[18][0] = uvs[19][0] = uvs[22][0] = uvs[23][0] = uvs[4][1] = uvs[5][1] = (y2 - y1 + 1);
		uvs[18][1] = uvs[17][1] = uvs[21][1] = uvs[22][1] = (h2 - h1);
		mesh.uv = uvs;
	}
	
	private void PlaceLabyrinth(string map, int sizeX, int sizeY, float h) {
		for (int y = 1; y < sizeY-1; y++) {
			int startWall = -1;
            for (int x = 1; x < sizeX; x++) {
				char field = GetField(map, sizeX, x, y);
				if((field == '#' || field == 'D') && startWall == -1) {
					startWall = x;
				} else if(field != '#' && startWall == x-1) {
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
				} else if(field != '#' && startWall == y-1
							&& (GetField(map, sizeX, x-1, y-1) == '#' || GetField(map, sizeX, x+1, y-1) == '#')) {
					startWall = -1;
				} else if((field != '#' || y == sizeY-1) && startWall >= 0) {
					PlaceWall(x, x, startWall, y-1, h, h+1);
					startWall = -1;
				}
            }
        }
	}
	
	private void PlaceDoor(int x, int y, int sizeX, float floorH, bool isCeiling) {
		float doorHeight = .8f;
		float h = isCeiling ? floorH - doorHeight/2f : floorH + doorHeight/2f;
		bool onVerticalWall = x == 0 || x == sizeX-1;
		
		GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
		cube.name = "Door";
		// works only for x=sizeX-1!
		cube.transform.position = new Vector3(x+.4f, h, y+.5f);
		cube.transform.localScale = new Vector3((onVerticalWall ? 1f : .6f), doorHeight, (onVerticalWall ? .6f : 1f));
		doors.Add(cube);
		
		Material material = cube.GetComponent<Renderer>().material;
		material.mainTexture = doorTexture;
		material.EnableKeyword("_EMISSION");	
		material.SetTexture("_EmissionMap", doorTexture);
		material.SetColor("_EmissionColor", new Color(.5f, .5f, .5f, .1f));
		material.EnableKeyword("_NORMALMAP");
		material.SetTexture("_BumpMap", doorHeightMap);
		material.SetFloat("_BumpScale", 0.5f);
		
		cube.GetComponent<Collider>().isTrigger = true;
		
	}
	
	private void PlaceObjects(string map, int sizeX, int sizeY, float floorH, bool isCeiling) {
		for (int x = 0; x < sizeX; x++) {
            for (int y = 0; y < sizeY; y++) {	
				char field = GetField(map, sizeX, x, y);
				if(field == 'D') PlaceDoor(x, y, sizeX, floorH, isCeiling);
				else if(field == 's') PlaceSzalami(x, y, floorH, isCeiling);
				else if(field == 'v') spikesScript.PlaceSpikes(x, y, floorH, isCeiling, y*sizeX + x);
			}
		}
	}
	
	private void PlaceFlames(string map, int sizeX, int sizeY, float floorH, bool isCeiling) {
		GameObject flame =  GameObject.Find("Flames");
	
		for (int x = 0; x < sizeX; x++) {
            for (int y = 0; y < sizeY; y++) {	
				char field = GetField(map, sizeX, x, y);
				if(field == '_') {
					GameObject new_flame = Instantiate(flame) as GameObject;
					new_flame.transform.position = new Vector3(x+.5f, 0, y+.5f);
					flames.Add(new_flame);
				}	
			}
		}
	}
	
	private void ClearAll() {
		walls.ForEach(Destroy);
		szalamis.ForEach(Destroy);
		flames.ForEach(Destroy);
		doors.ForEach(Destroy);
		
		walls.Clear();
		szalamis.Clear();
		flames.Clear();
		doors.Clear();
		
		torchesScript.ClearAll();
		spikesScript.ClearAll();
		floorScript.ClearAll();
		vasesScript.ClearAll();
	}
	
	public void BuildLabyrinth(int lvl) {
		ClearAll();
		
		t4maps.Level levelData = t4maps.levels[lvl];
		string map1 = levelData.mapFloor;
		string map2 = levelData.mapCeil;
		int sizeX = levelData.sizeX;
		int sizeY = levelData.sizeY;
	
		floorScript.PlaceFloor(map1, sizeX, sizeY, 0f, false);
		floorScript.PlaceFloor(map2, sizeX, sizeY, ceilingHeight, true);
		PlaceOuterWalls(sizeX, sizeY, ceilingHeight);
		PlaceLabyrinth(map1, sizeX, sizeY, 0);
		PlaceLabyrinth(map2, sizeX, sizeY, ceilingHeight - 1);
		PlaceObjects(map1, sizeX, sizeY, 0, false);
		PlaceObjects(map2, sizeX, sizeY, ceilingHeight, true);
		PlaceFlames(map1, sizeX, sizeY, 0, false);
		
		torchesScript.PlaceTorches(lvl);
		vasesScript.PlaceVases(lvl);
	}
}
