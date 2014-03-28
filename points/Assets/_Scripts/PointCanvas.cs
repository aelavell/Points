using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class PointCanvas : Singleton<PointCanvas> {
	Texture2D tex;
	public Color32[] colorMap;
	byte[] byteImg;

	void Start() {
		tex = new Texture2D(Mix.Instance.CanvasSize, Mix.Instance.CanvasSize);
		tex.filterMode = FilterMode.Point;
		renderer.material.mainTexture = tex;
		byteImg = new byte[(int)Mathf.Pow(Mix.Instance.CanvasSize, 2)];
		ClearImage();
	}

	void Update() {
		UpdateDisplay(byteImg);
	}

	public void AddPoint(byte teamIndex) {
		byteImg[ChooseRandomFreeIndex()] = teamIndex;
	}

	int ChooseRandomFreeIndex() {
		// want to build: list of indices of black pixels
		// easy mode: for loop, if value is 0, add i

		var freeIndices = new List<int>();
		for (int i = 0; i < byteImg.Length; i++) {
			if (byteImg[i] == 0) {
				freeIndices.Add(i);
			}
		}

		return freeIndices[UnityEngine.Random.Range(0, freeIndices.Count)];
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

	[ContextMenu("Clear")]
	void ClearImage() {
		for (int i = 0; i < Mathf.Pow(Mix.Instance.CanvasSize, 2); i++) {
			byteImg[i] = 0;
		}
	}

	public void UpdateDisplay(byte[] image) {
		tex.SetPixels32(IndexedImageToColor(image));
		tex.Apply();
	}
}