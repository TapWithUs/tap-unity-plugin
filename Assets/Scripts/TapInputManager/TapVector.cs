using UnityEngine;
using System.Collections;

public class TapVector {

	public int[] arr;

	public TapVector(int x,int y,int z,int w,int u)
	{
		arr = new int[5];
		arr [0] = x;
		arr [1] = y;
		arr [2] = z;
		arr [3] = w;
		arr [4] = u;
	}

	public TapVector(int binaryTapcode) {
		arr = new int[5];
		arr [0] = (binaryTapcode & 1) > 0 ? 1 : 0;
		arr [1] = (binaryTapcode & 2) > 0 ? 1 : 0;
		arr [2] = (binaryTapcode & 4) > 0 ? 1 : 0;
		arr [3] = (binaryTapcode & 8) > 0 ? 1 : 0;
		arr [4] = (binaryTapcode & 16) > 0 ? 1 : 0;
	}
}
