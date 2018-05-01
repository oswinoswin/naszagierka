using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class t4 : MonoBehaviour {
	
	public Texture texture;
	public Texture heightMap;
	public GameObject player;
	
	private string map1 = "" +
		"##########" +
		"#   #   ##" +
		"# #   #  #" +
		"######  ##" +
		"#        #" +
		"##########";
	
	private string map2 = "" +
		"##########" +
		"#        #" +
		"#    #   #" +
		"#    #   #" +
		"#        #" +
		"##########";
		
	private const int sizeX = 10;
	private const int sizeY = 6;
	private const float ceilingHeight = 3f;
	
	private Vector2 currentPosition;
	
	
	private char GetField(string map, int sizeX, int x, int y) {
		return map[y * sizeX + x];
	}
	
	private Vector2 CurrentPosition() {
		Transform t = player.transform;
		return new Vector2((int)t.position[0], (int)t.position[2]);
	}
	
	private void PositionChanged(Vector2 newPosition) {
		print(newPosition);
		currentPosition = newPosition;
	}
	
	private void PlacePlane(int sizeX, int sizeY, float h, bool isCeiling) {
		GameObject plane = GameObject.CreatePrimitive(PrimitiveType.Plane);
		plane.transform.position = new Vector3(sizeX / 2f, h, sizeY / 2f);
		plane.transform.rotation = Quaternion.Euler(isCeiling ? 180 : 0, 0, 0);
		plane.transform.localScale = new Vector3(sizeX / 10f, 1f, sizeY / 10f);
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
		//print(System.String.Join("", new List<Vector2>(mesh.uv).ConvertAll(i => i.ToString()).ToArray()));
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
				} else if((field == ' ' && startWall >= 0) || x == sizeX-1) {
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
				} else if((field == ' ' && startWall >= 0) || y == sizeY-1) {
					PlaceWall(x, x, startWall, y-1, h, h+1);
					startWall = -1;
				}
            }
        }
	}
	
	void Start () {
		PlacePlane(sizeX, sizeY, ceilingHeight, true);
		PlacePlane(2*sizeX, 2*sizeY, 0f, false);
		PlaceOuterWalls(sizeX, sizeY, ceilingHeight);
		PlaceLabyrinth(map1, sizeX, sizeY, 0);
		PlaceLabyrinth(map2, sizeX, sizeY, ceilingHeight - 1);
	}
	
	void Update() {
		Vector2 newPosition = CurrentPosition();
		if(newPosition != currentPosition) {
			PositionChanged(newPosition);
		}
	}
}
