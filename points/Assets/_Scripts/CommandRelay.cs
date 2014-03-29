using UnityEngine;
using System.Collections;

public class CommandRelay : MonoBehaviour {
	public char teamIndex;
	int points;
	public KeyCode[] keys;

	void OnNetworkInstantiate(NetworkMessageInfo info) {
		if (GlobalEvents.commandRelayCreated != null) GlobalEvents.commandRelayCreated(this, info.sender);
	}

	void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info) {
		stream.Serialize(ref points);
		if (stream.isReading) {
			StateRelay.Instance.UpdatePoints(teamIndex, points);
		}
	}

	void OnGUI() {
		GUI.Label(new Rect(5,30,100,30), points.ToString());
	}

	void Update() {
		if (Network.isClient) {
			int i = 0;
			foreach (var key in keys) {
				if (Input.GetKeyDown(key)) {
					points++;
				}
				i++;
			}
		}
	}
}