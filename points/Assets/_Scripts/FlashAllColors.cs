using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FlashAllColors : MonoBehaviour {
	public float flashTime = 0.06f; 
	float time;
	int index = 0;
	UIWidget widget;

	void Start() {
		widget = GetComponent<UIWidget>();
	}

	void Update() {
		time += Time.deltaTime;
		if (time > flashTime) {
			index = (index + 1) % (PointCanvas.Instance.colorMap.Length - 1);
			time -= flashTime;
			widget.color = PointCanvas.Instance.colorMap[index];
		}
	}
}