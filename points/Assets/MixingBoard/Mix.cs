using UnityEngine;
using System;
using System.Collections;
#if UNITY_EDITOR
using UnityEditor;
#endif

[System.Serializable]
public class Mix : ScriptableSingleton<Mix> {
	// *** Place your custom variables here		
	public int CanvasSize;
	public int requiredNumPlayers;
	public int BlankColorIndex = 4;
	// *** 
	
	#if UNITY_EDITOR
	public void OnGUI() {
		foreach (var fieldInfo in typeof(Mix).GetFields()) {
			var attributes = fieldInfo.GetCustomAttributes(false);
			var type = fieldInfo.FieldType;
			if (attributes.Length > 0) {
				var range = attributes[0] as SliderRange;	
				
				if (type == typeof(float)) {	
					var value = Convert.ToSingle(fieldInfo.GetValue(this));
					var target = EditorGUILayout.Slider(fieldInfo.Name, value, range.min, range.max);	
					fieldInfo.SetValue(this, target);
					if (target != value) EditorUtility.SetDirty(this);		
				}
				else if (type == typeof(int)) {
					var value = Convert.ToInt32(fieldInfo.GetValue(this));
					var target = EditorGUILayout.IntSlider(fieldInfo.Name, value, (int)(range.min), (int)(range.max));
					fieldInfo.SetValue(this, target);
					if (target != value) EditorUtility.SetDirty(this);	
				}			
			}
			else {
				if (type == typeof(float)){
					float value = Convert.ToSingle(fieldInfo.GetValue(this));
					float target = EditorGUILayout.FloatField(fieldInfo.Name,value);
					fieldInfo.SetValue(this,target);
					if (target!=value){
						EditorUtility.SetDirty(this);
					}
				}
				else if (type == typeof(int)){
					int value = Convert.ToInt32(fieldInfo.GetValue(this));
					int target = EditorGUILayout.IntField(fieldInfo.Name,value);
					fieldInfo.SetValue(this,target);
					if (target!=value){
						EditorUtility.SetDirty(this);
					}
				}
			}
		}
	}
	#endif
}


[System.AttributeUsage(System.AttributeTargets.Field)]
public class SliderRange : System.Attribute {
	public float min;
	public float max;

    public SliderRange(float min, float max)
    {
		this.min = min;
		this.max = max;
    }
}