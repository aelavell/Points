using UnityEngine;
using System.Collections;

public class VictoryScene : MonoBehaviour {
	public Animator animator;

	void Start() {
		animator = GetComponent<Animator>();
		animator.enabled = false;
	}

	[ContextMenu("play")]
	void Play() {
		animator.enabled = true;
		animator.Play("ThisRound");
	}
	
}