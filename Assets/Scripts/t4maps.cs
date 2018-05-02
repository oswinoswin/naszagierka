using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class t4maps : MonoBehaviour {
	
	public class Level {
		public string mapFloor;
		public string mapCeil;
		public int sizeX;
		public int sizeY;
		public Torches torches;
		
		public Level(string amapFloor, string amapCeil, int asizeX, int asizeY, Torches atorches) {
			mapFloor = amapFloor;
			mapCeil = amapCeil;
			sizeX = asizeX;
			sizeY = asizeY;
			torches = atorches;
		}
	}
	
	public class Torches {		
		public int[] xs;
		public int[] ys;
		public char[] orientations;
		public bool[] ceiling;
		
		public Torches(int[] axs, int[] ays, char[] aorientations, bool[] aceiling) {
			xs = axs;
			ys = ays;
			orientations = aorientations;
			ceiling = aceiling;
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
			
			10, 6,
			
			new Torches(
				new int[] {1, 2, 3, 3, 1}, 
				new int[] {1, 1, 1, 2, 1}, 
				new char[] {'w', 'n', 'e', 's', 'w'}, 
				new bool[] {false, false, false, false, true}
			)
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
			
			4, 4,
			
			new Torches(
				new int[] {2}, 
				new int[] {2}, 
				new char[] {'e'}, 
				new bool[] {true}
			)
		)
	};
}
