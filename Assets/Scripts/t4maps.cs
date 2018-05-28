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
		public Vase[] vases;
		
		public Level(string amapFloor, string amapCeil, int asizeX, int asizeY, Torch[] atorches, Vase[] avases) {
			mapFloor = amapFloor;
			mapCeil = amapCeil;
			sizeX = asizeX;
			sizeY = asizeY;
			torches = atorches;
			vases = avases;
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
	
	public class Vase {		
		public int x;
		public int y;
		public char orientation;
		public bool ceiling;
		
		public Vase(int ax, int ay, char aorientation, bool aceiling) {
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
			"#  ___  D" +
			"#  v v  #" +
			"#  ___  #" +
			"#########",
			
			"#########" +
			"#       #" +
			"#       #" +
			"#       #" +
			"#########",
			
			9, 5,
			
			new Torch[] {
				new Torch(1, 1, 'n', false),
				new Torch(1, 3, 's', false),
				new Torch(3, 1, 'n', false),
				new Torch(3, 3, 's', false),
				new Torch(5, 1, 'n', false),
				new Torch(5, 3, 's', false),
				new Torch(7, 1, 'n', false),
				new Torch(7, 3, 's', false)
			},
			new Vase[] {
				new Vase(2, 1, 'w', false),
				new Vase(7, 3, 's', false)
			}
		),
		new Level(
			"#########" +
			"#  __#  #" +
			"#  ___  D" +
			"#  _s_  #" +
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
				new Torch(1, 1, 'n', false),
				new Torch(1, 4, 's', false),
				new Torch(7, 1, 'n', false),
				new Torch(7, 4, 's', false)
			},
			new Vase[] {
			}
		),
		new Level(
			"#########" +
			"#       #" +
			"#######v#" +
			"# #   v #" +
			"#_#######" +
			"#____s  D" +
			"#########",
			
			"#########" +
			"####_s_##" +
			"###s#####" +
			"#   #_###" +
			"# ### ###" +
			"#     ###" +
			"#########",
			
			9, 7,
			
			new Torch[] {
				new Torch(2, 1, 'n', false),
				new Torch(7, 1, 'e', false),
				new Torch(7, 3, 's', false),
				new Torch(5, 3, 'n', false),
				new Torch(3, 3, 's', true),
				new Torch(1, 4, 'e', true),
				new Torch(3, 5, 's', true),
				new Torch(6, 5, 's', false),
				new Torch(1, 4, 'w', false)
			},
			new Vase[] {
			}
		),
		new Level(
			"#########" +
			"# #######" +
			"# #v    #" +
			"# # ### #" +
			"# # #s# #" +
			"# #   # #" +
			"# ##### #" +
			"#   v   #" +
			"#########",
			
			"#########" +
			"##s     D" +
			"## ######" +
			"##v#___##" +
			"##v#_  ##" +
			"## ### ##" +
			"##  s  ##" +
			"##_____##" +
			"#########",
			
			9, 9,
			
			new Torch[] {
				new Torch(1, 1, 'n', false),
				new Torch(1, 4, 'e', false),
				new Torch(1, 7, 'w', false),
				new Torch(3, 7, 'n', false),
				new Torch(6, 7, 's', false),
				new Torch(7, 2, 'n', false),
				new Torch(3, 3, 'w', false),
				new Torch(5, 4, 'n', false),
				new Torch(4, 6, 'n', true),
				new Torch(2, 6, 'w', true),
				new Torch(2, 2, 'e', true),
				new Torch(3, 1, 'n', true),
			},
			new Vase[] {
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
			},
			new Vase[] {
			}
		)
	};
}
