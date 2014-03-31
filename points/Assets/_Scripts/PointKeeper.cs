using UnityEngine;
using System.Collections;

public class PointKeeper : MonoBehaviour {
	public int teamIndex;
	UILabel label;

	void Start () {
		label = GetComponent<UILabel>();
		enabled = false;
		GlobalEvents.stateRelayCreated += () => enabled = true;
	}

	void Update () {
		label.text = StateRelay.Instance.pointsPerTeam[teamIndex].ToString();
	}
}
