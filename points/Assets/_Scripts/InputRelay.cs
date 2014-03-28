using UnityEngine;
using System.Collections;

public class InputRelay : MonoBehaviour {
	public KeyCode[] keys;

	void KeyPressed(int index) {

	}

	void Update() {
		int i = 0;
		foreach (var key in keys) {
			if (Input.GetKeyDown(key)) {
				KeyPressed(i);
			}
			i++;
		}
	}
}