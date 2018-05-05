using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class t4maps : MonoBehaviour {
	
	public class Level {
		public string mapFloor;
		public string mapCeil;
		public int sizeX;
		public int sizeY;
		public Torch[] torches;
		
		public Level(string amapFloor, string amapCeil, int asizeX, int asizeY, Torch[] atorches) {
			mapFloor = amapFloor;
			mapCeil = amapCeil;
			sizeX = asizeX;
			sizeY = asizeY;
			torches = atorches;
		}
	}
	
	public class Torch {		
		public int x;
		public int y;
		public char orientation;
		public bool ceiling;
		
		public Torch(int ax, int ay, char aorientation, bool aceiling) {
			x = ax;
			y = ay;
			orientation = aorientation;
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
			"#########" +
			"#  __#  #" +
			"#  ___  D" +
			"#  _ _  #" +
			"#  ___  #" +
			"#########",
			
			"#########" +
			"#       #" +
			"#       #" +
			"#       #" +
			"#       #" +
			"#########",
			
			9, 6,
			
			new Torch[] {
				new Torch(1, 1, 'n', false)
			}
		),
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
			
			new Torch[] {
				new Torch(1, 1, 'w', false),
				new Torch(2, 1, 'n', false),
				new Torch(3, 1, 'e', false),
				new Torch(3, 2, 's', false),
				new Torch(1, 1, 'w', true),
				new Torch(7, 1, 'n', false)
			}
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
			
			new Torch[] {
				new Torch(2, 2, 'e', true)
			}
		)
	};
}
