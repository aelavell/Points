using UnityEngine;
using System.Collections;

public class Poinze : MonoBehaviour {
	public void FinishedAnimation() {
		GetComponent<Animator>().enabled = false;
	}
}