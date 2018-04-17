using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TapCombination {


	private TapCombination() {
		
	}

	public static bool[] toFingers(int combination) {
		bool[] arr = new bool[5];
		arr [0] = (combination & 1) > 0 ? true : false;
		arr [1] = (combination & 2) > 0 ? true : false;
		arr [2] = (combination & 4) > 0 ? true : false;
		arr [3] = (combination & 8) > 0 ? true : false;
		arr [4] = (combination & 16) > 0 ? true : false;
		return arr;
	}

}
