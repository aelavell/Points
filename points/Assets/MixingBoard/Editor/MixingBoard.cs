using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Reflection;

public class MixingBoard : EditorWindow {	
	[MenuItem ("Window/Mixing Board")]
    static void ShowWindow() {
        // Get existing open window or if none, make a new one:
        MixingBoard.GetWindow(typeof(MixingBoard));
    }
	
	Mix mixToLoad;
	string mixSaveName;
	
	void OnGUI() {	
		GUILayout.Label("Mixing Board || User Manual: thinkrad.net/mixingboard", EditorStyles.boldLabel);
		GUILayout.BeginHorizontal();
		mixToLoad = EditorGUILayout.ObjectField(mixToLoad, typeof(Mix), false) as Mix;
		if (GUILayout.Button("Load")) {
			LoadMix();
		}
		GUILayout.EndHorizontal();
		
		GUILayout.BeginHorizontal();
		mixSaveName = EditorGUILayout.TextField(mixSaveName);
		if (GUILayout.Button("Save")) {
			SaveMix();
		}
		GUILayout.EndHorizontal();
		
		Mix.Instance.OnGUI();
	}
	
	void LoadMix() {
		if (mixToLoad != null) {
			Mix.Instance.LoadValues(mixToLoad);
			mixSaveName = mixToLoad.name;
			mixToLoad = null;
		}
		else {
			Debug.Log("You must select a mix before pressing Load.");
		}
	}
	
	void SaveMix() {
		if (mixSaveName != "") {
			Mix.Instance.SaveCopy("Assets/Resources/Mixes/" + mixSaveName + ".asset");
		}
		else {
			Debug.Log("You must enter a name for the mix before pressing Save.");
		}
	}
	
	void OnInspectorUpdate() {
		Repaint();
	}
}