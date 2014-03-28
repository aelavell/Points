using UnityEngine;
using System.Collections;

public class InputRelay : MonoBehaviour {
	public KeyCode[] keys;

	void KeyPressed(byte index) {
		PointCanvas.Instance.AddPoint(index);
	}

	void Update() {
		byte i = 0;
		foreach (var key in keys) {
			if (Input.GetKeyDown(key)) {
				KeyPressed((byte)(i+1));
			}
			i++;
		}
	}
}