using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class t4maps : MonoBehaviour {
	
	public class Level {
		public string mapFloor;
		public string mapCeil;
		public int sizeX;
		public int sizeY;
		
		public Level(string amapFloor, string amapCeil, int asizeX, int asizeY) {
			mapFloor = amapFloor;
			mapCeil = amapCeil;
			sizeX = asizeX;
			sizeY = asizeY;
		}
	}
	
	/*
	[ ] - empty space
	[#] - wall
	[_] - lava
	[s] - szalami
	[D] - door (level complete)
	*/
	
	public static Level[] levels = {
		new Level(
			"##########" +
			"#   #  s##" +
			"#     # _#" +
			"#_####  ##" +
			"#_#___   #" +
			"#######D##",
			
			"##########" +
			"#  #     #" +
			"##   #   #" +
			"#    #  ##" +
			"#  #     #" +
			"##########",
			
			10, 6
		),
		new Level(
			"####" +
			"#  #" +
			"#s #" +
			"####",
			
			"####" +
			"# ##" +
			"## #" +
			"####",
			
			4, 4
		)
	};

	public static string[] mapsFloor = {
		
		
		
	};
	
	public static string[] mapsCeil = {
		
		
		"####" +
		"# ##" +
		"#  #" +
		"####"
	};
		
	public static int[] sizeX = {10, 4};
	public static int[] sizeY = {6, 4};
	
	// torches
	public static int[] xs = {1, 2, 3, 3, 1};
	public static int[] ys = {1, 1, 1, 2, 1};
	public static char[] orientations = {'w', 'n', 'e', 's', 'w'};
	public static bool[] ceiling = {false, false, false, false, true};
}
