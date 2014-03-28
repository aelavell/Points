using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PointCanvas : MonoBehaviour {
	Texture2D tex;
	public Color32[] colorMap;
	byte[] byteImg;

	void Start() {
		tex = new Texture2D(Mix.Instance.CanvasSize, Mix.Instance.CanvasSize);
		tex.filterMode = FilterMode.Point;
		renderer.material.mainTexture = tex;
		byteImg = new byte[(int)Mathf.Pow(Mix.Instance.CanvasSize, 2)];
		GenerateRandomImage();
	}

	void Update() {
		UpdateDisplay(byteImg);
	}
	
	Color32[] IndexedImageToColor(byte[] image) {
		var colorImage = new Color32[(int)Mathf.Pow(Mix.Instance.CanvasSize, 2)];
		int i = 0;
		foreach (var colorIndex in image) {
			colorImage[i] = colorMap[colorIndex];
			i++;
		}
		return colorImage;
	}

	[ContextMenu("Generate")]
	void GenerateRandomImage() {
		for (int i = 0; i < Mathf.Pow(Mix.Instance.CanvasSize, 2); i++) {
			byteImg[i] = (byte)UnityEngine.Random.Range(0, 5);
		}
	}

	public void UpdateDisplay(byte[] image) {
		tex.SetPixels32(IndexedImageToColor(image));
		tex.Apply();
	}
}