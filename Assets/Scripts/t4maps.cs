﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class t4maps : MonoBehaviour {
	
	/*
	[ ] - empty space
	[#] - wall
	[_] - lava
	[s] - szalami
	[D] - door (level complete)
	*/

	public static string[] mapsFloor = {
		"##########" +
		"#   #  s##" +
		"# #   # _#" +
		"#_####  ##" +
		"#_#___   #" +
		"#######D##"
	};
	
	public static string[] mapsCeil = {
		"##########" +
		"#        #" +
		"#    #   #" +
		"#    #   #" +
		"#        #" +
		"##########"
	};
		
	public static int[] sizeX = {10};
	public static int[] sizeY = {6};
}
