using UnityEngine;
using System.Collections;

public class CommandRelay : MonoBehaviour {
	public int teamIndex;
	int points;
	public KeyCode[] keys;

	void OnNetworkInstantiate(NetworkMessageInfo info) {
		//if (Network.isClient && info.sender != Network.player) {
		//	Destroy(gameObject);
		//}
		//else {
			if (GlobalEvents.commandRelayCreated != null) GlobalEvents.commandRelayCreated(this, info.sender);
		//}
	}

	void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info) {
		stream.Serialize(ref teamIndex);
		stream.Serialize(ref points);
		if (stream.isReading) {
			StateRelay.Instance.UpdatePoints((byte)teamIndex, points);
		}
	}

	void OnGUI() {
		GUI.Label(new Rect(5, teamIndex * 30,100,30), teamIndex + " " + points);
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