using UnityEngine;
using System.Collections;

public class PointKeeper : MonoBehaviour {
	public bool allTime;
	public int teamIndex;
	UILabel label;

	void Start () {
		label = GetComponent<UILabel>();
		enabled = false;
		GlobalEvents.stateRelayCreated += () => enabled = true;
	}

	void Update () {
		if (allTime) {
			label.text = StateRelay.Instance.allTimePointsPerTeam[teamIndex].ToString();
		}
		else {
			label.text = StateRelay.Instance.pointsPerTeam[teamIndex].ToString();
		}
	}
}
