using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PointCanvas : Singleton<PointCanvas> {
	Texture2D tex;
	public Color32[] colorMap;
	StateRelay stateRelay;

	void Start() {
		tex = new Texture2D(Mix.Instance.CanvasSize, Mix.Instance.CanvasSize);
		tex.filterMode = FilterMode.Point;
		renderer.material.mainTexture = tex;

		enabled = false;
		GlobalEvents.stateRelayCreated += () => enabled = true;
	}

	void Update() {
		if (StateRelay.Instance.state == State.play) {
			UpdateDisplay(StateRelay.Instance.canvas);
		}
	}
	
	public void UpdateDisplay(char[] image) {
		tex.SetPixels32(IndexedImageToColor(image));
		tex.Apply();
	}

	Color32[] IndexedImageToColor(char[] image) {
		var colorImage = new Color32[(int)Mathf.Pow(Mix.Instance.CanvasSize, 2)];
		int i = 0;
		foreach (var colorIndex in image) {
			colorImage[i] = colorMap[colorIndex];
			i++;
		}
		return colorImage;
	}
}